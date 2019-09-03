using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace testImage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostEnvironment _environment;

        public HomeController(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Images(IFormFile file)
        {
            if (file == null || file.Length < 0)
            {
                return BadRequest();
            }

            await using var stream = new MemoryStream();
            
            await file.CopyToAsync(stream);

            using var img = Image.FromStream(stream);
            using var graphic = Graphics.FromImage(img);
            
            var font = new Font(FontFamily.GenericSansSerif, 35, FontStyle.Bold, GraphicsUnit.Pixel);
            var brush = new SolidBrush(Color.FromArgb(30, 162, 51, 255));
            var point = new Point(img.Width - 230, img.Height - 50);

            graphic.DrawString("cashwugeek", font, brush, point);
            
            img.Save($"{_environment.ContentRootPath}\\{Guid.NewGuid()}.png", ImageFormat.Png);

            return Ok();
        }
    }
}