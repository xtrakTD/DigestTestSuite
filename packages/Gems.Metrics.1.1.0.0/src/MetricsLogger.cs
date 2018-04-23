using System.Collections.Generic;
using System.Threading;

namespace Gems.Metrics
{
    using System;
    using System.Configuration;
    using System.Linq;
    using Gems.Metrics.Configuration;

    /// <summary>
    /// Журнал метрик. 
    /// Через адаптеры может использовать разные стратегии сохранения метрик в журнал.
    /// </summary>
    public static class MetricLogger
    {
        /// <summary>
        /// Теги с общей для всех метрик информацией.
        /// </summary>
        private static readonly HashSet<string> DefaultTags = new HashSet<string>();

        /// <summary>
        /// Объект синхронизации инициализации журнала.
        /// </summary>
        private static readonly object InitSync = new object();

        /// <summary>
        /// Объект синхронизации инициализации набора меток.
        /// </summary>
        private static readonly ReaderWriterLockSlim TagsLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Инициализация из конфигурационного файла проводилась?
        /// </summary>
        private static bool isInitiated;

        /// <summary>
        /// Адаптер, используемый по умолчанию. Метрики не будут сохраняться.
        /// </summary>
        private static IMetricLoggerAdapter adapter = new NullMetricLoggerAdapter();
        
        /// <summary>
        /// Регистрация нового адаптера, который будет использоваться для сохранения метрики в журнал.
        /// После регистрации вызывается инициализация адаптера.
        /// </summary>
        /// <param name="newAdapter">Адаптер, используемый для сохранения метрик в журнал.</param>
        public static void SetAdapter(IMetricLoggerAdapter newAdapter)
        {
            adapter = newAdapter;
            adapter.Initialize();
        }

        /// <summary>
        /// Записывает метрику в журнал.
        /// </summary>
        /// <typeparam name="T">Тип значения метрики.</typeparam>
        /// <param name="metricItem">Метрика записываемая в журнал.</param>
        public static void Log<T>(MetricItem<T> metricItem)
        {
            InitFromConfiguration();

            TagsLock.EnterReadLock();
            try
            {
                adapter.Log(new MetricItem<T>(metricItem, DefaultTags));
            }
            finally
            {
                if (TagsLock.IsReadLockHeld)
                {
                    TagsLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Записывает событие в журнал.
        /// </summary>
        /// <param name="eventItem">Событие записываемое в журнал.</param>
        public static void Log(EventItem eventItem)
        {
            InitFromConfiguration();

            TagsLock.EnterReadLock();
            try
            {
                adapter.Log(new EventItem(eventItem, DefaultTags));
            }
            finally
            {
                if (TagsLock.IsReadLockHeld)
                {
                    TagsLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Добавляет тег и значение к списку общих тегов.
        /// </summary>
        /// <param name="key">Ключ тега.</param>
        /// <param name="value">Значение тега.</param>
        public static void AddTag(string key, string value)
        {
            AddTag(key + ":" + value);
        }

        /// <summary>
        /// Добавление метки в список общих меток.
        /// </summary>
        /// <param name="tag">Добавляемая метка.</param>
        public static void AddTag(string tag)
        {
            TagsLock.EnterWriteLock();
            try
            {
                DefaultTags.Add(tag);
            }
            finally
            {
                if (TagsLock.IsWriteLockHeld)
                {
                    TagsLock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Инициализация журнала из конфигурационного файла.
        /// Выполняется только один раз.
        /// </summary>
        private static void InitFromConfiguration()
        {
            if (!isInitiated)
            {
                lock (InitSync)
                {
                    if (!isInitiated)
                    {
                        var section = ConfigurationManager.GetSection("metrics") as MetricsConfigurationSection;
                        if (section != null)
                        {
                            Type factoryType = Type.GetType(section.AdapterFactory);
                            if (factoryType != null)
                            {
                                var factory = Activator.CreateInstance(factoryType) as IMetricLoggerAdapterFactory;
                                if (factory != null)
                                {
                                    IMetricLoggerAdapter createdAdapter =
                                        factory.Create(
                                            section.FactoryParameters.Elements.ToDictionary(
                                                element => element.Key,
                                                element => element.Value));
                                    SetAdapter(createdAdapter);
                                }
                            }
                        }

                        isInitiated = true;
                    }
                }
            }
        }
    }
}
