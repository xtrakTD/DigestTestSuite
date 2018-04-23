using System.Collections.Generic;
using System.Linq;

namespace Gems.Metrics
{
    /// <summary>
    /// Элемент метрик.
    /// </summary>
    /// <typeparam name="T">Тип значения метрики.</typeparam>
    public class MetricItem<T>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MetricItem{T}"/>. 
        /// </summary>
        /// <param name="metricType">
        /// Тип метрики.
        /// </param>
        /// <param name="statName">
        /// Имя метрики.
        /// </param>
        /// <param name="value">
        /// Значение метрики.
        /// </param>
        public MetricItem(MetricType metricType, string statName, T value)
        {
            this.MetricType = metricType;
            this.StatName = statName;
            this.Value = value;
            this.SampleRate = 1.0;
            this.Tags = null;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MetricItem{T}"/>. 
        /// </summary>
        /// <param name="item">Экземпляр, к которому надо добавить метки.</param>
        /// <param name="defaultTags">Метки по умолчанию.</param>
        public MetricItem(MetricItem<T> item, HashSet<string> defaultTags)
        {
            this.MetricType = item.MetricType;
            this.StatName = item.StatName;
            this.Value = item.Value;
            this.SampleRate = 1.0;
            this.Tags = this.ConcatTags(item.Tags, defaultTags);
        }

        /// <summary>
        /// Тип метрики.
        /// </summary>
        public MetricType MetricType { get; private set; }

        /// <summary>
        /// Имя метрики.
        /// </summary>
        public string StatName { get; private set; }

        /// <summary>
        /// Значение метрики.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Чтобы сократить количество сохраняемых метрик используется доля выборки в процентах.
        /// Будет сохраняться только указанный процент метрик, но реальное значение будет автоматически рассчитано с учетом доли выборки.
        /// </summary>
        public double SampleRate { get; set; }

        /// <summary>
        /// Возможность снабдить метрики дополнительной информацией, для удобного анализа - метками.
        /// Не все журналы метрик поддерживают метки.
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// Объединение двух массивов меток в один.
        /// </summary>
        /// <param name="tags">Метки конкретной метрики или события.</param>
        /// <param name="defaultTags">Общие метки.</param>
        /// <returns>Общий массив меток.</returns>
        private string[] ConcatTags(string[] tags, ICollection<string> defaultTags)
        {
            if (tags == null && defaultTags == null)
            {
                return new string[0];
            }

            if (tags == null)
            {
                return defaultTags.ToArray();
            }

            if (defaultTags == null)
            {
                return tags;
            }

            string[] allTags = new string[tags.Length + defaultTags.Count];

            defaultTags.CopyTo(allTags, 0);
            tags.CopyTo(allTags, defaultTags.Count);

            return allTags;
        }
    }
}