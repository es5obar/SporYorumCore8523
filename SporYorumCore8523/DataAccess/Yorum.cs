using System;
using System.Collections.Generic;

namespace SporYorumCore8523.DataAccess
{
    public partial class Yorum
    {
        public int Id { get; set; }
        public string Icerik { get; set; } = null!;
        public string Yorumcu { get; set; } = null!;
        public int SporId { get; set; }

        public virtual Spor Spor { get; set; } = null!;
    }
}
