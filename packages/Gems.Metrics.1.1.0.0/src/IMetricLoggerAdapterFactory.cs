namespace Gems.Metrics
{
    using System.Collections.Generic;

    /// <summary>
    /// Фабрика создающая адаптер.
    /// </summary>
    public interface IMetricLoggerAdapterFactory
    {
        /// <summary>
        /// Создает адаптер.
        /// </summary>
        /// <param name="parameters">
        /// Параметры конфигурации адаптера.
        /// </param>
        /// <returns>
        /// Созданный адаптер.
        /// </returns>
        IMetricLoggerAdapter Create(IDictionary<string, string> parameters);
    }
}