using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoctorLink.Data;
using DoctorLink.Models;
using ZXing.QrCode;
using ZXing;
using ZXing.Windows.Compatibility;
using System.Drawing;
using System.Text;
using ZXing.QrCode.Internal;

namespace DoctorLink.Controllers
{
    public class MedicationsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MedicationsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Generate QR code
        public async Task<IActionResult> GenerateQRCode(int? id)
        {
            var qrCodeBitmap = GenerateQRBitmap(ConcatenateQRString(id));

            using (var stream = new MemoryStream())
            {
                qrCodeBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                var imageData = stream.ToArray();
                return File(imageData, "image/png");
            }

        }

        // GET: Medications
        public async Task<IActionResult> Index(int patientId)
        {
            ViewData["PatientId"] = patientId;
            return View(_db.Patients.Find(patientId).Medications);
        }

        // GET: Medications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _db.Medication
                .FirstOrDefaultAsync(m => m.MedicationId == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // GET: Medications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Id,DrugName,Dose,MedicationDescription,UsageDescription,Notes,NumberOfScans")] Medication medication)
        {
            int patientId = (int)ViewData["PatientId"];
            if (ModelState.IsValid)
            {
                _db.Patients.Find(patientId).Medications.Add(medication);
                await _db.SaveChangesAsync();
                TempData["success"] = "Medication added successfully";

                return RedirectToAction(nameof(Index));
            }

            return View(medication);
        }

        // GET: Medications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _db.Medication.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            return View(medication);
        }

        // POST: Medications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DrugName,Dose,MedicationDescription,UsageDescription,Notes,NumberOfScans")] Medication medication)
        {
            if (id != medication.MedicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(medication);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationExists(medication.MedicationId))
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

            TempData["success"] = "Medication updated successfully";
            return View(medication);
        }

        // GET: Medications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _db.Medication
                .FirstOrDefaultAsync(m => m.MedicationId == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // POST: Medications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medication = await _db.Medication.FindAsync(id);
            if (medication != null)
            {
                _db.Medication.Remove(medication);
            }

            await _db.SaveChangesAsync();

            TempData["success"] = "Medication removed successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool MedicationExists(int id)
        {
            return _db.Medication.Any(e => e.MedicationId == id);
        }

        private string ConcatenateQRString(int? id)
        {
            StringBuilder qrString = new StringBuilder();
            var medication = _db.Medication.Find(id);

            qrString.Append(medication.DrugName + "/");
            qrString.Append(medication.Dose + "/");
            qrString.Append(medication.MedicationDescription + "/");
            qrString.Append(medication.UsageDescription);
            if (medication.Notes != null)
            {
                qrString.Append("/" +medication.Notes);
            }
            return qrString.ToString();
        }

        private Bitmap GenerateQRBitmap(string qrString)
        {
            QrCodeEncodingOptions options = new()
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 500,
                Height = 500
            };

            BarcodeWriter writer = new()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };

            var qrCodeBitmap = writer.Write(qrString);
            return qrCodeBitmap;
        }
    }
}
