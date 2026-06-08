using Microsoft.AspNetCore.Mvc;
using LostAndFound.Data;
using LostAndFound.Models;
using LostAndFound.Models.Entities;

namespace LostAndFound.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ItemsController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddItemViewModel viewModel)
        {
            string fileName = null;

            if (viewModel.ImageFile != null)
            {
                string uploadsFolder =
                    Path.Combine(
                        webHostEnvironment.WebRootPath,
                        "images");

                fileName = Guid.NewGuid().ToString() + "_" +
                           viewModel.ImageFile.FileName;

                string filePath =
                    Path.Combine(uploadsFolder, fileName);

                using (var fileStream =
                       new FileStream(filePath, FileMode.Create))
                {
                    viewModel.ImageFile.CopyTo(fileStream);
                }
            }

            var userId =
    Guid.Parse(HttpContext.Session.GetString("UserId"));

            var item = new Item
            {
                UserId = userId,

                Title = viewModel.Title,
                Category = viewModel.Category,
                Type = viewModel.Type,
                Description = viewModel.Description,
                Location = viewModel.Location,
                DateLostFound = viewModel.DateLostFound,
                ContactName = viewModel.ContactName,
                ContactNumber = viewModel.ContactNumber,
                ImagePath = fileName,
                Status = "Pending"
            };

            dbContext.Items.Add(item);
            dbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List(string searchTerm)
        {
            var items = dbContext.Items.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                items = items.Where(x =>
                    x.Title.Contains(searchTerm) ||
                    x.Category.Contains(searchTerm));
            }

            return View(items.ToList());
        }

        [HttpGet]
        public IActionResult ClaimedHistory()
        {
            var history = dbContext.ClaimHistories.ToList();

            return View(history);
        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var item = dbContext.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            var currentUserId =
                Guid.Parse(HttpContext.Session.GetString("UserId"));

            if (item.UserId != currentUserId)
            {
                return Unauthorized();
            }

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Item viewModel)
        {
            var item = dbContext.Items.Find(viewModel.Id);

            if (item != null)
            {
                item.Title = viewModel.Title;
                item.Category = viewModel.Category;
                item.Type = viewModel.Type;
                item.Description = viewModel.Description;
                item.Location = viewModel.Location;
                item.DateLostFound = viewModel.DateLostFound;
                item.ContactName = viewModel.ContactName;
                item.ContactNumber = viewModel.ContactNumber;
                item.Status = viewModel.Status;

                var currentUserId = Guid.Parse(HttpContext.Session.GetString("UserId"));

                if (item.UserId != currentUserId)
                {
                    return Unauthorized();
                }

                if (viewModel.Status == "Claimed")
                {
                    var history = new ClaimHistory
                    {
                        ItemTitle = item.Title,
                        ClaimedBy = item.ContactName,
                        DateClaimed = DateTime.Now
                    };

                    dbContext.ClaimHistories.Add(history);
                }

                dbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(Item viewModel)
        {
            var item = dbContext.Items.Find(viewModel.Id);

            if (item != null)
            {
                var currentUserId = Guid.Parse(HttpContext.Session.GetString("UserId"));

                if (item.UserId != currentUserId)
                {
                    return Unauthorized();
                }
                dbContext.Items.Remove(item);

                dbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}
