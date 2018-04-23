namespace Gems.Metrics.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Конфигурационная секция для настроек адаптеров метрик производительности.
    /// </summary>
    public class MetricsConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Имя типа фабрики адаптера.
        /// </summary>
        [ConfigurationProperty("adapterFactory", IsRequired = true)]
        public string AdapterFactory
        {
            get
            {
                return (string)this["adapterFactory"];
            }

            set
            {
                this["adapterFactory"] = value;
            }
        }
        
        /// <summary>
        /// Параметры адаптера журнала метрик производительности.
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public MetricsElementCollection FactoryParameters
        {
            get
            {
                return (MetricsElementCollection)base[string.Empty];
            }
        }
    }
}