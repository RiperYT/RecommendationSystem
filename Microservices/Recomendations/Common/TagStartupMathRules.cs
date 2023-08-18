using System.Text.Json;

namespace Recomendations.Common
{
    public class TagStartupMathRules
    {
        public static int MinValueCount { get; private set; }
        public static int MaxValueCount { get; private set; }
        public static int PercentOfStandard { get; private set; }
        public static int PercentOfStandartStartup { get; private set; }

        private static TagStartupMathRules? instance;

        private TagStartupMathRules() { }

        public static TagStartupMathRules getInstance(string json)
        {
            if (instance == null)
            {
                instance = new TagStartupMathRules();
                JsonToRules(json);
            }
            return instance;
        }

        private static void JsonToRules(string json)
        {
            ForJson? forJson = JsonSerializer.Deserialize<ForJson>(json);
            if (forJson == null)
                throw new Exception("Json rules format is wrong");
            if (forJson.PercentOfStandard < 0 || forJson.PercentOfStandard > 100)
                throw new Exception("Percents are worng (<0 || >100)");
            MinValueCount = forJson.MinValueCount;
            MaxValueCount = forJson.MaxValueCount;
            PercentOfStandard = forJson.PercentOfStandard;
            PercentOfStandartStartup = forJson.PercentOfStandartStartup;
        }
        private class ForJson
        {
            public int MinValueCount { get; set; }
            public int MaxValueCount { get; set; }
            public int PercentOfStandard { get; set; }
            public int PercentOfStandartStartup { get; set; }
        }
    }
}
