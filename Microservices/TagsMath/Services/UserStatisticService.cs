using TagsMath.Common;
using TagsMath.Data.Entities;
using TagsMath.Data.Abstractions;
using TagsMath.Services.Abstractions;
using TagsMath.Data.Repositories;

namespace TagsMath.Services
{
    internal class UserStatisticService : IUserStatisticService
    {
        private int maxValue = TagLikeMathRules.MaxValueCount;
        private int minValue = TagLikeMathRules.MinValueCount;
        private int percent = TagLikeMathRules.PercentOfStandart;
        private IUser_stat_TagRepository _user_Stat_TagRepository;
        public UserStatisticService(
                                    IUser_stat_TagRepository user_Stat_TagRepository) 
        {
            _user_Stat_TagRepository = user_Stat_TagRepository;
        }

        public Dictionary<int, int> CountStats(Dictionary<int, int> userTags, Dictionary<int, int> postTags, List<int> postStandartTags)
        {
            var totalPost = new Dictionary<int, int>();

            foreach (var tag in postTags)
                totalPost.Add(tag.Key, (int) Math.Round((float) tag.Value / 100 * (100 - percent)));

            foreach(var tag in postStandartTags)
                if (totalPost.ContainsKey(tag))
                    totalPost[tag] += ((int)Math.Round((float) maxValue / 100 * percent));
                else
                    totalPost.Add(tag, (int)Math.Round((float) maxValue / 100 * percent));

            var answer = new Dictionary<int, int>();

            foreach (var tag in userTags)
                answer.Add(tag.Key, tag.Value - 1);

            foreach (var tag in totalPost)
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

        public void UpdateStats(int user_id, Dictionary<int, int> userTagsOld, Dictionary<int, int> userTagsNew)
        {
            foreach (var tag in userTagsOld)
                if (userTagsNew.ContainsKey(tag.Key))
                {
                    var stat = new User_stat_Tag();
                    stat.user_id = user_id;
                    stat.tag_id = tag.Key;
                    _user_Stat_TagRepository = new User_stat_TagRepository(new Data.DataContext());
                    var user_Stat = _user_Stat_TagRepository.GetUser_stat_TagByIds(user_id, tag.Key);
                    if (user_Stat != null)
                    {
                        stat.tag_count = user_Stat.tag_count + (userTagsNew[tag.Key] - userTagsOld[tag.Key]);
                        _user_Stat_TagRepository.Update(stat);
                        _user_Stat_TagRepository.SaveChanges();
                    }
                    else if (userTagsNew[tag.Key] - userTagsOld[tag.Key] >= minValue)
                    {
                        stat.tag_count = userTagsNew[tag.Key] - userTagsOld[tag.Key];
                        _user_Stat_TagRepository.Add(stat);
                        _user_Stat_TagRepository.SaveChanges();
                    }
                }
                else
                {
                    var stat = new User_stat_Tag();
                    stat.user_id = user_id;
                    stat.tag_id = tag.Key;
                    stat.tag_count = userTagsOld[tag.Key];
                    _user_Stat_TagRepository.Remove(stat);
                    _user_Stat_TagRepository.SaveChanges();
                }
        }
    }
}