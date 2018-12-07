using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBlogRepository blogRepository;
        public HomeController(IBlogRepository _blogRepository) {
            blogRepository = _blogRepository;
        }
        public IActionResult Index()
        {
            HomeBlogModel model = new HomeBlogModel()
            {
                HomeBlogs = blogRepository.GetAll().Where(x => x.IsApproved && x.IsPublishedInHome).ToList(),
                SliderBlogs = blogRepository.GetAll().Where(x => x.IsApproved && x.IsPublishedInSlider).ToList()
            };
            return View(model);
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}