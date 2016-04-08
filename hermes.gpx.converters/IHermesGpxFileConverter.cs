using System;
using Distributor.Api.Core;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;

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
		/// Выполнение разбора "сырых данных" переданных в объекте Stream c помощью алгоритма Action<T>, возвращающего результат вида ITrack
		/// </summary>
		/// <param name="parcesstrategy">Parcesstrategy.</param>
		void Parce(Func<Stream,Action<Type>,ITrack> parcesstrategy);

	}

}

