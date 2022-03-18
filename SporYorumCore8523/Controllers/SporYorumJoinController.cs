using Microsoft.AspNetCore.Mvc;
using SporYorumCore8523.DataAccess;
using SporYorumCore8523.Models;

namespace SporYorumCore8523.Controllers
{
    public class SporYorumJoinController : Controller
    {
        private SporYorumContext _db = new SporYorumContext();

        public IActionResult SporYorumInnerJoin()
        {
            IQueryable<Spor> sporQuery = _db.Spor.AsQueryable(); // select * from Spor
            IQueryable<Yorum> yorumQuery = _db.Yorum.AsQueryable(); // select * from Yorum
            var joinQuery = from spor in sporQuery
                            join yorum in yorumQuery
                            on spor.Id equals yorum.SporId
                            //where konu.Id == 5
                            orderby yorum.Yorumcu
                            select new SporYorumInnerJoinModel()
                            {
                                TakimAdi = spor.TakimAdi,
                                TakimUlke = spor.TakimUlke,
                                Icerik = yorum.Icerik,
                                Yorumcu = yorum.Yorumcu,
                                

                     
                            };
            var model = joinQuery.ToList();
            return View(model);
        }

        /*
        select k.Baslik, k.Aciklama, y.Icerik, y.Yorumcu, y.Puan, 
        case when y.Puan < 3 then 'Kötü' when y.Puan = 3 then 'Orta' else 'İyi' end as PuanDurumu
        from Konu k left outer join Yorum y
        on k.Id = y.KonuId
        order by y.Puan desc, y.Yorumcu
        */
        public IActionResult SporYorumLeftOutJoin()
        {
            IQueryable<Spor> sporQuery = _db.Spor.AsQueryable();
            IQueryable<Yorum> yorumQuery = _db.Yorum.AsQueryable();
            var joinQuery = from spor in sporQuery
                            join yorum in yorumQuery
                            on spor.Id equals yorum.SporId into sporYorumJoin
                            from subSporYorumJoin in sporYorumJoin.DefaultIfEmpty()
                                //where konu.Id == 5
                            select new SporYorumLeftOuterJoin()
                            {
                                TakimAdi=spor.TakimAdi,
                                TakimUlke = spor.TakimUlke,
                                Icerik = subSporYorumJoin.Icerik,
                                Yorumcu = subSporYorumJoin.Yorumcu,
                            };
            var model = joinQuery.ToList();
            return View(model);
        }
    }
}
