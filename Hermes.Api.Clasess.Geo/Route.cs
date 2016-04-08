using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;

namespace Hermes.Api.Clasess.Geo
{

	/// <summary>
	/// Маршрут(сутки) транспортного средства
	/// </summary>
	public sealed class Route:IRoute
	{
		#region IRoute implementation
		/// <summary>
		/// Заголовок маршрута траспортного средства
		/// </summary>
		/// <value>The header.</value>
		public IRouteElementHeader Header {
			get;
			set;
		}
		/// <summary>
		/// Сегменты маршрута транспортного средства
		/// </summary>
		/// <value>The segments.</value>
		public IEnumerable<IRouteSegment> Segments {
			get;
			set;
		}
		#endregion
	}

}
