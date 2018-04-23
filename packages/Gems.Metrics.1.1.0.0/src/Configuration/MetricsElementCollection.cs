namespace Gems.Metrics.Configuration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// Конфигурационная секция, которая позволяет задать фабрику создающую адаптер.
    /// </summary>
    public sealed class MetricsElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Возвращает параметры фабрики, необходимые для создания адаптера.
        /// </summary>
        public IEnumerable<AddElement> Elements
        {
            get
            {
                return this.OfType<AddElement>();
            }
        }

        /// <summary>
        /// Получает конфигурационный элемент из коллекции по индексу.
        /// </summary>
        /// <param name="i">Индекс конфигурационного элемента в коллекции.</param>
        /// <returns>Конфигурационный элемент из коллекции.</returns>
        public AddElement this[int i]
        {
            get
            {
                return (AddElement)this.BaseGet(i);
            }
        }

        /// <summary>
        /// Создает конфигурационный элемент.
        /// </summary>
        /// <returns>Конфигурационный элемент.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AddElement();
        }

        /// <summary>
        /// Получает ключ конфигурационного элемента.
        /// </summary>
        /// <param name="element">Конфигурационный элемент с ключом.</param>
        /// <returns>Ключ конфигурационного элемента.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AddElement)element).Key;
        }

        /// <summary>
        /// Конфигурационный элемент, для передачи параметров в фабрику адаптера.
        /// </summary>
        public sealed class AddElement : ConfigurationElement
        {
            /// <summary>
            /// Ключ параметра фабрики адаптеров.
            /// </summary>
            [ConfigurationPropertyAttribute("key", IsRequired = true)]
            public string Key
            {
                get
                {
                    return (string)this["key"];
                }

                set
                {
                    this["key"] = value;
                }
            }

            /// <summary>
            /// Значение параметра фабрики адаптеров.
            /// </summary>
            [ConfigurationPropertyAttribute("value", IsRequired = true)]
            public string Value
            {
                get
                {
                    return (string)this["value"];
                }

                set
                {
                    this["value"] = value;
                }
            }
        }
    }
}