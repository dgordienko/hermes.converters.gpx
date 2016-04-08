using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;

namespace Hermes.Api.Clasess.Geo
{
	
	/// <summary>
	/// Загловок элементов маршрутов
	/// </summary>
	public sealed class RouteElementHeader:IRouteElementHeader
	{
		#region IRouteElementHeader implementation
		/// <summary>
		/// Дистанция
		/// </summary>
		/// <value>The distance.</value>
		public double Distance {
			get;
			set;
		}
		/// <summary>
		/// Длительность
		/// </summary>
		/// <value>The duration.</value>
		public TimeSpan Duration {
			get;
			set;
		}
		/// <summary>
		/// Начало движения
		/// </summary>
		/// <value>The star time.</value>
		public DateTime StarTime {
			get;
			set;
		}
		/// <summary>
		/// Окончание движения
		/// </summary>
		/// <value>The stop time.</value>
		public DateTime StopTime {
			get;
			set;
		}
		#endregion			
	}
}
