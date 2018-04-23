namespace Gems.Metrics
{
    /// <summary>
    /// Адаптер к конкретной реализации журнала метрик.
    /// </summary>
    public interface IMetricLoggerAdapter
    {
        /// <summary>
        /// Инициализирует адаптер.
        /// Адаптер должен сам гарантировать безопасность вызова инициализации в нескольких потоках.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Записывает в журнал метрику.
        /// </summary>
        /// <typeparam name="T">Тип значения метрики.</typeparam>
        /// <param name="item">Метрика записываемая в журнал.</param>
        void Log<T>(MetricItem<T> item);

        /// <summary>
        /// Записывает в журнал событие.
        /// </summary>
        /// <param name="item">Событие записываемое в журнал.</param>
        void Log(EventItem item);
    }
}