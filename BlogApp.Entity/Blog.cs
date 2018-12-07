using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Entity
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }
        public string BlogBody { get; set; }
        public string BlogImage { get; set; }
        [BindNever]
        public DateTime BlogDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsPublishedInHome { get; set; }
        public bool IsPublishedInSlider { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsActive { get; set; }
    }
}
