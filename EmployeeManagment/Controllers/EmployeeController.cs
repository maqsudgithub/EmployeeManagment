using EmployeeManagment.Data;
using EmployeeManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _DB;
        private readonly IWebHostEnvironment _webHost;
        public EmployeeController(EmployeeContext Db, IWebHostEnvironment webHost)
        {
            _DB = Db;
            this._webHost = webHost;
        }

        public IActionResult EmployeeList(string SearchText = "")
        {
            List<Employee> emp;
            if (SearchText != "" & SearchText != null)
            {
                emp = _DB.tbl_Employee
                    .Where(e => e.Name.Contains(SearchText)).ToList();
            }
            else

                emp = _DB.tbl_Employee.ToList();

            return View(emp);
        }





        [HttpGet]
        public IActionResult Create(Employee obj)
        {
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee obj, IFormFile image)
        {
            try
            {   
                string uniquefilename = string.Empty;
                if (image != null)
                {
                    string uploadsfolder = Path.Combine(_webHost.WebRootPath, "image");
                    uniquefilename = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadsfolder, uniquefilename);
                    image.CopyTo(new FileStream(filePath, FileMode.Create));
                    obj.Image = uniquefilename;
                }
                if (obj.ID == 0)
                {
                    _DB.tbl_Employee.Add(obj);
                    await _DB.SaveChangesAsync();
                }
                else
                {
                    _DB.Entry(obj).State = EntityState.Modified;
                    await _DB.SaveChangesAsync();
                }
                return RedirectToAction("EmployeeList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("EmployeeList");

            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var std = await _DB.tbl_Employee.FindAsync(id);
                if (std != null)
                {
                    _DB.tbl_Employee.Remove(std);
                    await _DB.SaveChangesAsync();
                }
                return RedirectToAction("EmployeeList");
            }
            catch (Exception ex)
            {

                return RedirectToAction("EmployeeList");
            }
        }



        public IActionResult EmployeeDetail(int Id)
        {
            Employee emp = _DB.tbl_Employee.Where(x=>x.ID==Id).FirstOrDefault();
            return View(emp);
        }
    }
}
