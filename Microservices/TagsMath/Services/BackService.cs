using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using TagsMath.Common;
using TagsMath.Data;
using TagsMath.Data.Abstractions;
using TagsMath.Services.Abstractions;

namespace TagsMath.Services
{
    internal sealed class BackServices : BackgroundService
    {
        private object locker;
        private int activeProcessCount;
        private readonly int processCount;
        private IModel channel;
        private readonly IServiceProvider _serviceProvider;
        private ILikeStaticService _likeStaticService;
        public BackServices(/*ILikeStaticService likeStaticService,*/ IServiceProvider serviceProvider)
        {
            locker = new();
            activeProcessCount = 0;
            processCount = Environment.ProcessorCount;
            //_likeStaticService = likeStaticService;
            _serviceProvider = serviceProvider;

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
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
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

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
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


                /*IServiceScope scope = _serviceProvider.CreateScope();
                IPostStatisticService s1 = scope.ServiceProvider.GetRequiredService<IPostStatisticService>();
                IUserStatisticService s2 = scope.ServiceProvider.GetRequiredService<IUserStatisticService>();
                IPost_stat_TagRepository s3 = scope.ServiceProvider.GetRequiredService<IPost_stat_TagRepository>();
                IUser_stat_TagRepository s4 = scope.ServiceProvider.GetRequiredService<IUser_stat_TagRepository>();
                IPost_standard_TagRepository s5 = scope.ServiceProvider.GetRequiredService<IPost_standard_TagRepository>();
                IUser_standard_TagRepository s6 = scope.ServiceProvider.GetRequiredService<IUser_standard_TagRepository>();
                //DataContext dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                ILikeStaticService scopedProcessingService =
                    scope.ServiceProvider.GetRequiredService<ILikeStaticService>();
                //ILikeStaticService scopedProcessingService = new LikeStaticService(s1, s2, s3, s4, s5, s6);
                scopedProcessingService.LikeUserPost(forJson.user_id, forJson.post_id);*/
                using (var scope = _serviceProvider.CreateScope())
                {
                    var services = scope.ServiceProvider.GetRequiredService<ILikeStaticService>;
                    services.Invoke().LikeUserPost(forJson.user_id, forJson.post_id);
                }
                //_likeStaticService = _serviceProvider.GetService<ILikeStaticService>();
                //_likeStaticService.LikeUserPost(forJson.user_id, forJson.post_id);
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        private class ForJson
        {
            public int user_id { get; set; }
            public int post_id { get; set; }
        }
    }
}
