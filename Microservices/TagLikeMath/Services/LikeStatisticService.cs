using TagLikeMath.Data.Abstractions;
using TagLikeMath.Services.Abstractions;

namespace TagLikeMath.Services
{
    internal class LikeStatisticService : ILikeStatisticService
    {
        private IPostStatisticService _postStatisticService;
        private IUserStatisticService _userStatisticService;
        private IPost_stat_TagRepository _post_Stat_TagRepository;
        private IUser_stat_TagRepository _user_Stat_TagRepository;
        private IPost_standard_TagRepository _post_Standard_TagRepository;
        private IUser_standard_TagRepository _user_Standard_TagRepository;
        public LikeStatisticService(IPostStatisticService postStatisticService,
                                 IUserStatisticService userStatisticService,
                                 IPost_stat_TagRepository post_Stat_Tag,
                                 IUser_stat_TagRepository user_Stat_TagRepository,
                                 IPost_standard_TagRepository post_Standard_TagRepository,
                                 IUser_standard_TagRepository user_Standard_TagRepository)
        {
            _postStatisticService = postStatisticService;
            _userStatisticService = userStatisticService;
            _post_Stat_TagRepository = post_Stat_Tag;
            _user_Stat_TagRepository = user_Stat_TagRepository;
            _post_Standard_TagRepository = post_Standard_TagRepository;
            _user_Standard_TagRepository = user_Standard_TagRepository;
        }

        public void LikeUserPost(int user_id, int post_id)
        {
            var userTagsQ = _user_Stat_TagRepository.GetUser_stat_TagByUserId(user_id).ToList();
            var postTagsQ = _post_Stat_TagRepository.GetPost_stat_TagByPostId(post_id).ToList();
            var userStandardTagsQ = _user_Standard_TagRepository.GetUser_standard_TagByUserId(user_id).ToList();
            var postStandardTagsQ = _post_Standard_TagRepository.GetPost_standard_TagByPostId(post_id).ToList();

            var userTags = new Dictionary<int, int>();
            var postTags = new Dictionary<int, int>();
            var userStandardTags = new List<int>();
            var postStandardTags = new List <int>();

            foreach (var tag in userTagsQ)
                userTags.Add(tag.tag_id, tag.tag_count);

            foreach (var tag in postTagsQ)
                postTags.Add(tag.tag_id, tag.tag_count);

            foreach (var tag in userStandardTagsQ)
                userStandardTags.Add(tag.tag_id);

            foreach (var tag in postStandardTagsQ)
                postStandardTags.Add(tag.tag_id);

            var userNewTags = new Dictionary<int, int>();
            var postNewTags = new Dictionary<int, int>();

            var tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() => userNewTags = _userStatisticService.CountStats(userTags, postTags, postStandardTags));
            tasks[1] = Task.Factory.StartNew(() => postNewTags = _postStatisticService.CountStats(userTags, postTags, userStandardTags));

            Task.WaitAll(tasks);

            tasks[0] = Task.Factory.StartNew(() => _userStatisticService.UpdateStats(user_id, userTags, userNewTags));
            tasks[0] = Task.Factory.StartNew(() => _postStatisticService.UpdateStats(post_id, postTags, postNewTags));
        }
    }
}
