using BlogApp.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EFCore
{
    public class SeedData
    {
        public static void Seed(IApplicationBuilder app) {
            BlogContext context = app.ApplicationServices.GetRequiredService<BlogContext>();
            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category()
                {
                    CategoryName = "Test Category",
                    IsActive = true
                });

                context.SaveChanges();
            }

            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange(new Blog()
                {
                    BlogTitle = "Test Example No:1",
                    BlogDescription = "It was created for testing",
                    BlogImage = "1.jpeg",
                    BlogBody = "Hello World",
                    BlogDate = DateTime.Now.AddDays(-1),
                    CategoryId = 1,
                    IsActive = true,
                    IsApproved = true
                }, new Blog()
                {
                    BlogTitle = "Test Example No:2",
                    BlogDescription = "It was created for testing",
                    BlogImage = "2.jpeg",
                    BlogBody = "Again Hello World",
                    BlogDate = DateTime.Now.AddDays(-2),
                    CategoryId = 1,
                    IsActive = true,
                    IsApproved = false
                });

                context.SaveChanges();
            }

           
        }
    }
}
