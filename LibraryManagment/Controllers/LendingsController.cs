using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagment.Data;
using LibraryManagment.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagment.Controllers
{
    [Authorize]
    public class LendingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LendingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lendings
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            //var applicationDbContext = _context.Lending.Include(l => l.Book).Include(l => l.Member);
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["RetDateSortParm"] = sortOrder == "ReturnDate" ? "ReturnDate_desc" : "ReturnDate";
            ViewData["CurrentFilter"] = searchString;
            
            var lending = from s in _context.Lending.Include(l => l.Book).Include(l => l.Member)
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                lending = lending.Where(s => s.Member.Forename.Contains(searchString)
                                       || s.Member.Surname.Contains(searchString)
                                       || s.Book.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    lending = lending.OrderByDescending(s => s.Member);
                    break;
                case "Date":
                    lending = lending.OrderBy(s => s.LendingDate);
                    break;
                case "date_desc":
                    lending = lending.OrderByDescending(s => s.LendingDate);
                    break;
                case "ReturnDate":
                    lending = lending.OrderBy(s => s.ReturnDate);
                    break;
                case "ReturnDate_desc":
                    lending = lending.OrderByDescending(s => s.ReturnDate);
                    break;
                default:
                    lending = lending.OrderBy(s => s.Member);
                    break;
            }
            return View(await lending.AsNoTracking().ToListAsync());
            //return View(await applicationDbContext.ToListAsync());

            ////Adding search into the lending
            //ViewData["CurrentFilter"] = searchString;
            //var members = from s in _context.Member
            //              select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    members = members.Where(s => s.Surname.Contains(searchString)
            //                           || s.Forename.Contains(searchString));
            //}


            ////Implementing sorting for Full Name
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ////var members = from s in _context.Member
            ////               select s;
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        members = members.OrderByDescending(s => s.Forename);
            //        break;
            //    //case "Date":
            //    //    members = members.OrderBy(s => s.EnrollmentDate);
            //    //    break;
            //    //case "date_desc":
            //    //    members = members.OrderByDescending(s => s.EnrollmentDate);
            //    //    break;
            //    default:
            //        members = members.OrderBy(s => s.Forename);
            //        break;



            //}
            ////return View(await members.AsNoTracking().ToListAsync());


        }

        // GET: Lendings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lending = await _context.Lending
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lending == null)
            {
                return NotFound();
            }

            return View(lending);
        }

        // GET: Lendings/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title");
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");

            //Another way to transfer data to vews (DTO). Refer to LendingViewModel class in Models.
            //LendingViewModel lendingViewModel = new LendingViewModel();
            //lendingViewModel.Books = _context.Book.ToList();
            //lendingViewModel.Members = _context.Member.ToList();
            return View();
        }

        // POST: Lendings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,BookId,LendingDate,ReturnDate,AmountPaid")] Lending lending)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lending);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", lending.BookId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName", lending.MemberId);
            return View(lending);
        }

        // GET: Lendings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lending = await _context.Lending.FindAsync(id);
            if (lending == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", lending.BookId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName", lending.MemberId);
            return View(lending);
        }

        // POST: Lendings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,BookId,LendingDate,ReturnDate,AmountPaid")] Lending lending)
        {
            if (id != lending.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lending);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LendingExists(lending.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", lending.BookId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName", lending.MemberId);
            return View(lending);
        }

        // GET: Lendings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lending = await _context.Lending
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lending == null)
            {
                return NotFound();
            }

            return View(lending);
        }

        // POST: Lendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lending = await _context.Lending.FindAsync(id);
            _context.Lending.Remove(lending);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LendingExists(int id)
        {
            return _context.Lending.Any(e => e.Id == id);
        }

        //Future works

        private decimal CalculateAmountOwned(DateTime landingDate, DateTime returnDate)
        {
            decimal amountOwned = 0;

            return amountOwned;
        }
    }
}
