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
