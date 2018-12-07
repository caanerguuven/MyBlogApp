using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.ViewComponents
{
    public class CategoryMenuViewComponent:ViewComponent
    {
        private ICategoryRepository categoryRepository;
        public CategoryMenuViewComponent(ICategoryRepository _categoryRepository) {
            categoryRepository = _categoryRepository;
        }
        public IViewComponentResult Invoke() {
            ViewBag.SelectedCategoryId = RouteData?.Values["id"];
            return View(categoryRepository.GetAll());
        }
    }
}
