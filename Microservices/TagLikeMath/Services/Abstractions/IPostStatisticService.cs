namespace TagLikeMath.Services.Abstractions
{
    internal interface IPostStatisticService
    {
        public Dictionary<int, int> CountStats(Dictionary<int, int> userTags, Dictionary<int, int> postTags, List<int> userStandartTags);
        public void UpdateStats(int post_id, Dictionary<int, int> postTagsOld, Dictionary<int, int> postTagsNew);
    }
}
