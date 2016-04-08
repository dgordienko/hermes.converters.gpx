using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;

namespace Hermes.Api.Clasess.Geo
{
	/// <summary>
	/// Географическая позиция
	/// </summary>
	public sealed class Position:IPosition
	{
		#region IPosition implementation
		/// <summary>
		/// Широта
		/// </summary>
		/// <value>The lat.</value>
		public double Lat {
			get;
			set;
		}
		/// <summary>
		/// Долгота
		/// </summary>
		/// <value>The lon.</value>
		public double Lon {
			get;
			set;
		}
		/// <summary>
		/// Высота
		/// </summary>
		/// <value>The ele.</value>
		public double Ele {
			get;
			set;
		}
		#endregion			
	}


}

