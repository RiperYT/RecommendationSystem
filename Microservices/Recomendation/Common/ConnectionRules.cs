﻿using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Recomendation.Common
{
    public class ConnectionRules
    {
        public static string PostgreConncection { get; private set; }
        public static string RabbitMQHostName { get; private set; }
        public static int RabbitMQPort { get; private set; }
        public static string RabbitMQUserName { get; private set; }
        public static string RabbitMQPassword { get; private set; }
        public static string QueueStartup { get; private set; }
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
            if (string.IsNullOrEmpty(forJson.PostgreConncection))
                throw new Exception("PostgreConncection is null");
            if (string.IsNullOrEmpty(forJson.RabbitMQHostName))
                throw new Exception("RabbitMQHostName is null");
            if (string.IsNullOrEmpty(forJson.PostgreConncection))
                throw new Exception("PostgreConncection is null");
            if (string.IsNullOrEmpty(forJson.RabbitMQUserName))
                throw new Exception("RabbitMRabbitMQUserNameQHostName is null");
            if (string.IsNullOrEmpty(forJson.RabbitMQPassword))
                throw new Exception("RabbitMQPassword is null");
            if (string.IsNullOrEmpty(forJson.QueueStartup))
                throw new Exception("QueueName is null");
            PostgreConncection = forJson.PostgreConncection;
            RabbitMQHostName = forJson.RabbitMQHostName;
            RabbitMQPort = forJson.RabbitMQPort;
            RabbitMQUserName = forJson.RabbitMQUserName;
            RabbitMQPassword = forJson.RabbitMQPassword;
            QueueStartup = forJson.QueueStartup;
        }
        private class ForJson
        {
            public string PostgreConncection { get; set; }
            public string RabbitMQHostName { get; set; }
            public int RabbitMQPort { get; set; }
            public string RabbitMQUserName { get; set; }
            public string RabbitMQPassword { get; set; }
            public string QueueStartup { get; set; }
        }
    }
}
