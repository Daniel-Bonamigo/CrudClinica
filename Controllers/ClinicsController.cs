using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinicWeb.Data;
using VetClinicWeb.Models;

namespace VetClinicWeb.Controllers
{
    public class ClinicsController : Controller
    {

        private readonly VetClinicContext _context;

        public ClinicsController(VetClinicContext context)


        {
            _context = context;
        }

        [HttpGet]
       
        public async Task<IActionResult> Index()
        {
            return _context.Clinic != null ?
                        View(await _context.Clinic.ToListAsync()) :
                        Problem("Entity Clinic id null");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create( [Bind("Id,Name,Phone,Email")] Clinic clinic)
        
        {
            if (ModelState.IsValid)
            {
                _context.Clinic.Add(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(clinic);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clinic == null) 
                return NotFound();

            var clinic = await _context.Clinic.FindAsync(id);

            if (clinic == null)
                return NotFound();
            
            return View(clinic);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Email")] Clinic clinic)
        {
            if (id != clinic.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicExists(clinic.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));            
            
            }   
            return View(clinic);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context == null)
                return NotFound();
            var clinic = await (_context.Clinic.FirstOrDefaultAsync(c => c.Id == id));
            if (clinic == null) 
                return NotFound();

            return View(clinic);

        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clinic == null)
                return Problem("Entity set 'Clinic' is null");
            var clinic = await _context.Clinic.FindAsync(id);
            if (clinic != null)
                _context.Clinic.Remove(clinic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool ClinicExists(int id)
        {
            return (_context.Clinic?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }


}

