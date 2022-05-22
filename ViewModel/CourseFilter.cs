using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppRSWEB.Models;

namespace WebAppRSWEB.ViewModel
{
    public class CourseFilter
    {
        public IList<Course> Courses { get; set; }
        public SelectList filtered { get; set; }
        public int CourseSem { get; set; }
        public string CourseProg { get; set; }
        public string CTitle { get; set; }
    }
}
