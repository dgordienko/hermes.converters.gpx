using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;

namespace hermes.gpx.converters
{
	
	/// <summary>
	/// Делегат событий разбора данных в формате gpx
	/// </summary>
	public delegate void HermesGpxFileConverterEvent(object sender,HermesGpxFileConverterArg arg);


	/// <summary>
	/// Базовый интерфейс конвертора данных в формате gpx
	/// </summary>
	public interface IHermesGpxFileConverter
	{		
		/// <summary>
		/// Событие завершающее операцию разбора данных в формате gpx
		/// </summary>
		event HermesGpxFileConverterEvent Parced;
		/// <summary>
		/// Выполнение разбора сырых данных 
		/// </summary>
		/// <param name="parcesstrategy">Parcesstrategy.</param>
		void Parce(Func<object,ITrack> parcesstrategy);

	}

}

