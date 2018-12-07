using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EFCore
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private BlogContext context;
        public EFCategoryRepository(BlogContext _context)
        {
            context = _context;
        }
        public void AddCategory(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category != null)
            {
                category.IsActive = false;
                context.SaveChanges();
            }
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories.Where(x => x.IsActive == true);
        }

        public Category GetById(int categoryId)
        {
            return context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
        }

        public void SaveCategory(Category entity)
        {
            if (entity.CategoryId==0)
            {
                entity.IsActive = true;
                context.Categories.Add(entity);
            }
            else
            {
                var category = GetById(entity.CategoryId);
                if (category != null)
                {
                    category.CategoryName = entity.CategoryName;
                }
            }
            context.SaveChanges();
        }

        public void UpdateCategory(Category entity)
        {
            var category = GetById(entity.CategoryId);
            if (category != null)
            {
                category.CategoryName = entity.CategoryName;
                context.SaveChanges();
            }
        }
    }
}
