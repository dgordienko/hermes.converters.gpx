using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;
using Hermes.Api.Clasess.Geo;
using System.Xml.Linq;
using System.Linq;
using System.Globalization;

namespace hermes.gpx.converters
{
	/// <summary>
	/// Методы рсширения для конвертера маршрутов автотраспорта
	/// </summary>
	public static class GpxFileConverterExtentionMethods
	{
		/// <summary>
		/// Сегмент трека
		/// </summary>
		const string Trkseg = "trkseg";

		/// <summary>
		/// Трек
		/// </summary>
		const string Trk = "trk";

		/// <summary>
		/// Радиус Земли
		/// </summary>
		private const double EarthRadius = 6378.7;



		/// <summary>
		///  Возвращает дистанцию по сегменту трека
		/// </summary>
		/// <param name="points"></param>
		/// <returns></returns>
		public static double ToSegmentDistance(this IEnumerable<IRoutePoint> points )
		{
			if(points == null)
				throw  new ArgumentNullException("points");
			var p = points.ToList();
			var result = 0.0;
			for (var i = 0; i < p.Count; i++)
			{
				var p1 = p[i];
				if (i == (p.Count - 1)) continue; //!+Что делать если точка одна
				var p2 = p[i + 1];
				result = result + GetDistanceFromPoints(p1, p2);                
			}
			return result;
		}
		/// <summary>
		/// Длительность сегмента трека
		/// </summary>
		/// <param name="points">Набр точек, характеризующих маршрут трансопртного средства</param>
		/// <returns></returns>
		public static TimeSpan ToSegmentDuration(this IEnumerable<IRoutePoint> points)
		{
			if(points == null)
				throw new ArgumentNullException("points");
			var q = points.OrderBy (x => x.Time);
			var d1 = q.FirstOrDefault();
			var d2 = q.LastOrDefault();
			if ((d2 != null) && (d1!=null)) return d2.Time - d1.Time;
			return TimeSpan.Zero;
		}
		/// <summary>
		/// Возвращает растояние между двумя географическими точками
		/// </summary>
		/// <param name="point1">Первая точка(начало отсчета)</param>
		/// <param name="point2">Вторая точка(окончание отсчета)</param>
		/// <returns>Растояние между двумя географическими точками</returns>
		/// <remarks>
		/// Используется расчет предоставленный  http://www.meridianworlddata.com/Distance-Calculation.asp
		/// Формула расчета x = EarthRadius * arctan[sqrt(1-x^2)/x], где
		/// EarthRadius - радиус Земли
		/// х = x = [sin(lat1/57.2958) * sin(lat2/57.2958)] +
		/// +[cos(lat1/57.2958) * cos(lat2/57.2958) * cos(lon2/57.2958 - lon1/57.2958)]
		/// </remarks>
		private static double GetDistanceFromPoints(IRoutePoint point1, IRoutePoint point2)
		{
			var dLat1InRad = point1.Position.Lat * (Math.PI / 180.0);
			var dLong1InRad = point1.Position.Lon * (Math.PI / 180.0);
			var dLat2InRad = point2.Position.Lat * (Math.PI / 180.0);
			var dLong2InRad = point2.Position.Lon * (Math.PI / 180.0);
			var dLongitude = dLong2InRad - dLong1InRad;
			var dLatitude = dLat2InRad - dLat1InRad;
			var x = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
				Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
				Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);
			var dist = 2.0 * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1.0 - x));
			return EarthRadius * dist;
		}

		/// <summary>
		/// Возвращает треки из документа
		/// </summary>
		public static IEnumerable<XElement> ToTrk(this XDocument gpx)
		{
			if (gpx?.Root == null)
				throw new ArgumentNullException("gpx");
			return gpx.Root.Elements()
				.Where(x => x.Name.LocalName
					.ToLower()
					.Equals(Trk));
		}


		/// <summary>
		/// Получение сегментов маршрутов транспорта
		/// </summary>
		/// <returns>The segments.</returns>
		/// <param name="trk">Trk.</param>
		public static Dictionary<int, IEnumerable<XElement>> ToSegments(this IEnumerable<XElement> trk)
		{
			if (trk == null)
				throw new ArgumentNullException("trk");
			var result = new Dictionary<int, IEnumerable<XElement>>();
			var i = 0;
			foreach (var elements in trk.Select(item => item.Elements()
				.Where(x => x.Name.LocalName
					.ToLower()
					.Equals(Trkseg))))
			{
				result.Add(i, elements);
				i++;
			}
			return result;
		}			

		/// <summary>
		/// Преобразование элемента XML в точку марщрута
		/// </summary>
		/// <returns>The route point.</returns>
		/// <param name="element">Element.</param>
		private static IRoutePoint ToRoutePoint(this XElement element)
		{
			var result = new RoutePoint();
			var timeValue = element.Elements().FirstOrDefault(x => x.Name.LocalName.Equals("time"));
			var ci = CultureInfo.InvariantCulture;
			if (timeValue != null)
			{
				DateTime time;
				if (DateTime.TryParse(timeValue.Value,ci,DateTimeStyles.AssumeUniversal,out time)){
					result.Time = time;
				}
			}
			var latValue = element.Attributes().FirstOrDefault(x => x.Name.LocalName.Equals("lat"));
			if (latValue != null)
			{
				double lat;
				if (double.TryParse(latValue.Value, NumberStyles.Number, ci, out lat))
				{
					result.Position.Lat = lat;
				}
			}
			var lonValue = element.Attributes().FirstOrDefault(x => x.Name.LocalName.Equals("lon"));
			if (lonValue != null)
			{
				double lon;
				if (double.TryParse(lonValue.Value, NumberStyles.Number, ci, out lon))
				{
					result.Position.Lon = lon;
				}
			}
			var speedValue = element.Elements().FirstOrDefault(x => x.Name.LocalName.Equals("speed"));
			if (speedValue != null)
			{
				double speed;
				if (double.TryParse(speedValue.Value, NumberStyles.Number, ci, out speed))
				{
					result.Speed = speed;
				}
				else
				{
					result.Speed = -1;
				}
			}
			else
			{
				result.Speed = -1;
			}
			return result;

		}

		/// <summary>
		/// Время на начало дня(гавнокод!!!!)
		/// </summary>
		/// <returns>The begin day.</returns>
		/// <param name="time">Time.</param>
		public static DateTime ToBeginDay(this DateTime time)
		{
			return
				time.AddHours(-time.Hour)
					.AddMinutes(-time.Minute)
					.AddSeconds(-time.Second)
					.AddMilliseconds(-time.Millisecond);
		}


		/// <summary>
		///  Возвращает даты полученных треков на основании всех точек
		/// </summary>
		/// <returns>The route dates.</returns>
		/// <param name="dict">Dict.</param>
		public static IEnumerable<DateTime> ToRouteDates(this Dictionary<int, IEnumerable<IEnumerable<IRoutePoint>>> dict)
		{
			if (dict == null)
				throw new ArgumentNullException("dict");
			var result = new List<DateTime>();
			var temp = new List<IRoutePoint>();
			foreach (var item in dict)
			{
				var val = item.Value;
				foreach (var segment in val)
				{
					temp.AddRange(segment);
				}
			}
			var q = temp.Select(x => x.Time.ToBeginDay()).Distinct();
			result.AddRange(q);
			return result;
		}
		/// <summary>
		/// фильтрация данных в сегменте трека по дню
		/// </summary>
		/// <returns>The filtered by day.</returns>
		/// <param name="segment">Segment.</param>
		/// <param name="date">Date.</param>
		private static IEnumerable<IRoutePoint> ToFilteredByDay(this IEnumerable<IRoutePoint> segment, DateTime date)
		{
			var q = segment.Where(x => x.Time.ToBeginDay() == date);
			return q;
		}

		/// <summary>
		/// Tos the route filtered by day.
		/// </summary>
		/// <returns>The route filtered by day.</returns>
		/// <param name="track">Track.</param>
		/// <param name="date">Date.</param>
		private static IEnumerable<IEnumerable<IRoutePoint>> ToRouteFilteredByDay(this IEnumerable<IEnumerable<IRoutePoint>> track, DateTime date)
		{
			if (track == null)
				throw new ArgumentNullException("track");
			return track.Select(item => item.ToFilteredByDay(date)).Where(o => o != null).ToList();
		}

		/// <summary>
		/// Возвращает преобразованную до определенного вида
		/// </summary>
		/// <returns>The route by date.</returns>
		/// <param name="dictionary">Dictionary.</param>
		/// <param name="dates">Dates.</param>
		public static Dictionary<DateTime, IEnumerable<IEnumerable<IEnumerable<IRoutePoint>>>> ToRouteByDate(
				this Dictionary<int, IEnumerable<IEnumerable<IRoutePoint>>> dictionary,
			IEnumerable<DateTime> dates)
		{
			if (dictionary == null)
				throw new ArgumentNullException("dictionary");
			if (dates == null)
				throw new ArgumentNullException("dates");
			var result = new Dictionary<DateTime, IEnumerable<IEnumerable<IEnumerable<IRoutePoint>>>>();
			foreach (var item in dates)
			{
				var key = item;
				var item1 = item;
				var r = dictionary.Select(route => route.Value).Select(track => track.ToRouteFilteredByDay(item1)).ToList();
				result.Add(key, r);
			}
			return result;
		}

		/// <summary>
		/// Возвращает сегменты по дням
		/// </summary>
		/// <returns>The segments by day.</returns>
		/// <param name="sourceDictionary">Source dictionary.</param>
		private static Dictionary<DateTime, IEnumerable<IEnumerable<IRoutePoint>>> ToSegmentsByDay(
			this Dictionary<int, IEnumerable<IEnumerable<IRoutePoint>>> sourceDictionary)
		{
			if (sourceDictionary == null)
				throw new ArgumentNullException("sourceDictionary");
			var result = new Dictionary<DateTime, IEnumerable<IEnumerable<IRoutePoint>>>();
			var dates = sourceDictionary.ToRouteDates();
			var t = sourceDictionary.ToRouteByDate(dates);
			foreach (var elements in t)
			{
				var l = new List<List<IRoutePoint>>();
				var key = elements.Key;
				var value = elements.Value; //все сегменты
				foreach (var item in value)
				{
					l.AddRange(from o in item
						select o as IRoutePoint[] ?? o.ToArray()
						into points
						where points.Count() > 1
						select points.ToList());
				}
				result.Add(key, l);
			}
			return result;
		}

			
		/// <summary>
		///  Получение данных о сегменте маршрута транспорта
		/// </summary>
		/// <returns>The segment header.</returns>
		/// <param name="points">Points.</param>
		public static IRouteElementHeader ToSegmentHeader(this IEnumerable<IRoutePoint> points){
			if (points == null)
				throw new ArgumentNullException ("points");			
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Получение заголовочных данных по маршруту за день на основании данных сегмента
		/// </summary>
		/// <returns>The route header.</returns>
		/// <param name="route">Route.</param>
		public static IRouteElementHeader ToRouteHeader(this IEnumerable<IRouteSegment> segments){
			if (segments == null)
				throw new ArgumentNullException ("segments");
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Получение заголовочных данных по треку на основании данных о маршрутах за день
		/// </summary>
		/// <returns>The track header.</returns>
		/// <param name="routes">Routes.</param>
		public static IRouteElementHeader ToTrackHeader(this IEnumerable<IRoute> routes){
			if (routes == null)
				throw new ArgumentNullException ("routes");
			throw new NotImplementedException ();
		}						
		/// <summary>
		/// Преобразование gpxfiles в требуемый формат для дальнейшей обработки
		/// </summary>
		/// <returns>The track item.</returns>
		/// <param name="track">Track.</param>
		public static ITrack ToTrackItem(this object track){
			if(track == null)
				throw new ArgumentNullException("track");
			if(track.GetType() != typeof(Stream))
				throw new NotSupportedException();
			var result = new Track();
			throw new NotImplementedException ();
			return result;			
		}		
	}

}
