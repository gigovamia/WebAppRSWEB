using Microsoft.EntityFrameworkCore;
using WebAppRSWEB.Data;

namespace WebAppRSWEB.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebAppRSWEBContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<WebAppRSWEBContext>>()))
            {
                // Look for any movies.
                if (context.Student.Any() || context.Course.Any() || context.Teacher.Any())
                {
                    return; // DB has been seeded
                }

                context.Teacher.AddRange(
                new Teacher { /*Id = 1, */FirstName = "Rob", LastName = "Reiner", Degree = "FEIT", AcademicRank = "PhD" },
                new Teacher { /*Id = 2, */FirstName = "Ivan", LastName = "Reitman", Degree = "MFS", AcademicRank = "Master" },
                new Teacher { /*Id = 3, */FirstName = "Howard", LastName = "Hawks", Degree = "PMF", AcademicRank = "Docent" }
            );
                context.SaveChanges();

                context.Student.AddRange(
        new Student { /*Id = 1, */FirstName = "Billy", LastName = "Crystal", AcquiredCredits = 50 , CurrentSemester=5, EducationLevel="Bachelors degree" },
        new Student { /*Id = 2, */FirstName = "Meg", LastName = "Ryan", AcquiredCredits = 70 },
        new Student { /*Id = 3, */FirstName = "Carrie", LastName = "Fisher", AcquiredCredits = 45 },
        new Student { /*Id = 4, */FirstName = "Bill", LastName = "Murray", AcquiredCredits = 30 },
        new Student { /*Id = 5, */FirstName = "Dan", LastName = "Aykroyd", AcquiredCredits = 80 },
        new Student { /*Id = 6, */FirstName = "Sigourney", LastName = "Weaver", AcquiredCredits = 70 },
        new Student { /*Id = 7, */FirstName = "John", LastName = "Wayne", AcquiredCredits = 100 },
        new Student { /*Id = 8, */FirstName = "Dean", LastName = "Martin", AcquiredCredits = 70 }

        );
                context.SaveChanges();

                context.Course.AddRange(
new Course
{
    //Id = 1,
    Title = "RSWEB",
    Credits = 6,
    Semester = 6,
    Programme = "KTI, TKII",
    FirstTeacherId = context.Teacher.Single(d => d.FirstName == "Rob" && d.LastName == "Reiner").Id,
    SecondTeacherId = context.Teacher.Single(d => d.FirstName == "Howard" && d.LastName == "Hawks").Id
},
new Course
{
    //Id = 2,
    Title = "Math 1",
    Credits = 7,
    Semester = 1,
    Programme = "All",
    FirstTeacherId = context.Teacher.Single(d => d.FirstName == "Ivan" && d.LastName == "Reitman").Id,
    SecondTeacherId = context.Teacher.Single(d => d.FirstName == "Howard" && d.LastName == "Hawks").Id
},
new Course
{
    //Id = 3,
    Title = "Digital Telecommunications",
    Credits = 6,
    Semester = 5,
    Programme = "TKII",
    FirstTeacherId = context.Teacher.Single(d => d.FirstName == "Ivan" && d.LastName == "Reitman").Id,
    SecondTeacherId = context.Teacher.Single(d => d.FirstName == "Rob" && d.LastName == "Reiner").Id
},
new Course
{
    //Id = 4,
    Title = "Signals and Systems",
    Credits = 6,
    Semester = 3,
    Programme = "All",
    FirstTeacherId = context.Teacher.Single(d => d.FirstName == "Howard" && d.LastName == "Hawks").Id,
    SecondTeacherId = context.Teacher.Single(d => d.FirstName == "Ivan" && d.LastName == "Reitman").Id
}
);
                context.SaveChanges();

                context.Enrollment.AddRange(
                new Enrollment { studentId = 1, CourseId = 1 },
                new Enrollment { studentId = 2, CourseId = 3 },
                new Enrollment { studentId = 7, CourseId = 1 },
                new Enrollment { studentId = 4, CourseId = 2 },
                new Enrollment { studentId = 5, CourseId = 2 },
                new Enrollment { studentId = 7, CourseId = 4 },
                new Enrollment { studentId = 4, CourseId = 3 },
                new Enrollment { studentId = 8, CourseId = 3 },
                new Enrollment { studentId = 6, CourseId = 4 }
                    );
                context.SaveChanges();
            }
        }

    }
}
