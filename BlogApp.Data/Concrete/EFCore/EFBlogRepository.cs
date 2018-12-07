using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EFCore
{
    public class EFBlogRepository : IBlogRepository
    {

        private BlogContext context;
        public EFBlogRepository(BlogContext _context)
        {
            context = _context;
        }

        public void AddBlog(Blog entity)
        {
            context.Blogs.Add(entity);
            context.SaveChanges();
        }

        public void DeleteBlog(int blogId)
        {
            var blog = context.Blogs.FirstOrDefault(x => x.BlogId == blogId);
            if (blog!=null)
            {
                blog.IsActive = false;
                context.SaveChanges();
            }
        }

        public IQueryable<Blog> GetAll()
        {
            return context.Blogs.Where(x => x.IsActive == true);
        }

        public Blog GetById(int blogId)
        {
            return context.Blogs.FirstOrDefault(x => x.BlogId == blogId);
        }

        public void UpdateBlog(Blog entity)
        {
            var blog = GetById(entity.BlogId);
            if (blog!=null)
            {
                blog.BlogTitle = entity.BlogTitle;
                blog.BlogDescription = entity.BlogDescription;
                blog.BlogBody = entity.BlogBody;
                blog.BlogImage = entity.BlogImage;
                blog.CategoryId = entity.CategoryId;
                blog.IsApproved = entity.IsApproved;
                blog.IsPublishedInHome = entity.IsPublishedInHome;
                blog.IsPublishedInSlider = entity.IsPublishedInSlider;

                context.SaveChanges();
            }
        }
    }
}
