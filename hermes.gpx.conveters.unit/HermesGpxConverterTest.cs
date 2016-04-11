using NUnit.Framework;
using System;
using Rhino.Mocks;

using Distributor.Api.Core;
using Hermes.Api.Clasess;
using System.Collections.Generic;
using System.Linq;
using hermes.gpx.converters;

namespace hermes.gpx.conveters.unit
{
	[TestFixture (Description="Тестирование контроллера обработки маршрутов автотранспорта")]
	public class HermesGpxConverterTest
	{
		/// <summary>
		/// Набор точек сегметов первого маршрута
		/// </summary>
		private IEnumerable<IRoutePoint> points0;

		/// <summary>
		/// Набор точек сегментов второго маршрута
		/// </summary>
		private IEnumerable <IRoutePoint> points1;

		/// <summary>
		/// Сегмент первого маршрута
		/// </summary>
		private IRouteSegment Segment0;

		/// <summary>
		/// Сегмент второго маршрута
		/// </summary>
		private IRouteSegment Segment1;

		/// <summary>
		/// Vfhihen
		/// </summary>
		private IRoute Route;

		/// <summary>
		/// Трек транспортного средства
		/// </summary>
		private ITrack Track;
		[SetUp]
		public void Init(){
			points0 = new List<IRoutePoint> ();
			points1 = new List<IRoutePoint> ();
			var rnd = new Random ();
			var date = DateTime.Today;
			for (int i = 0; i < 100; i++) {
				var point = MockRepository.GenerateStub<IRoutePoint> ();
				var position = MockRepository.GenerateStub<IPosition> ();
				position.Lat = 53.23 + rnd.NextDouble ();
				position.Lon = 30.34 + rnd.NextDouble ();
				point.Position = position;
				point.Speed = i * rnd.NextDouble () * 1000 / 3600;
				point.Time = date.AddMinutes (i);
				((List<IRoutePoint>)points0).Add (point);
			}
			date = date.AddMinutes (120);
			for (int i = 0; i < 100; i++) {
				var point = MockRepository.GenerateStub<IRoutePoint> ();
				var position = MockRepository.GenerateStub<IPosition> ();
				position.Lat = 53.45 + rnd.NextDouble ();
				position.Lon = 30.46 + rnd.NextDouble ();
				point.Position = position;
				point.Speed = i * rnd.NextDouble () * 1000 / 3600;
				point.Time = date.AddMinutes (i);
				((List<IRoutePoint>)points1).Add (point);
			}
			Segment0 = MockRepository.GenerateStub<IRouteSegment> ();
			Segment0.Points = points0;
			Segment1 = MockRepository.GenerateStub<IRouteSegment> ();
			Segment1.Points = points1;
			Route = MockRepository.GenerateStub<IRoute> ();
			Route.Segments = new List<IRouteSegment> ();
			((List<IRouteSegment>)Route.Segments).Add (Segment0);
			((List<IRouteSegment>)Route.Segments).Add (Segment1);
			Track = MockRepository.GenerateStub<ITrack> ();
			Track.Routes = new List<IRoute> ();
			((List<IRoute>)Track.Routes).Add (Route);
		}


		[Test (Description="Тестирование методов расшрения для элементов маршрута автотранспорта")]
		public void ExtMethodsTestCase ()
		{
			Assert.IsNotNull (points0);
			Console.WriteLine (string.Format ("Точек в первом сегменте {0}",points0.Count()));
			Assert.IsNotNull (points1);
			Console.WriteLine (string.Format ("Точек во втором сегменте {0}",points1.Count()));
			Assert.IsNotNull (Segment0);
			Assert.IsNotNull (Segment1);
			Assert.IsNotNull (Route);
			Assert.IsNotNull (Track);
			//Должно выполнится без ошибки преобразование в загловок маршрута
			Assert.DoesNotThrow (() => 
				{
					var header = points0.ToSegmentHeader();
					Assert.That(header != null);
				});

		}
	}
}

