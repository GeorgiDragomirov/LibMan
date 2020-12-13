using LibraryManagment.Data;
using LibraryManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> BooksRented()
        {

            IQueryable<LendedBooksVM> data =
                    from Lending in _context.Lending where Lending.AmountPaid == false
                    group Lending by Lending.Member.Forename + ' ' + Lending.Member.Surname + ' ' into dateGroup

                    select new LendedBooksVM()
                    {
                        FullName = dateGroup.Key,
                        BookNumbers = dateGroup.Count()
                    };
            return View(await data.AsNoTracking().ToListAsync());
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
