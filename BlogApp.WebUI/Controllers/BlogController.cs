using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository blogRepository;
        private ICategoryRepository categoryRepository;
        public BlogController(IBlogRepository _blogRepository, ICategoryRepository _categoryRepository) {
            blogRepository = _blogRepository;
            categoryRepository = _categoryRepository;
        }
        public IActionResult Index(int? id, string query)
        {
            var blogs = blogRepository.GetAll();
            if ((id ?? 0) > 0)
            {
                blogs = blogs.Where(x => x.IsApproved && ((id ?? 0) == 0 || x.CategoryId == id));
            }
            if (!string.IsNullOrWhiteSpace(query))
            {
                blogs = blogs.Where(x => x.BlogTitle.Contains(query) || x.BlogDescription.Contains(query) || x.BlogBody.Contains(query));
            }

            return View(blogs.OrderByDescending(o => o.BlogDate));
        }

        public IActionResult List()
        {
            return View(blogRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog entity, IFormFile file)
        {
            entity.BlogDate = DateTime.Now;
            entity.IsActive = true;
            if (ModelState.IsValid)
            {
                if (file!=null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    entity.BlogImage = file.FileName;
                }
                
                blogRepository.AddBlog(entity);
                TempData["message"] = $"{entity.BlogTitle} created successfully";
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
            return View(blogRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Blog entity, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    entity.BlogImage = file.FileName;
                }
                blogRepository.UpdateBlog(entity);
                TempData["message"] = $"{entity.BlogTitle} updated successfully";
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
            return View(entity);
        }

        public IActionResult Details(int id)
        {
            return View(blogRepository.GetById(id));
        }

        public IActionResult Delete(int id)
        {
            blogRepository.DeleteBlog(id);
            return RedirectToAction("List");
        }

    }
}