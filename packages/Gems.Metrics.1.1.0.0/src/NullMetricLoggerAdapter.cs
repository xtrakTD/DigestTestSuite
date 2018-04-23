namespace Gems.Metrics
{
    /// <summary>
    /// Адаптер к журналу, который используется по умолчанию.
    /// </summary>
    public class NullMetricLoggerAdapter : IMetricLoggerAdapter
    {
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
        /// <param name="item">Метрика записываемая в журнал.</param>
        public void Log<T>(MetricItem<T> item)
        {
        }

        /// <summary>
        /// Записывает в журнал событие.
        /// </summary>
        /// <param name="item">Событие записываемое в журнал.</param>
        public void Log(EventItem item)
        {
        }
    }
}