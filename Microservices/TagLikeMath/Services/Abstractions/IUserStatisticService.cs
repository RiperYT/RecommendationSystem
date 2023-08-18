namespace TagLikeMath.Services.Abstractions
{
    internal interface IUserStatisticService
    {
        public Dictionary<int, int> CountStats(Dictionary<int, int> userTags, Dictionary<int, int> postTags, List<int> postStandartTags);
        public void UpdateStats(int user_id, Dictionary<int, int> userTagsOld, Dictionary<int, int> userTagsNew);
    }
}
