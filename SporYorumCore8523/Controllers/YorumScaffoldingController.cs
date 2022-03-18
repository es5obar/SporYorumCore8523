using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SporYorumCore8523.DataAccess;

namespace SporYorumCore8523.Controllers
{
    public class YorumScaffoldingController : Controller
    {
        private readonly SporYorumContext _context;

        //public YorumScaffoldingController(BA_KonuYorumCoreContext context)
        //{
        //    _context = context;
        //}

        public YorumScaffoldingController()
        {
            _context = new SporYorumContext();
        }

        // GET: YorumScaffolding
        public IActionResult Index()
        {
            var SporYorumContext = _context.Yorum.Include(y => y.Spor);
            return View(SporYorumContext.ToList());
        }

        // GET: YorumScaffolding/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = _context.Yorum
                .Include(y => y.Spor)
                .SingleOrDefault(s => s.Id == id);
            if (yorum == null)
            {
                return NotFound();
            }

            return View(yorum);
        }

        // GET: YorumScaffolding/Create
        public IActionResult Create()
        {
            ViewData["SporId"] = new SelectList(_context.Spor, "Id", "TakimAdi");
            return View();
        }

        // POST: YorumScaffolding/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yorum);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SporId"] = new SelectList(_context.Spor, "Id", "TakimAdi", yorum.SporId);
            return View(yorum);
        }

        // GET: YorumScaffolding/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = _context.Yorum.Find(id);
            if (yorum == null)
            {
                return NotFound();
            }
            ViewData["SporId"] = new SelectList(_context.Spor, "Id", "TakimAdi", yorum.SporId);
            return View(yorum);
        }

        // POST: YorumScaffolding/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                _context.Update(yorum);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SporId"] = new SelectList(_context.Spor, "Id", "TakimAdi", yorum.SporId);
            return View(yorum);
        }

        // GET: YorumScaffolding/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = _context.Yorum
                .Include(y => y.Spor)
                .SingleOrDefault(s => s.Id == id);
            if (yorum == null)
            {
                return NotFound();
            }

            return View(yorum);
        }

        // POST: YorumScaffolding/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var yorum = _context.Yorum.Find(id);
            _context.Yorum.Remove(yorum);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
