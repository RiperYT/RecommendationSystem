using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagStartupMath.Common;
using TagStartupMath.Data.Abstractions;
using TagStartupMath.Data.Entities;
using TagStartupMath.Data.Repositories;
using TagStartupMath.Services.Abstractions;

namespace TagStartupMath.Services
{
    internal class StartupStatisticMathService : IStartupStatisticMathService
    {
        private readonly int minValue = TagStartupMathRules.MinValueCount;
        private readonly int maxValue = TagStartupMathRules.MaxValueCount;
        private readonly int percentOfStandard = TagStartupMathRules.PercentOfStandard;
        private readonly int percentOfStandartStartup = TagStartupMathRules.PercentOfStandartStartup;
        private IPost_stat_TagRepository _post_stat_TagRepository;
        private IPost_standard_TagRepository _post_standard_TagRepository;
        private IStartup_stat_TagRepository _startup_stat_TagRepository;
        private IStartup_standard_TagRepository _startup_standard_TagRepository;
        private IPost_owner_StartupRepository _post_owner_StartupRepository;

        public StartupStatisticMathService(IPost_stat_TagRepository post_stat_TagRepository,
                                           IPost_standard_TagRepository post_standard_TagRepository,
                                           IStartup_stat_TagRepository startup_stat_TagRepository,
                                           IStartup_standard_TagRepository startup_standard_TagRepository,
                                           IPost_owner_StartupRepository post_owner_StartupRepository)
        {
            _post_stat_TagRepository = post_stat_TagRepository;
            _post_standard_TagRepository = post_standard_TagRepository;
            _startup_stat_TagRepository = startup_stat_TagRepository;
            _startup_standard_TagRepository = startup_standard_TagRepository;
            _post_owner_StartupRepository = post_owner_StartupRepository;
        }

        public void StatisticStartup(int startup_id)
        {
            var posts = _post_owner_StartupRepository.GetPost_owner_StartupByStartupId(startup_id).ToList();

            var posts_id = new List<int>();
            foreach (var post in posts)
                posts_id.Add(post.post_id);

            var standard_startup_tags = _startup_standard_TagRepository.GetStartup_standard_TagByStartupId(startup_id).ToList();

            var newtags = new Dictionary<int, int>();

            foreach (var tag in standard_startup_tags)
                newtags.Add(tag.tag_id, percentOfStandartStartup);

            if (posts_id.Count > 0)
            {
                foreach (var post_id in posts_id)
                {

                    var stat_tags = _post_stat_TagRepository.GetPost_stat_TagByPostId(post_id).ToList();
                    var standard_tags = _post_standard_TagRepository.GetPost_standard_TagByPostId(post_id).ToList();

                    var tags = new Dictionary<int, int>();

                    foreach (var tag in stat_tags)
                        tags.Add(tag.tag_id, (int)Math.Round((float)tag.tag_count / 100 * (100 - percentOfStandard)));

                    foreach (var tag in standard_tags)
                        if (tags.ContainsKey(tag.tag_id))
                            tags[tag.tag_id] += ((int)Math.Round((float)maxValue / 100 * (100 - percentOfStandartStartup)));
                        else
                            tags.Add(tag.tag_id, ((int)Math.Round((float)maxValue / 100 * (100 - percentOfStandartStartup))));

                    foreach (var tag in tags)
                        if (newtags.ContainsKey(tag.Key))
                            newtags[tag.Key] += ((int)Math.Round((float)tags[tag.Key] / 100 * (100 - percentOfStandartStartup)));
                        else
                            newtags.Add(tag.Key, ((int)Math.Round((float)tags[tag.Key] / 100 * (100 - percentOfStandartStartup))));
                }

                foreach (var tag in newtags)
                    if (newtags[tag.Key] >= minValue)
                        newtags[tag.Key] = (int)Math.Round((float)newtags[tag.Key] / posts_id.Count);
                    else
                        newtags.Remove(tag.Key);

                var nowtags = _startup_stat_TagRepository.GetStartup_stat_TagByStartupId(startup_id).ToList();

                foreach (var tag in nowtags)
                    try
                    {
                        if (newtags.ContainsKey(tag.tag_id))
                        {
                            tag.tag_count = newtags[tag.tag_id];
                            _startup_stat_TagRepository.Update(tag);
                            _startup_stat_TagRepository.SaveChanges();
                        }
                        else
                        {
                            _startup_stat_TagRepository.Remove(tag);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                foreach (var tag in newtags)
                    try
                    {
                        if (nowtags.Where(x => x.tag_id == tag.Key).ToList().Count == 0)
                        {
                            var new_tag = new Startup_stat_Tag();
                            new_tag.startup_id = startup_id;
                            new_tag.tag_id = tag.Key;
                            new_tag.tag_count = newtags[tag.Key];
                            _startup_stat_TagRepository.Add(new_tag);
                            _startup_stat_TagRepository.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            }
            else
            {
                var nowtags = _startup_stat_TagRepository.GetStartup_stat_TagByStartupId(startup_id);
                foreach (var tag in nowtags)
                {
                    try
                    {
                        _startup_stat_TagRepository.Remove(tag);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
