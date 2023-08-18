using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagLikeMath.Services.Abstractions
{
    internal interface ILikeStatisticService
    {
        public void LikeUserPost(int user_id, int post_id);
    }
}
