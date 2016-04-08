using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;

namespace Hermes.Api.Clasess.Geo
{

	/// <summary>
	/// Общий обработанный маршрут 
	/// </summary>
	public sealed class Track:ITrack
	{
		#region ITrack implementation
		/// <summary>
		/// Заголовок маршрута траспортного средства
		/// </summary>
		/// <value>The header.</value>
		public IRouteElementHeader Header {
			get;
			set;
		}

		/// <summary>
		/// Маршруты траспортного средства по дням
		/// </summary>
		/// <value>The routes.</value>
		public IEnumerable<IRoute> Routes {
			get;
			set;
		}
		#endregion			
	}
}
