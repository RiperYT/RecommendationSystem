using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsMath.Services.Abstractions
{
    internal interface ILikeStaticService
    {
        public void LikeUserPost(int user_id, int post_id);
    }
}
