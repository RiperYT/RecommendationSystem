using System.Text.Json;
using System.Text;
using TagLikeMath.Common;
using TagLikeMath.Services.Abstractions;

using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using TagLikeMath.Data.Abstractions;
using TagLikeMath.Data;
using TagLikeMath.Data.Repositories;
using TagLikeMath.Data.Entities;

namespace TagLikeMath.Services
{
    public class BackService
    {
        private object locker;
        private string queuename;
        private int activeProcessCount;
        private readonly int processCount;
        private IModel channel;
        private ILikeStatisticService _likeStaticService;
        public BackService()
        {
            locker = new();
            activeProcessCount = 0;
            queuename = ConnectionRules.QueueLike;
            processCount = Environment.ProcessorCount;

            //_likeStaticService = likeStaticService;

            var factory = new ConnectionFactory();
            factory.HostName = ConnectionRules.RabbitMQHostName;
            factory.Port = ConnectionRules.RabbitMQPort;
            factory.UserName = ConnectionRules.RabbitMQUserName;
            factory.Password = ConnectionRules.RabbitMQPassword;

            _likeStaticService = CreateLikeService();

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
                    BasicGetResult result = channel.BasicGet(queuename, autoAck: false);
                    if (result != null)
                    {
                        channel.BasicAck(result.DeliveryTag, multiple: false);
                        byte[] body = result.Body.ToArray();
                        string message = Encoding.UTF8.GetString(body);

                        _ = Task.Factory.StartNew(rabbitMQReader, message);

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

                IUserRepository rep = new UserRepository(new DataContext());
                User user = rep.GetUserById(forJson.user_id);
                if (user == null)
                    throw new ArgumentNullException("User is not correct");
                if (!user.password.Equals(forJson.password))
                    throw new ArgumentNullException("Password is not correct");

                CreateLikeService().LikeUserPost(forJson.user_id, forJson.post_id);
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
            public string password { get; set; }
            public int post_id { get; set; }
        }

        private ILikeStatisticService CreateLikeService()
        {
            IPost_stat_TagRepository i1 = new Post_stat_TagRepository(new DataContext());
            IUser_stat_TagRepository i2 = new User_stat_TagRepository(new DataContext());
            IPost_standard_TagRepository i3 = new Post_standard_TagRepository(new DataContext());
            IUser_standard_TagRepository i4 = new User_standard_TagRepository(new DataContext());
            IPost_stat_TagRepository i5 = new Post_stat_TagRepository(new DataContext());
            IUser_stat_TagRepository i6 = new User_stat_TagRepository(new DataContext());
            IPostStatisticService i7 = new PostStatisticService(i5);
            IUserStatisticService i8 = new UserStatisticService(i6);
            return new LikeStatisticService(i7, i8, i1, i2, i3, i4);
        }
    }
}
