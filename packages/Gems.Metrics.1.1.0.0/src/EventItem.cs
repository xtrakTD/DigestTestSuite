using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gems.Metrics
{
    /// <summary>
    /// Элемент событий.
    /// </summary>
    public class EventItem
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EventItem"/>. 
        /// </summary>
        /// <param name="title">Заголовок события.</param>
        /// <param name="text">Текст события.</param>
        /// <param name="alertLevel">Уровень оповещения о событии.</param>
        /// <param name="priority">Приоритет события.</param>
        /// <param name="tags">Метки для дополнительной информации.</param>
        /// <param name="hostname">Имя хоста.</param>
        /// <param name="sourceTypeName">Тип отправителя.</param>
        /// <param name="aggregationKey">Ключ для объединения с другими событиями.</param>
        public EventItem(string title, string text, AlertLevel alertLevel, PriorityLevel priority, string[] tags = null, string hostname = "", string sourceTypeName = "", string aggregationKey = "")
        {
            this.Title = title;
            this.Text = text;
            this.AlertLevel = alertLevel;
            this.Priority = priority;
            this.Tags = tags;
            this.Hostname = hostname;
            this.SourceTypeName = sourceTypeName;
            this.AggregationKey = aggregationKey;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EventItem"/> с добавлением меток по умолчанию. 
        /// </summary>
        /// <param name="item">Экземпляр, к которому надо добавить метки.</param>
        /// <param name="defaultTags">Метки по умолчанию.</param>
        public EventItem(EventItem item, ICollection<string> defaultTags)
        {
            this.Title = item.Title;
            this.Text = item.Text;
            this.AlertLevel = item.AlertLevel;
            this.Priority = item.Priority;
            this.Hostname = item.Hostname;
            this.SourceTypeName = item.SourceTypeName;
            this.AggregationKey = item.AggregationKey;
            this.Tags = this.ConcatTags(item.Tags, defaultTags);
        }

        /// <summary>
        /// Заголовок события.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Текст события.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Уровень оповещения о событии.
        /// </summary>
        public AlertLevel AlertLevel { get; private set; }

        /// <summary>
        /// Приоритет события.
        /// </summary>
        public PriorityLevel Priority { get; private set; }

        /// <summary>
        /// Метки для дополнительной информации.
        /// </summary>
        public string[] Tags { get; private set; }

        /// <summary>
        /// Имя хоста.
        /// </summary>
        public string Hostname { get; private set; }

        /// <summary>
        /// Тип отправителя.
        /// </summary>
        public string SourceTypeName { get; private set; }

        /// <summary>
        /// Ключ для объединения с другими событиями.
        /// </summary>
        public string AggregationKey { get; private set; }

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
