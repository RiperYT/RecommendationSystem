using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TagStartupMath.Common;
using TagStartupMath.Data.Abstractions;
using TagStartupMath.Data.Repositories;
using TagStartupMath.Data;
using TagStartupMath.Services.Abstractions;

namespace TagStartupMath.Services
{
    internal class BackService
    {
        private object locker;
        private string queuename;
        private int activeProcessCount;
        private readonly int processCount;
        private IModel channel;
        private IStartupStatisticMathService _startupStatisticMathService;
        public BackService()
        {
            locker = new();
            activeProcessCount = 0;
            queuename = ConnectionRules.QueueStartup;
            processCount = Environment.ProcessorCount;
            var factory = new ConnectionFactory();
            factory.HostName = ConnectionRules.RabbitMQHostName;
            factory.Port = ConnectionRules.RabbitMQPort;
            factory.UserName = ConnectionRules.RabbitMQUserName;
            factory.Password = ConnectionRules.RabbitMQPassword;

            _startupStatisticMathService = CreateStartupStatisticMathService();

            var connection = factory.CreateConnection();
            channel = connection.CreateModel();
            // Оголошуємо чергу
            channel.QueueDeclare(queue: queuename,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public Task Start()
        {
            while (true)
            {
                if (activeProcessCount < processCount)
                {
                    // Отримуємо одне повідомлення з черги
                    BasicGetResult result = channel.BasicGet(queuename, autoAck: false);
                    if (result != null)
                    {
                        channel.BasicAck(result.DeliveryTag, multiple: false);
                        // Обробляємо отримане повідомлення
                        byte[] body = result.Body.ToArray();
                        string message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received message: {0}", message);

                        _ = Task.Factory.StartNew(rabbitMQReader, message);
                        Console.WriteLine(activeProcessCount);

                    }
                }
            }
        }

        private Task rabbitMQReader(object json)
        {
            plusProcess();
            try
            {
                string str = (string)json;

                ForJson? forJson = JsonSerializer.Deserialize<ForJson>(str);
                if (forJson == null)
                    throw new ArgumentNullException("Json is not correct or empty");

                CreateStartupStatisticMathService().StatisticStartup(forJson.startup_id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            minusProcess();
            return Task.CompletedTask;
        }

        private void minusProcess()
        {
            lock (locker)
            {
                activeProcessCount--;
            }
        }
        private void plusProcess()
        {
            lock (locker)
            {
                activeProcessCount++;
            }
        }

        private class ForJson
        {
            public int startup_id { get; set; }
        }

        private IStartupStatisticMathService CreateStartupStatisticMathService()
        {
            IPost_stat_TagRepository i1 = new Post_stat_TagRepository(new DataContext());
            IPost_standard_TagRepository i2 = new Post_standard_TagRepository(new DataContext());
            IStartup_stat_TagRepository i3 = new Startup_stat_TagRepository(new DataContext());
            IStartup_standard_TagRepository i4 = new Startup_standard_TagRepository(new DataContext());
            IPost_owner_StartupRepository i5 = new Post_owner_StartupRepository(new DataContext());
            return new StartupStatisticMathService(i1, i2, i3, i4, i5);
        }
    }
}
