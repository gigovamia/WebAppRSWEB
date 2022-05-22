using WebAppRSWEB.Models;

namespace WebAppRSWEB.ViewModel
{
    public class StudentsFilter
    {
        public IList<Student>? Students { get; set; }

        public string? FullName { get; set; }

        public string? studentID { get; set; }
    }
}
