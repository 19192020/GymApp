﻿using System.ComponentModel.DataAnnotations;

namespace GymApp.Web.Models {
    public class Category : BaseEntity {
        public Category() 
        {
         this.Courses=new HashSet<Course>();//kategorinin altındaki kursları barındırır
        }   
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
