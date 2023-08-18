using Microsoft.EntityFrameworkCore;

namespace TagStartupMath.Data.Entities
{
    [PrimaryKey(nameof(startup_id), nameof(tag_id))]
    internal class Startup_standard_Tag
    {
        public int startup_id { get; set; }
        public int tag_id { get; set; }
    }
}
