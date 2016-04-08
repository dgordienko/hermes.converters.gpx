using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;

namespace Hermes.Api.Clasess.Geo
{

	/// <summary>
	/// Точка маршрута автотраспорта
	/// </summary>
	public sealed class RoutePoint:IRoutePoint
	{
		#region IRoutePoint implementation
		/// <summary>
		/// Географическая позиция
		/// </summary>
		/// <value>The position.</value>
		public IPosition Position {
			get;
			set;
		}
		/// <summary>
		/// Зафиксированная скорость движения
		/// </summary>
		/// <value>The speed.</value>
		public double Speed {
			get;
			set;
		}
		/// <summary>
		/// Время фиксации координат
		/// </summary>
		/// <value>The time.</value>
		public DateTime Time {
			get;
			set;
		}
		#endregion			
	}



}
