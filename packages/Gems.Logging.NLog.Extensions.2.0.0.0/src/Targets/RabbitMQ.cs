using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using RabbitMQ.Client;

namespace Gems.Logging.NLog.Extensions.Targets
{
	[Target("RabbitMQ")]
	public class RabbitMq : TargetWithLayout
	{
		private class Message
		{
			public string Payload;
			public string Topic;
			public string Exchange;
		}

		private Task _task;
		private readonly ConcurrentQueue<Message> _messageQueue = new ConcurrentQueue<Message>();
		private readonly TaskFactory _taskFactory = new TaskFactory();

		public RabbitMq()
		{
			RetryCount = 4;
			RetryDelay = TimeSpan.FromMilliseconds(500);
			DeliveryMode = 2;
			MaxMessageCount = 1000;
		}

		protected override void Write(LogEventInfo logEvent)
		{
			if (_messageQueue.Count >= MaxMessageCount)
			{
				InternalLogger.Info(string.Format("Сообщение не будет обработано [{0}]", Layout.Render(logEvent)));

				return;
			}
			_messageQueue.Enqueue(new Message
				{
					Payload = Layout.Render(logEvent),
					Topic = Topic.Render(logEvent),
					Exchange = Exchange.Render(logEvent)
				});


			CreateWorker();
		}

		[RequiredParameter]
		public Layout Topic { get; set; }

		[RequiredParameter]
		public Layout Exchange { get; set; }

		[RequiredParameter]
		public string ConnectionString { get; set; }

		public bool Durable { get; set; }

		[DefaultValue("4")]
		public int RetryCount { get; set; }

		[DefaultValue("00:00:00.500")]
		public TimeSpan RetryDelay { get; set; }

		[DefaultValue("2")]
		public byte DeliveryMode { get; set; }

		[DefaultValue(10000)]
		public int MaxMessageCount { get; set; }

		private void CreateWorker()
		{
			try
			{
				if (IsAlive(_task))
				{
					return;
				}

				lock (SyncRoot)
				{
					if (IsAlive(_task))
					{
						return;
					}

					_task = _taskFactory.StartNew(Consume);
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex.ToString());
			}
		}

		private static bool IsAlive(Task task)
		{
			return task != null && !(task.IsCanceled || task.IsCompleted || task.IsFaulted);
		}

		private void Consume()
		{
			try
			{
				var factory = new ConnectionFactory
				{
					Uri = ConnectionString
				};

				using (var connection = factory.CreateConnection())
				using (var channel = connection.CreateModel())
				{

					var tryCount = 0;
					while (tryCount < RetryCount)
					{
						Message message;

						while (_messageQueue.TryDequeue(out message))
						{
							tryCount = 0;

							var properties = channel.CreateBasicProperties();

							properties.ContentEncoding = Encoding.UTF8.WebName;

							properties.ContentType = "application/json";

							properties.DeliveryMode = DeliveryMode;

							channel.ExchangeDeclare(message.Exchange, ExchangeType.Topic, Durable);

							var messageBodyBytes = Encoding.UTF8.GetBytes(message.Payload);

							channel.BasicPublish(message.Exchange, message.Topic, properties, messageBodyBytes);
						}

						tryCount++;

						Thread.Sleep((int)RetryDelay.TotalMilliseconds);
					}

					channel.Close();
					connection.Close();
				}

				InternalLogger.Info("Закончена обработка очереди");
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex.ToString());
			}
		}
	}
}
