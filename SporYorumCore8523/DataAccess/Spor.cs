using System;
using System.Collections.Generic;

namespace SporYorumCore8523.DataAccess
{
    public partial class Spor
    {
        public Spor()
        {
            Yorum = new HashSet<Yorum>();
        }

        public int Id { get; set; }
        public string TakimAdi { get; set; } = null!;
        public string TakimUlke { get; set; } = null!;

        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}
