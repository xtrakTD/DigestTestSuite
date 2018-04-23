using System;

using Gems.ServiceBus.Receiving;
using Gems.ServiceBus.Receiving.Consumers;

namespace Gems.ServiceBus.Operators
{
    /// <summary>
    /// ������������ ������. 
    /// ��������� ���������� ������������� ���������� �� ������ ���������������� ��������� �� ������ ����������.
    /// </summary>
    /// <typeparam name="T">��� ������������ �������� ����������.</typeparam>
    public class DynamicFilter<T> : Filter
        where T : class
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="DynamicFilter{T}"/>. 
        /// </summary>
        /// <param name="routeFilter">������� ���������� ������������ ������� �������������.</param>
        /// <param name="storage">��������� ������ �������������.</param>
        public DynamicFilter(Func<IMessage, IKeyValueStorage<T>, bool> routeFilter, IKeyValueStorage<T> storage)
            : base(message => routeFilter(message, storage))
        {
        }

        /// <summary>
        /// ���������� ����������� ���������.
        /// </summary>
        /// <typeparam name="TV">��� ������� �������������.</typeparam>
        public class DynamicFilterControlConsumer<TV> : IConsumerOf<T>
            where TV : class
        {
            private readonly Action<IMessage, IKeyValueStorage<TV>> createRoute;

            private readonly IKeyValueStorage<TV> storage;

            /// <summary>
            /// �������������� ����� ��������� ������ <see cref="DynamicFilterControlConsumer{TV}"/>. 
            /// </summary>
            /// <param name="createRoute">������� ���������� ������� �������������.</param>
            /// <param name="storage">��������� ������ �������������.</param>
            public DynamicFilterControlConsumer(Action<IMessage, IKeyValueStorage<TV>> createRoute, IKeyValueStorage<TV> storage)
            {
                this.createRoute = createRoute;
                this.storage = storage;
            }

            /// <summary>
            /// ������������ ����������� ��������� � ����� ������������� ������ �������������. 
            /// </summary>
            /// <param name="context">�������� ����������� ���������.</param>
            public void Handle(IConsumingContext<T> context)
            {
                this.createRoute(context.Message, this.storage);
                context.Accept();
            }
        }
    }
}