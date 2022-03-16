using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporYorumCore8523.DataAccess;

namespace SporYorumCore8523.Controllers
{
    public class SporController : Controller
    {
        private SporYorumContext _db = new SporYorumContext();
        public IActionResult Index()
        {
            List<Spor> sporlar = _db.Spor.ToList();
            return View(sporlar);
        }
        public IActionResult Details(int id)
        {
            Spor spor = _db.Spor.Find(id);
            return View(spor);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(Spor spor)
        {
            if (string.IsNullOrWhiteSpace(spor.TakimAdi))
            {
                // ViewBag (özellik) ile ViewData (index) birbirleri yerine aynı özellik ve index adları üzerinden kullanılabilir 

                //ViewData["Mesaj"] = "Başlık boş girilemez!";
                ViewBag.Mesaj = "Takım adı boş girilemez!";

                return View(spor);
            }
            if (string.IsNullOrWhiteSpace(spor.TakimUlke))
            {
                ViewBag.Mesaj = "Takımın ülkesi boş girilemez!";
            }
            if (spor.TakimAdi.Length > 50)
            {
                ViewBag.Mesaj = "Takım Adı en fazla 50 karakter olmalıdır!";
                return View(spor);
            }
            if (spor.TakimUlke.Length > 50)
            {
                ViewBag.Mesaj = "Takımın ülkesi en fazla 50 karakter olmalıdır!";
                return View(spor);
            }

            _db.Spor.Add(spor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Spor spor = _db.Spor.SingleOrDefault(spor => spor.Id == id);
            return View(spor);
        }
        [HttpPost]
        public IActionResult Edit(Spor spor)
        {
            if (string.IsNullOrWhiteSpace(spor.TakimAdi))
            {
                ViewData["Mesaj"] = "Takım adı boş girilemez!";
                return View(spor);
            }
            if (spor.TakimAdi.Length > 50)
            {
                ViewBag.Mesaj = "Takım adı en fazla 50 karakter olmalıdır!";
                return View(spor);
            }
            if (!string.IsNullOrWhiteSpace(spor.TakimUlke))
            {
                ViewBag.Mesaj = "Takımın ülkesi boş girilemez!";
                return View(spor);
            }
            if (spor.TakimUlke.Length > 50)
            {
                ViewBag.Mesaj = "Takımın ülkesi en fazla 50 karakter olmalıdır!";
                return View(spor);
            }

            // güncelleme ve silme işlemleri için veri önce mutlaka veritabanındaki tablodan çekilmelidir ve sonra çekilen obje üzerinden güncelleme veya silme yapılmalıdır.
            Spor mevcutSpor = _db.Spor.SingleOrDefault(mevcutSpor => mevcutSpor.Id == spor.Id);
            mevcutSpor.TakimAdi = spor.TakimAdi;
            mevcutSpor.TakimUlke = spor.TakimUlke;
            _db.Spor.Update(mevcutSpor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Spor spor = _db.Spor.Include(s => s.Yorum).SingleOrDefault(s => s.Id == id);
            if (spor.Yorum != null && spor.Yorum.Count > 0)
            {
                // Eğer view dönmek yerine başka bir aksiyona yönlendirme yapılıyorsa TempData ile veriler yönlendirilen aksiyonun view'ına taşınmalıdır
                TempData["Mesaj"] = "Silinmek istenen spor ile ilişkili yorum kayıtları bulunmaktadır!";
            }
            else
            {
                _db.Spor.Remove(spor);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

