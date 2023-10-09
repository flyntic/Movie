using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Models;
using Movie.Resources;
using Movie.Services;
using System.Diagnostics;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieApiService movieApiService;
        ApplicationContext _context;
        IWebHostEnvironment _appEnvironment;
        public HomeController(IMovieApiService movieApiService)
        {
            this.movieApiService = movieApiService;

            
        }
        /*
        public HomeController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }*/

        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> Movie(string id)
        {
            Cinema cinema = null;

            try
            {
                cinema = await movieApiService.SearchByIdAsync(id);
            }
            catch (Exception ex)
            {

                ViewBag.errorMessages = ex.Message;
            }
            return View(cinema);
        }


        public async Task<IActionResult> Search(string title)
        {
            MovieApiResponse result = null;

            try
            {
                 result = await movieApiService.SearchByTitleAsync(title);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessages = ex.Message;
            }

            ViewBag.searchMovie = title;
            return View(result);
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
       /*
        [HttpPost]
        public async Task<IActionResult>void AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment?.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileImage file = new FileImage(uploadedFile.FileName, path);
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }*/
    }
}