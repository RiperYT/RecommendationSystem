using Microsoft.Extensions.Hosting;
using Recomendation.Common;
using Recomendation.Data.Abstractions;
using Recomendation.Services.Abstractions;

namespace Recomendation.Services
{
    public class PostRecomendationsService : IPostRecomendationsService
    {
        private readonly int minValue = TagStartupMathRules.MinValueCount;
        private readonly int maxValue = TagStartupMathRules.MaxValueCount;
        private readonly int percentOfStandard = TagStartupMathRules.PercentOfStandard;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        public PostRecomendationsService(IUserRepository userRepository, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public List<int> GetLimitRecomendation(int user_id, int limit)
        {
            var result = new Dictionary<int, int>();
            
            var posts_id = _postRepository.GetUserPostsIDsSubscrription(user_id);

            var user_stat = GetUserStat(user_id);

            foreach (var post in posts_id)
            {
                var percentOfInteresting = _postRepository.GetPercentInterestingInOwning(post, user_id);
                var score = 0;
                if (percentOfInteresting > 0)
                {
                    var post_stat = GetPostStat(post);
                    score = GetScore(user_stat, post_stat, percentOfInteresting);
                }
                result.Add(post, score);
            }

            var sortedResult = result.OrderBy(post => post.Value).Take(limit).Select(post => post.Key).ToList();
            if (sortedResult == null)
                sortedResult = new List<int>();

            return sortedResult;
        }

        private Dictionary<int, int> GetUserStat(int user_id)
        {
            var user_stat = _userRepository.GetStatTagsOfUser(user_id);
            foreach (var stat in user_stat)
                user_stat[stat.Key] = (int)Math.Round((float)stat.Value / 100 * (100 - percentOfStandard));

            var user_standard = _userRepository.GetStandardTagsOfUser(user_id);
            foreach (var tag in user_standard)
                if (user_stat.ContainsKey(tag))
                    user_stat[tag] += (int)Math.Round((float)maxValue / 100 * percentOfStandard);
                else
                    user_stat.Add(tag, (int)Math.Round((float)maxValue / 100 * percentOfStandard));

            return user_stat;
        }

        private Dictionary<int, int> GetPostStat(int post_id)
        {
            var post_stat = _postRepository.GetStatTagsOfPost(post_id);
            foreach (var stat in post_stat)
                post_stat[stat.Key] = (int)Math.Round((float)stat.Value / 100 * (100 - percentOfStandard));

            var post_standard = _postRepository.GetStandardTagsOfPost(post_id);
            foreach (var tag in post_standard)
                if (post_stat.ContainsKey(tag))
                    post_stat[tag] += (int)Math.Round((float)maxValue / 100 * percentOfStandard);
                else
                    post_stat.Add(tag, (int)Math.Round((float)maxValue / 100 * percentOfStandard));

            return post_stat;
        }

        private int GetScore(Dictionary<int, int> user_stat, Dictionary<int, int> post_stat, int percentOfInteresting)
        {
            var score = 0;
            foreach (var stat in user_stat)
                if (post_stat.ContainsKey(stat.Key))
                    score += Math.Abs(stat.Value - post_stat[stat.Key]);
                else
                    score += stat.Value;
            return score * (100 - percentOfInteresting);
        }

    }
}
