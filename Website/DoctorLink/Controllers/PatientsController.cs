using DoctorLink.Data;
using DoctorLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorLink.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _db;

        // ApplicationDbContext accessed through dependency injection
        public PatientsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // no need for SQL statements when using ENTITY framework
            IEnumerable<Patient> objCategoryList = _db.Patients;

            // pass data to view
            return View(objCategoryList);
        }

        //GET action method when you click Create btn to bring u to create new category creation prompt
        public IActionResult Create()
        {
            return View();
        }

        //POST action method
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient obj)
        {
            //if  (obj.Name == obj.DisplayOrder)
            //{
            //    //important to have name as key for model error so that it displays error in Name field
            //    ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the key"); 
            //}
            if (ModelState.IsValid) //check if model has valid entries
            {
                _db.Patients.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index"); // look for Index method within same controller (could also specify a diff controller)
            }
            return View(obj);
            
        }

        //GET action method when you click Create btn to bring u to create new category creation prompt
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var patientFromDb = _db.Patients.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSngle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (patientFromDb == null)
            {
                return NotFound();
            }

            return View(patientFromDb);
        }

        //POST action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient obj)
        {
            //if (obj.Name == obj.DisplayOrder)
            //{
            //    //important to have name as key for model error so that it displays error in Name field
            //    ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the key");
            //}
            if (ModelState.IsValid)
            {
                _db.Patients.Update(obj); //update in db
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index"); // look for Index method within same controller (could also specify a diff controller)
            }
            return View(obj);

        }

        //GET action method when you click Delete btn to bring u to create new category creation prompt
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var patientFromDb = _db.Patients.Find(id);

            if (patientFromDb == null)
            {
                return NotFound();
            }

            return View(patientFromDb);
        }

        //POST action method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Patients.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            
            _db.Patients.Remove(obj); //update in db
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
