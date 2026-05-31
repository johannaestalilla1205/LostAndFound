using Microsoft.AspNetCore.Mvc;
using LostAndFound.Data;
using LostAndFound.Models;
using LostAndFound.Models.Entities;

namespace LostAndFound.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ItemsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddItemViewModel viewModel)
        {
            var item = new Item
            {
                Title = viewModel.Title,
                Category = viewModel.Category,
                Type = viewModel.Type,
                Description = viewModel.Description,
                Location = viewModel.Location,
                DateLostFound = viewModel.DateLostFound,
                ContactName = viewModel.ContactName,
                ContactNumber = viewModel.ContactNumber
            };

            dbContext.Items.Add(item);
            dbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            var items = dbContext.Items.ToList();

            return View(items);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var item = dbContext.Items.Find(id);

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
                dbContext.Items.Remove(item);

                dbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}
