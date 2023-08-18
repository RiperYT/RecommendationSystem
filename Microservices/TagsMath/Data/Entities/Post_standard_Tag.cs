﻿using Microsoft.EntityFrameworkCore;

namespace TagsMath.Data.Entities
{
    [PrimaryKey(nameof(post_id), nameof(tag_id))]
    internal class Post_standard_Tag
    {
        public int post_id { get; set; }

        public int tag_id { get; set; }
    }
}
