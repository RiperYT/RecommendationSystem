using System.Text.Json;

namespace TagLikeMath.Common
{
    internal class TagLikeMathRules
    {
        public static int MinValueCount { get; private set; }
        public static int MaxValueCount { get; private set; }
        public static int PercentOfStandard { get; private set; }
        private static TagLikeMathRules? instance;

        private TagLikeMathRules() { }

        public static TagLikeMathRules getInstance(string json)
        {
            if (instance == null)
            {
                instance = new TagLikeMathRules();
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
        }
        private class ForJson
        {
            public int MinValueCount { get; set; }
            public int MaxValueCount { get; set; }
            public int PercentOfStandard { get; set; }
        }
    }
}
