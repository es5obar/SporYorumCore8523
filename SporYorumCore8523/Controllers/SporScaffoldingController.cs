using Microsoft.AspNetCore.Mvc;
using SporYorumCore8523.DataAccess;

namespace SporYorumCore8523.Controllers
{
    public class SporScaffoldingController : Controller
    {
        private readonly SporYorumContext _context;
        public SporScaffoldingController()
        {
            _context = new SporYorumContext();
        }
        public IActionResult Index()
        {
           
            return View(_context.Spor.ToList());
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spor = _context.Spor
                .SingleOrDefault(s => s.Id == id);
            if (spor == null)
            {
                return NotFound();
            }

            return View(spor);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Spor spor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(spor);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spor = _context.Spor.Find(id);
            if (spor == null)
            {
                return NotFound();
            }
            return View(spor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Spor spor)
        {
            if (ModelState.IsValid)
            {
                _context.Update(spor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(spor);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spor = _context.Spor
                .SingleOrDefault(s => s.Id == id);
            if (spor == null)
            {
                return NotFound();
            }

            return View(spor);
        }

        // POST: KonuScaffolding/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var spor = _context.Spor.Find(id);
            _context.Spor.Remove(spor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
