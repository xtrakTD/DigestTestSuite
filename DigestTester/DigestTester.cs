using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Gems.ServiceBus;
using Gems.ServiceBus.Configurator;
using Gems.ServiceBus.Sending;

using Common.Logging;

namespace DigestTester
{
    class DigestTester
    {
        private static ILog log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                ShowUsage();
                return;
            }

            try
            {
                var exchange_letters = string.Empty;

                var configurator = new AppConfigConfigurator();
                var bus = new BusFactory().Create(
                    cfg =>
                    {
                        configurator.Configure("Letters.Sender", cfg);
                        exchange_letters = configurator.GetEvent("Letters.Sender", "letter.send");
                    });

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                var girls = ConfigurationManager.AppSettings["lady-user-ids"].Split(',');
                IList<long> girlUserIds = new List<long>();

                foreach(var girl in girls)
                {
                    girlUserIds.Add(Convert.ToInt64(girl));
                }

                if (args.Length > 1)
                {
                    for(int i = 1; i < args.Length; i++)
                    {
                        girlUserIds.Add(Convert.ToInt64(args[i]));
                    }
                }

                int total_letter_count = girlUserIds.Count * (Convert.ToInt32(ConfigurationManager.AppSettings["letter-count-each-lady"].ToString()));
                log.Debug(m => m($"Пытаемся отправить [{total_letter_count.ToString()}] писем"));
        
                for(int i = 0; i < Convert.ToInt32(ConfigurationManager.AppSettings["letter-count-each-lady"].ToString()); i++)
                {
                    foreach(var girl in girlUserIds)
                    {
                        var letter_request = new SendLetterRequest()
                        {
                            FromId = girl,
                            ToId = Convert.ToInt64(args[0]),
                            Type = i == 0 ? "FirstReply" : "Common",
                            Body = $"Hi there. this is a [{(i == 0 ? "FirstReply" : "Common")}] letter that you should see in a digest notification if all goes well.... " + Environment.NewLine + Regex.Replace(Guid.NewGuid().ToString(), @"\d+", "")
                        };

                        log.Info(m => m($"Сгенерирован запрос на отправку письма: [{letter_request.ToString()}]"));
                        try
                        {
                            bus.Request<SendLetterRequest, SendLetterResponse>(
                                exchange_letters,
                                letter_request,
                                letter_response =>
                                    {
                                        log.Info(m => m($"Ответ на команду отправки письма: [Status: [{letter_response.Status}], ErrorMessage: [{letter_response.ErrorMessage}]]"));
                                    });
                        }
                        catch(Exception e)
                        {
                            log.Error(m => m(e.ToString()));
                        }
                    }
                }
                bus.Dispose();
            }
            catch(Exception e)
            {
                log.Error(m => m(e.ToString()));                
            }
        }

        public static void ShowUsage()
        {
            Console.WriteLine("Usage: OneLetterSender.exe {ReceiverId} [{SenderID}[]]");
            Console.WriteLine("Example: OneLetterSender.exe 10035013209");
            Console.WriteLine("Example: OneLetterSender.exe 10035013209 2000038254 2001188254");
            Console.ReadKey();
        }
    }
}
