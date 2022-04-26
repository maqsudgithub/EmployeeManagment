using EmployeeManagment.Data;
using EmployeeManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Controllers
{
    public class AttendenceController : Controller
    {
        private readonly EmployeeContext _DB;
        public AttendenceController(EmployeeContext Db)
        {
            _DB = Db;
        }


        public IActionResult AttendenceList()
        {

            try
            {
                var stdlist = from a in _DB.tbl_Attendence
                              join b in _DB.tbl_Employee
                              on a.EmpID equals b.ID
                              into Dep
                              from b in Dep.DefaultIfEmpty()
                              select new Attendence
                              {
                                  ID = a.ID,
                                  EmpID = a.EmpID,
                                  Date = a.Date,
                                  Attandence = a.Attandence,
                                  Employee = b == null ? null : b.Name,

                              };

                return View(stdlist);
            }
            catch (Exception ex)
            {

                return View();
            }
            
        }


        [HttpGet]
        public IActionResult Create(Attendence obj)
        {
            loadDDL();
            return View(obj);
        }


        [HttpPost]
        public async Task<IActionResult> AddAttendence(Attendence obj)
        {
            try
            {
                
                //var todaydate = DateTime.Now;
                //var check = _DB.tbl_Attendence.Where(a => a.Date == todaydate && a.Employee == "Name").FirstOrDefault()

                
                
                if (obj.ID == 0)
                {
                    _DB.tbl_Attendence.Add(obj);
                    await _DB.SaveChangesAsync();

                }
                else
                {
                    _DB.Entry(obj).State = EntityState.Modified;
                    await _DB.SaveChangesAsync();
                }
                return RedirectToAction("AttendenceList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("AttendenceList");

            }
        }


        private void loadDDL()
        {
            try
            {
                List<Employee> deplist = new List<Employee>();
                deplist = _DB.tbl_Employee.ToList();
                deplist.Insert(0, new Employee { ID = 0, Name = "Please Select" });
                ViewBag.DepList = deplist;

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var std = await _DB.tbl_Attendence.FindAsync(id);
                if (std != null)
                {
                    _DB.tbl_Attendence.Remove(std);
                    await _DB.SaveChangesAsync();

                }
                return RedirectToAction("AttendenceList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("AttendenceList");

            }
        }
    }
}
