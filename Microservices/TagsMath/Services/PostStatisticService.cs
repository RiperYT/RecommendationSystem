using TagsMath.Common;
using TagsMath.Data.Entities;
using TagsMath.Data.Abstractions;
using TagsMath.Services.Abstractions;
using TagsMath.Data.Repositories;

namespace TagsMath.Services
{
    internal class PostStatisticService : IPostStatisticService
    {
        private int maxValue = TagLikeMathRules.MaxValueCount;
        private int minValue = TagLikeMathRules.MinValueCount;
        private int percent = TagLikeMathRules.PercentOfStandart;
        private IPost_stat_TagRepository _post_Stat_TagRepository;
        public PostStatisticService(
                                    IPost_stat_TagRepository post_Stat_Tag)
        {
            _post_Stat_TagRepository = post_Stat_Tag;
        }

        public Dictionary<int, int> CountStats(Dictionary<int, int> userTags, Dictionary<int, int> postTags, List<int> userStandartTags)
        {
            var totalUser = new Dictionary<int, int>();

            foreach (var tag in userTags)
                totalUser.Add(tag.Key, (int)Math.Round((float)tag.Value / 100 * (100 - percent)));

            foreach (var tag in userStandartTags)
                if (totalUser.ContainsKey(tag))
                    totalUser[tag] += ((int)Math.Round((float) maxValue / 100 * percent));
                else
                    totalUser.Add(tag, (int)Math.Round((float) maxValue / 100 * percent));

            var answer = new Dictionary<int, int>();

            foreach (var tag in postTags)
                answer.Add(tag.Key, tag.Value - 1);

            foreach (var tag in totalUser)
                if (answer.ContainsKey(tag.Key))
                    answer[tag.Key] += 2;
                else
                    answer.Add(tag.Key, minValue + 1);

            foreach (var tag in answer)
                if (tag.Value > maxValue)
                    foreach (var tag2 in answer)
                        answer[tag2.Key]--;

            foreach (var tag in answer)
                if (tag.Value < minValue)
                    answer.Remove(tag.Key);

            return answer;
        }

        public void UpdateStats(int post_id, Dictionary<int, int> postTagsOld, Dictionary<int, int> postTagsNew)
        {
            foreach (var tag in postTagsOld)
                if (postTagsNew.ContainsKey(tag.Key))
                {
                    var stat = new Post_stat_Tag();
                    stat.post_id = post_id;
                    stat.tag_id = tag.Key;
                    _post_Stat_TagRepository = new Post_stat_TagRepository(new Data.DataContext());
                    var post_Stat = _post_Stat_TagRepository.GetPost_stat_TagByIds(post_id, tag.Key);
                    if (post_Stat != null)
                    {
                        stat.tag_count = post_Stat.tag_count + (postTagsNew[tag.Key] - postTagsOld[tag.Key]);
                        _post_Stat_TagRepository.Update(stat);
                        _post_Stat_TagRepository.SaveChanges();
                    }
                    else if (postTagsNew[tag.Key] - postTagsOld[tag.Key] >= minValue)
                    {
                        stat.tag_count = postTagsNew[tag.Key] - postTagsOld[tag.Key];
                        _post_Stat_TagRepository.Add(stat);
                        _post_Stat_TagRepository.SaveChanges();
                    }
                    //stat.tag_count = postTagsNew[tag.Key];
                    //_post_Stat_TagRepository.Update(stat);
                }
                else
                {
                    var stat = new Post_stat_Tag();
                    stat.post_id = post_id;
                    stat.tag_id = tag.Key;
                    stat.tag_count = postTagsOld[tag.Key];
                    _post_Stat_TagRepository.Remove(stat);
                    _post_Stat_TagRepository.SaveChanges();
                }
        }
    }
}
