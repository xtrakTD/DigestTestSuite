namespace Gems.Metrics
{
    /// <summary>
    /// Вспомогательный класс для удобного создания метрик.
    /// </summary>
    public static class Metric
    {
        /// <summary>
        /// Создает метрику <c>MetricType.Counter</c>.
        /// </summary>
        /// <typeparam name="T">Тип значения метрики.</typeparam>
        /// <param name="statName">Имя метрики.</param>
        /// <param name="value">Значение метрики.</param>
        /// <param name="sampleRate">Доля выборки.</param>
        /// <param name="tags">Метки метрики.</param>
        public static void Counter<T>(string statName, T value, double sampleRate = 1.0, string[] tags = null)
        {
            MetricLogger.Log(new MetricItem<T>(MetricType.Counter, statName, value) { SampleRate = sampleRate, Tags = tags });
        }

        /// <summary>
        /// Создает метрику <c>MetricType.Increment</c>.
        /// </summary>
        /// <param name="statName">Имя метрики.</param>
        /// <param name="sampleRate">Доля выборки.</param>
        /// <param name="tags">Метки метрики.</param>
        public static void Increment(string statName, double sampleRate = 1.0, string[] tags = null)
        {
            MetricLogger.Log(new MetricItem<int>(MetricType.Increment, statName, 0) { SampleRate = sampleRate, Tags = tags });
        }

        /// <summary>
        /// Создает метрику <c>MetricType.Decrement</c>.
        /// </summary>
        /// <param name="statName">Имя метрики.</param>
        /// <param name="sampleRate">Доля выборки.</param>
        /// <param name="tags">Метки метрики.</param>
        public static void Decrement(string statName, double sampleRate = 1.0, string[] tags = null)
        {
            MetricLogger.Log(new MetricItem<int>(MetricType.Decrement, statName, 0) { SampleRate = sampleRate, Tags = tags });
        }

        /// <summary>
        /// Создает метрику <c>MetricType.Gauge</c>.
        /// </summary>
        /// <typeparam name="T">Тип значения метрики.</typeparam>
        /// <param name="statName">Имя метрики.</param>
        /// <param name="value">Значение метрики.</param>
        /// <param name="sampleRate">Доля выборки.</param>
        /// <param name="tags">Метки метрики.</param>
        public static void Gauge<T>(string statName, T value, double sampleRate = 1.0, string[] tags = null)
        {
            MetricLogger.Log(new MetricItem<T>(MetricType.Gauge, statName, value) { SampleRate = sampleRate, Tags = tags });
        }

        /// <summary>
        /// Создает метрику <c>MetricType.Histogram</c>.
        /// </summary>
        /// <typeparam name="T">Тип значения метрики.</typeparam>
        /// <param name="statName">Имя метрики.</param>
        /// <param name="value">Значение метрики.</param>
        /// <param name="sampleRate">Доля выборки.</param>
        /// <param name="tags">Метки метрики.</param>
        public static void Histogram<T>(string statName, T value, double sampleRate = 1.0, string[] tags = null)
        {
            MetricLogger.Log(new MetricItem<T>(MetricType.Histogram, statName, value) { SampleRate = sampleRate, Tags = tags });
        }

        /// <summary>
        /// Создает метрику <c>MetricType.Set</c>.
        /// </summary>
        /// <typeparam name="T">Тип значения метрики.</typeparam>
        /// <param name="statName">Имя метрики.</param>
        /// <param name="value">Значение метрики.</param>
        /// <param name="sampleRate">Доля выборки.</param>
        /// <param name="tags">Метки метрики.</param>
        public static void Set<T>(string statName, T value, double sampleRate = 1.0, string[] tags = null)
        {
            MetricLogger.Log(new MetricItem<T>(MetricType.Set, statName, value) { SampleRate = sampleRate, Tags = tags });
        }

        /// <summary>
        /// Создает метрику <c>MetricType.Timer</c>.
        /// </summary>
        /// <typeparam name="T">Тип значения метрики.</typeparam>
        /// <param name="statName">Имя метрики.</param>
        /// <param name="value">Значение метрики.</param>
        /// <param name="sampleRate">Доля выборки.</param>
        /// <param name="tags">Метки метрики.</param>
        public static void Timer<T>(string statName, T value, double sampleRate = 1.0, string[] tags = null)
        {
            MetricLogger.Log(new MetricItem<T>(MetricType.Timer, statName, value) { SampleRate = sampleRate, Tags = tags });
        }

        /// <summary>
        /// Создаёт событие <c>Event</c>. 
        /// </summary>
        /// <param name="title">Заголовок события.</param>
        /// <param name="text">Текст события.</param>
        /// <param name="alertLevel">Уровень оповещения о событии.</param>
        /// <param name="priority">Приоритет события.</param>
        /// <param name="tags">Метки для дополнительной информации.</param>
        /// <param name="hostname">Имя хоста.</param>
        /// <param name="sourceTypeName">Тип отправителя.</param>
        /// <param name="aggregationKey">Ключ для объединения с другими событиями.</param>
        public static void Event(string title, string text, AlertLevel alertLevel, PriorityLevel priority, string[] tags = null, string hostname = "", string sourceTypeName = "", string aggregationKey = "")
        {
            MetricLogger.Log(new EventItem(title, text, alertLevel, priority, tags, hostname, sourceTypeName, aggregationKey));
        }
    }
}