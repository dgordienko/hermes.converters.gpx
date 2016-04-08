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

	public class PointsComparer : IEqualityComparer<IRoutePoint>
	{

		public bool Equals(IRoutePoint x, IRoutePoint y)
		{
			if(x == null)
				throw new ArgumentNullException("x");
			if (y == null)
				throw new ArgumentNullException("y");
			if (x.Position == null) return false;
			if (y.Position == null) return false;
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			return (x.Position.Lat == y.Position.Lat) & (x.Position.Lon == y.Position.Lon);            
		}

		public int GetHashCode(IRoutePoint obj)
		{
			if(obj == null)
				throw new ArgumentNullException("obj");
			return (int) (Math.Pow(obj.Position.Lat, obj.Position.Lon));
		}
	}
}
