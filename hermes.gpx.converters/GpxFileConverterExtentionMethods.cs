using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;
using Hermes.Api.Clasess.Geo;

namespace hermes.gpx.converters
{
	/// <summary>
	/// Методы рсширения для конвертера маршрутов автотраспорта
	/// </summary>
	public static class GpxFileConverterExtentionMethods
	{
		/// <summary>
		///  Получение данных о сегменте маршрута транспорта
		/// </summary>
		/// <returns>The segment header.</returns>
		/// <param name="points">Points.</param>
		public static IRouteElementHeader ToSegmentHeader(this IEnumerable<IRoutePoint> points){
			if (points == null)
				throw new ArgumentNullException ("points");
			var result = new RouteElementHeader ();

			return result;
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
			return result;			
		}		
	}
}
