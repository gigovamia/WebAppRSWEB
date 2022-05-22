using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppRSWEB.Data;
using WebAppRSWEB.Models;
using WebAppRSWEB.ViewModel;

namespace WebAppRSWEB.Controllers
{
    public class TeachersController : Controller
    {
        private readonly WebAppRSWEBContext _context;

        public TeachersController(WebAppRSWEBContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(string fullname, string degree, string academicRank)
        {
            IQueryable<Teacher> Teachers = _context.Teacher.AsQueryable();
            IQueryable<string> degreeQuery = _context.Teacher.OrderBy(m => m.Degree).Select(m => m.Degree).Distinct();
            IQueryable<string> academicRankQuery = _context.Teacher.OrderBy(m => m.AcademicRank).Select(m => m.AcademicRank).Distinct();
            if (!string.IsNullOrEmpty(fullname))
            {
                if (fullname.Contains(" "))
                {
                    string[] names = fullname.Split(" ");
                    Teachers = Teachers.Where(s => s.FirstName.Contains(names[0]) || s.LastName.Contains(names[1]));
                }
                else
                {
                    Teachers = Teachers.Where(s => s.FirstName.Contains(fullname) || s.LastName.Contains(fullname));
                }
            }

            if (!string.IsNullOrEmpty(academicRank))
            {
                Teachers = Teachers.Where(c => c.AcademicRank.Contains(academicRank));
            }

            if (!string.IsNullOrEmpty(degree))
            {
                Teachers = Teachers.Where(c => c.Degree.Contains(degree));
            }

            var TeachersFilter = new TeacherFilter
            {
                Teachers = await Teachers.Include(m => m.Courses1).Include(m => m.Courses2).ToListAsync(),
                Degrees = new SelectList(await degreeQuery.ToListAsync()),
                AcademicRanks = new SelectList(await academicRankQuery.ToListAsync()),

            };
            //            var mvcFacultyContext = _context.Teacher.Include(m => m.Courses1)
            //                                                  .Include(m => m.Courses2);
            return View(TeachersFilter);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.Include(c => c.Courses1)
               .Include(c => c.Courses2)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Degree,AcademicRank,OfficeNumber,HireDate")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Degree,AcademicRank,OfficeNumber,HireDate")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            IQueryable<Course> course = _context.Course.AsQueryable();
            IQueryable<Course> courses1 = course.Where(x => x.FirstTeacherId == teacher.Id);
            IQueryable<Course> courses2 = course.Where(x => x.SecondTeacherId == teacher.Id);

            foreach (var courses in courses1)
            {
                courses.FirstTeacherId = null;
            }
            foreach (var courses in courses2)
            {
                courses.SecondTeacherId = null;
            }

            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
          return (_context.Teacher?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
