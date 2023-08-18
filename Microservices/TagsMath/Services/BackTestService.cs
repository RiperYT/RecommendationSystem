using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TagsMath.Common;
using TagsMath.Data.Abstractions;
using TagsMath.Services.Abstractions;

namespace TagsMath.Services
{
    internal class BackTestService : IBackTestService
    {
        private object locker;
        private int activeProcessCount;
        private readonly int processCount;
        private IModel channel;
        private readonly ILikeStaticService _likeStaticService;
        public BackTestService(ILikeStaticService likeStaticService)
        {
            locker = new();
            activeProcessCount = 0;
            processCount = Environment.ProcessorCount;
            //_likeStaticService = new LikeStaticService();

            var factory = new ConnectionFactory();
            factory.HostName = ConnectionRules.RabbitMQHostName;
            factory.Port = ConnectionRules.RabbitMQPort;
            factory.UserName = ConnectionRules.RabbitMQUserName;
            factory.Password = ConnectionRules.RabbitMQPassword;
            var connection = factory.CreateConnection();
            channel = connection.CreateModel();
            // Оголошуємо чергу
            channel.QueueDeclare(queue: ConnectionRules.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            while (true)
            {
                if (activeProcessCount < processCount)
                {
                    // Отримуємо одне повідомлення з черги
                    BasicGetResult result = channel.BasicGet(ConnectionRules.QueueName, autoAck: false);
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

                _likeStaticService.LikeUserPost(forJson.user_id, forJson.post_id);
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
            public int user_id { get; set; }
            public int post_id { get; set; }
        }
    }
}
