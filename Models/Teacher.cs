﻿using System.ComponentModel.DataAnnotations;

namespace WebAppRSWEB.Models
{
    public class Teacher
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Degree { get; set; }

        [Display(Name = "Academic rank")]
        [StringLength(25)]
        public string? AcademicRank { get; set; }

        [Display(Name = "Office number")]
        [StringLength(10)]
        public string? OfficeNumber { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date of hiring")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Courses as Professor")]
        public ICollection<Course>? Courses1 { get; set; }

        [Display(Name = "Courses as Assistant")]
        public ICollection<Course>? Courses2 { get; set; }
      
    }
}
