using System;

namespace Gems.Metrics
{
    using Common.Logging;

    /// <summary>
    /// Записывает метрики в файл на диске.
    /// </summary>
    public class FileMetricLoggerAdapter : IMetricLoggerAdapter
    {
        /// <summary>
        /// Журнал адаптера.
        /// </summary>
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует адаптер.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Записывает метрику в журнал.
        /// </summary>
        /// <typeparam name="T">Тип значения метрики.</typeparam>
        /// <param name="item">Метрика, записываемая в журнал.</param>
        public void Log<T>(MetricItem<T> item)
        {
            Logger.Trace(m => m("[{0}] [{1}] [{2}] [{3}] [{4}]", item.MetricType, item.StatName, item.Value, string.Join(";", item.Tags ?? new string[0]), item.SampleRate));
        }

        /// <summary>
        /// Записывает событие в журнал.
        /// </summary>
        /// <param name="item">Событие, записываемое в журнал.</param>
        public void Log(EventItem item)
        {
            Logger.Trace(m => m("[{0}] [{1}] [{2}] [{3}] [{4}] [{5}] [{6}] [{7}]", item.Title, item.Text, item.AlertLevel, item.Priority, string.Join(";", item.Tags ?? new string[0]), item.Hostname, item.SourceTypeName, item.AggregationKey));
        }
    }
}