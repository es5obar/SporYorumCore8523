namespace SporYorumCore8523.Models
{
    public class SporYorumLeftOuterJoin
    {
        #region Spor entity'sinden gelen özellikler
        public string TakimAdi { get; set; }
        public string TakimUlke { get; set; }
        #endregion

        #region Yorum entity'sinden gelen özellikler
        public string Icerik { get; set; }
        public string Yorumcu { get; set; }
        #endregion
    }
}
