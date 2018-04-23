﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangeBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The exchange builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gems.ServiceBus.Transport.RabbitMQ.Topology
{
    using global::RabbitMQ.Client;

    /// <summary>
    /// The exchange builder.
    /// </summary>
    public class ExchangeBuilder
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ExchangeBuilder"/>.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        internal ExchangeBuilder(string name)
        {
            this.Instance = new Exchange(name);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the auto delete.
        /// </summary>
        public ExchangeBuilder AutoDelete
        {
            get
            {
                this.Instance.AutoDelete = true;
                return this;
            }
        }

        /// <summary>
        /// Gets the direct.
        /// </summary>
        public ExchangeBuilder Direct
        {
            get
            {
                this.Instance.Type = ExchangeType.Direct;
                return this;
            }
        }

        /// <summary>
        /// Gets the durable.
        /// </summary>
        public ExchangeBuilder Durable
        {
            get
            {
                this.Instance.Durable = true;
                return this;
            }
        }

        /// <summary>
        /// Gets the fanout.
        /// </summary>
        public ExchangeBuilder Fanout
        {
            get
            {
                this.Instance.Type = ExchangeType.Fanout;
                return this;
            }
        }

        /// <summary>
        /// Gets the topic.
        /// </summary>
        public ExchangeBuilder Topic
        {
            get
            {
                this.Instance.Type = ExchangeType.Topic;
                return this;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        internal Exchange Instance { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The of type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="ExchangeBuilder"/>.
        /// </returns>
        public ExchangeBuilder OfType(string type)
        {
            this.Instance.Type = type;
            return this;
        }

        #endregion
    }
}