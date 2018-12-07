using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(categoryRepository.GetAll());
        }

        [HttpGet]
        public IActionResult AddOrUpdate(int? id)
        {
            int categoryId = id ?? 0;
            if (categoryId == 0)
            {
                return View(new Category());
            }
            else
            {
                return View(categoryRepository.GetById(categoryId));
            }
        }

        [HttpPost]
        public IActionResult AddOrUpdate(Category entity)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.SaveCategory(entity);
                TempData["message"] = $"{entity.CategoryName} was loaded successfully";
                return RedirectToAction("List");
            }

            return View(entity);
        }

        public IActionResult Delete(int id)
        {
            categoryRepository.DeleteCategory(id);
            return RedirectToAction("List");
        }
    }
}