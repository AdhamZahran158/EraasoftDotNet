using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Task11.Models;
using Task11.Models.DataAccess;
using Task11.ViewModels;

namespace Task11.Controllers
{
    public class DoctorController : Controller
    {
        ApplicationDbContext db = new();
        private string docId;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(ILogger<DoctorController> logger)
        {
            _logger = logger;
        }

        public IActionResult BookAppointment(BookAppointmentVM bookAppointmentVM)
        {
            var doctors = db.doctors.AsNoTracking().AsQueryable();
            // FILTER
            ViewBag.Spec = doctors.GroupBy(d => d.Specialization).Select(d => new
            {
                Specialization = d.Key
            });
            if(bookAppointmentVM.DocName is not null)
            {
                doctors = doctors.Where(d => d.Name.Contains(bookAppointmentVM.DocName));
                ViewBag.tempName=bookAppointmentVM.DocName;
            }

            if(bookAppointmentVM.Specialization is not null)
            {
                doctors = doctors.Where(d => d.Specialization ==  bookAppointmentVM.Specialization);
                ViewBag.tempSpec = bookAppointmentVM.Specialization;
            }

            // PAGINATION
            int page = bookAppointmentVM.page <= 0 ? 1 : bookAppointmentVM.page;
            var numOfPages = Math.Ceiling(doctors.Count() / 3.0);
            ViewBag.numOfPages = numOfPages;
            ViewBag.currentPage = page;
            doctors = doctors.Skip((page - 1)*3).Take(3);

            return View(doctors.AsEnumerable());
        }

        public IActionResult CompleteAppointment(AppointmentVM appointmentVM, int id)
        {
            var appointments = db.appointments.AsNoTracking().AsQueryable();
            var doctors = db.doctors.AsNoTracking().AsQueryable();
            bool isAppointed = false;


            if (appointmentVM.patientName is not null && appointmentVM.appointmentDate is not null && appointmentVM.appointmentTime is not null)
            {
                var checkAppointment = appointments
                    .Where(a => a.DoctorId == id && a.AppointmentDate == DateOnly.Parse(appointmentVM.appointmentDate)).AsEnumerable().Any(a =>
                    {
                        var Start = a.AppointmentTime;
                        var End = Start.AddMinutes(30);

                        return TimeOnly.Parse(appointmentVM.appointmentTime) < End && TimeOnly.Parse(appointmentVM.appointmentTime).AddMinutes(30) > Start;
                    });
                if (checkAppointment || TimeOnly.Parse(appointmentVM.appointmentTime) < TimeOnly.Parse("09:00") || TimeOnly.Parse(appointmentVM.appointmentTime) >= TimeOnly.Parse("17:00") || DateOnly.Parse(appointmentVM.appointmentDate).DayOfWeek == DayOfWeek.Saturday || DateOnly.Parse(appointmentVM.appointmentDate).DayOfWeek == DayOfWeek.Friday)
                {
                    isAppointed = true;
                    ViewBag.message = "Doctor is busy this time";
                }
            }

            if (appointmentVM.patientName is not null && appointmentVM.appointmentDate is not null && appointmentVM.appointmentTime is not null && isAppointed == false)
            {
                db.appointments.Add(new Appointment
                {
                    PatientName = appointmentVM.patientName,
                    AppointmentDate = DateOnly.Parse(appointmentVM.appointmentDate),
                    AppointmentTime = TimeOnly.Parse(appointmentVM.appointmentTime),
                    DoctorId = id
                });
                db.SaveChanges();
                ViewBag.message = "Appointed Successfully";
            }
            var doc = doctors.Where(d=>d.Id == id).FirstOrDefault();
                ViewBag.docName = doc.Name;
            return View();
        }

        public IActionResult ShowAppointments()
        {
            var appointments = db.appointments.AsNoTracking().AsQueryable();
            var doctors = db.doctors.AsNoTracking().AsQueryable();
            var appointmentsWithDoctors = appointments.Join(db.doctors, a => a.DoctorId, d => d.Id, (a, d) => new
            {
                doctorName = d.Name,
                patientName = a.PatientName,
                date = a.AppointmentDate,
                time = a.AppointmentTime
            });
            return View(appointmentsWithDoctors.AsEnumerable());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
