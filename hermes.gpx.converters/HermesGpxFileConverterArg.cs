using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;

namespace hermes.gpx.converters
{
	
	/// <summary>
	/// Аргумент событий парсера данных в формате gpx
	/// </summary>
	public sealed class HermesGpxFileConverterArg
	{
		public ITrack Result { get; set;}
		public Exception Exception { get; set;}
	}
	
}
