using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;

namespace Hermes.Api.Clasess.Geo
{

	/// <summary>
	/// Сегмент маршрута автотраспорта
	/// </summary>
	public sealed class Segment:IRouteSegment
	{
		#region IRouteSegment implementation

		/// <summary>
		/// Заголовок элеемнта маршрута
		/// </summary>
		/// <value>The header.</value>
		public IRouteElementHeader Header {
			get;
			set;
		}
		/// <summary>
		/// Набор точек, формарующих маршрут движения
		/// </summary>
		/// <value>The points.</value>
		public IEnumerable<IRoutePoint> Points {
			get;
			set;
		}
		#endregion			
	}


}
