namespace SporYorumCore8523.Models
{
    public class SporYorumInnerJoinModel
    {
        #region Spor entity'sinden gelen özellikler
        //public int Id { get; set; }
        public string TakimAdi { get; set; }
        public string TakimUlke { get; set; }
        #endregion

        #region Yorum entity'sinden gelen özellikler
        //public int Id { get; set; }
        public string Icerik { get; set; }
        public string Yorumcu { get; set; }
       
        #endregion
    }
}
