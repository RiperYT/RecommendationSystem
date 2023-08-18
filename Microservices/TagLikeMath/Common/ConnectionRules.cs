using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace TagLikeMath.Common
{
    internal class ConnectionRules
    {
        public static string PostgreConncection { get; private set; }
        public static string RabbitMQHostName { get; private set; }
        public static int RabbitMQPort { get; private set; }
        public static string RabbitMQUserName { get; private set; }
        public static string RabbitMQPassword { get; private set; }
        public static string QueueLike { get; private set; }
        private static ConnectionRules? instance;

        private ConnectionRules() { }

        public static ConnectionRules getInstance(string json)
        {
            if (instance == null)
            {
                instance = new ConnectionRules();
                JsonToRules(json);
            }
            return instance;
        }

        private static void JsonToRules(string json)
        {
            ForJson? forJson = JsonSerializer.Deserialize<ForJson>(json);
            if (forJson == null)
                throw new Exception("Json rules format is wrong");
            if (forJson.PostgreConncection.IsNullOrEmpty())
                throw new Exception("PostgreConncection is null");
            if (forJson.RabbitMQHostName.IsNullOrEmpty())
                throw new Exception("RabbitMQHostName is null");
            if (forJson.PostgreConncection.IsNullOrEmpty())
                throw new Exception("PostgreConncection is null");
            if (forJson.RabbitMQUserName.IsNullOrEmpty())
                throw new Exception("RabbitMRabbitMQUserNameQHostName is null");
            if (forJson.RabbitMQPassword.IsNullOrEmpty())
                throw new Exception("RabbitMQPassword is null");
            if (forJson.QueueLike.IsNullOrEmpty())
                throw new Exception("QueueName is null");
            PostgreConncection = forJson.PostgreConncection;
            RabbitMQHostName = forJson.RabbitMQHostName;
            RabbitMQPort = forJson.RabbitMQPort;
            RabbitMQUserName = forJson.RabbitMQUserName;
            RabbitMQPassword = forJson.RabbitMQPassword;
            QueueLike = forJson.QueueLike;
        }
        private class ForJson
        {
            public string PostgreConncection { get; set; }
            public string RabbitMQHostName { get; set; }
            public int RabbitMQPort { get; set; }
            public string RabbitMQUserName { get; set; }
            public string RabbitMQPassword { get; set; }
            public string QueueLike { get; set; }
        }
    }
}
