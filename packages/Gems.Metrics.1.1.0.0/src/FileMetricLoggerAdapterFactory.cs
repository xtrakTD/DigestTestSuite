namespace Gems.Metrics
{
    using System.Collections.Generic;

    /// <summary>
    /// Создает экземпляр фабрики адаптера журнала метрик в файл на диске.
    /// </summary>
    public class FileMetricLoggerAdapterFactory : IMetricLoggerAdapterFactory
    {
        /// <summary>
        /// Создает экземпляр адаптера.
        /// </summary>
        /// <param name="parameters">Параметры инициализации адаптера.</param>
        /// <returns>Созданный экземпляр адаптера.</returns>
        public IMetricLoggerAdapter Create(IDictionary<string, string> parameters)
        {
            return new FileMetricLoggerAdapter();
        }
    }
}