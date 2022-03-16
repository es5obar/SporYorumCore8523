using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SporYorumCore8523.DataAccess;

namespace SporYorumCore8523.Controllers
{
    public class YorumController : Controller
    {
        private SporYorumContext _db = new SporYorumContext();
        public IActionResult Index()
        {
            List<Yorum> yorumlar = _db.Yorum.Include(yorum => yorum.Spor).OrderBy(yorum => yorum.Yorumcu).ToList(); // ThenBy, ThenByDescending
            return View(yorumlar);
        }
        public IActionResult Details(int id)
        {
            Yorum yorum = _db.Yorum.Include(yorum => yorum.Spor).SingleOrDefault(yorum => yorum.Id == id);
            return View(yorum);
        }
        public IActionResult Create()
        {
            List<Spor> sporlar = _db.Spor.OrderBy(spor => spor.TakimAdi).ToList();

            //ViewBag.KonuId = new SelectList(konular, "Id", "Baslik");
            ViewData["SporId"] = new SelectList(sporlar, "Id", "TakimAdi"); // SelectList -> DropDownList, MultiSelectList -> ListBox

            return View();
        }
        [HttpPost]
        public IActionResult Create(Yorum yorum)
        {
            if (string.IsNullOrWhiteSpace(yorum.Icerik))
            {
                ViewBag.Mesaj = "İçerik boş girilemez!";
                ViewBag.SporId = new SelectList(_db.Spor.OrderBy(s => s.TakimAdi).ToList(), "Id", "TakimAdi", yorum.SporId);
                return View(yorum);
            }
            if (yorum.Icerik.Length > 200)
            {
                ViewBag.Mesaj = "İçerik en fazla 200 karakter olmalıdır!";
                ViewBag.SporId = new SelectList(_db.Spor.OrderBy(s => s.TakimAdi).ToList(), "Id", "TakimAdi", yorum.SporId);
                return View(yorum);
            }
            if (string.IsNullOrWhiteSpace(yorum.Yorumcu))
            {
                ViewBag.Mesaj = "Yorumcu boş girilemez!";
                ViewBag.SporId = new SelectList(_db.Spor.OrderBy(s => s.TakimAdi).ToList(), "Id", "TakimAdi", yorum.SporId);
                return View(yorum);
            }
            if (yorum.Yorumcu.Length > 100)
            {
                ViewBag.Mesaj = "Yorumcu en fazla 100 karakter olmalıdır!";
                ViewBag.SporId = new SelectList(_db.Spor.OrderBy(k => k.TakimAdi).ToList(), "Id", "TakimAdi", yorum.SporId);
                return View(yorum);
            }

            _db.Yorum.Add(yorum);
            _db.SaveChanges();
            TempData["YorumMesaj"] = "Yorum başarıyla eklendi.";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Yorum yorum = _db.Yorum.SingleOrDefault(yorum => yorum.Id == id);
            ViewBag.SporId = new SelectList(_db.Spor.OrderBy(konu => konu.TakimAdi).ToList(), "Id", "TakimAdi", yorum.SporId);
            return View(yorum);
        }
        [HttpPost]
        public IActionResult Edit(Yorum yorum)
        {
            if (string.IsNullOrWhiteSpace(yorum.Icerik))
            {
                ViewBag.Mesaj = "İçerik boş girilemez!";
                ViewBag.SporId = new SelectList(_db.Spor.OrderBy(k => k.TakimAdi).ToList(), "Id", "TakimAdi", yorum.SporId);
                return View(yorum);
            }
            if (yorum.Icerik.Length > 200)
            {
                ViewBag.Mesaj = "İçerik en fazla 200 karakter olmalıdır!";
                ViewBag.SporId = new SelectList(_db.Spor.OrderBy(k => k.TakimAdi).ToList(), "Id", "TakimAdi", yorum.SporId);
                return View(yorum);
            }
            if (string.IsNullOrWhiteSpace(yorum.Yorumcu))
            {
                ViewBag.Mesaj = "Yorumcu boş girilemez!";
                ViewBag.SporId = new SelectList(_db.Spor.OrderBy(k => k.TakimAdi).ToList(), "Id", "TakimAdi", yorum.SporId);
                return View(yorum);
            }
            if (yorum.Yorumcu.Length > 100)
            {
                ViewBag.Mesaj = "Yorumcu en fazla 100 karakter olmalıdır!";
                ViewBag.SporId = new SelectList(_db.Spor.OrderBy(k => k.TakimAdi).ToList(), "Id", "TakimAdi", yorum.SporId);
                return View(yorum);
            }

            Yorum mevcutYorum = _db.Yorum.SingleOrDefault(mevcutYorum => mevcutYorum.Id == yorum.Id);
            mevcutYorum.Icerik = yorum.Icerik;
            mevcutYorum.Yorumcu = yorum.Yorumcu;
            mevcutYorum.SporId = yorum.SporId;
            _db.Yorum.Update(mevcutYorum);
            _db.SaveChanges();
            TempData["YorumMesaj"] = "Yorum başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id) // ~/Yorum/Delete/1
        {
            Yorum yorum = _db.Yorum.Include(yorum => yorum.Spor).SingleOrDefault(yorum => yorum.Id == id);
            return View(yorum);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id) // ~/Yorum/Delete
        {
            Yorum yorum = _db.Yorum.Find(id);
            _db.Yorum.Remove(yorum);
            _db.SaveChanges();
            TempData["YorumMesaj"] = "Yorum başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
