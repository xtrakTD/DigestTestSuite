namespace Gems.Metrics
{
    /// <summary>
    /// Множество возможных уровней оповещения о событии.
    /// </summary>
    public enum AlertLevel
    {
        /// <summary>
        /// Информационное сообщение
        /// </summary>
        Info = 0,

        /// <summary>
        /// Оповещение с предупреждением
        /// </summary>
        Warning = 1,

        /// <summary>
        /// Оповещение об ошибке
        /// </summary>
        Error = 2,

        /// <summary>
        /// Сообщение об успешном событии
        /// </summary>
        Success = 3
    }
}
