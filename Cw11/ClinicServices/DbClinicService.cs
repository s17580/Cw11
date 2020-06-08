using Cw11.ClinicRequest;
using Cw11.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cw11.ClinicServices
{
    public class DbClinicService : IDbClinicService
    {
        private readonly ClinicCodeFirstContext _clinicCodeFirstContext;

        public DbClinicService(ClinicCodeFirstContext clinicCodeFirstContext)
        {
            _clinicCodeFirstContext = clinicCodeFirstContext;
        }

        public Doctor AddDoctor(AddClinicDoctorRequest req)
        {
            var doctor = new Doctor { IdDoctor = req.IdDoctor, FirstName = req.FirstName, LastName = req.LastName, Email = req.Email };


            _clinicCodeFirstContext.Add(doctor);
            _clinicCodeFirstContext.SaveChanges();

            return doctor;
        }

        public string DeleteDoctor(int id)
        {
            var doctor = _clinicCodeFirstContext.Doctors.FirstOrDefault(Dr => Dr.IdDoctor == id);
            if (doctor != null)
            {
                _clinicCodeFirstContext.Remove(doctor);
                _clinicCodeFirstContext.SaveChanges();
                return "Lekarz usunięty";
            }
            else
                return "Lekarz nie znaleziony";
        }


        public IEnumerable<Doctor> GetDoctors()
        {
            return _clinicCodeFirstContext.Doctors;
        }

        public Doctor UpdateDoctor(UpdateClinicDoctorRequest req)
        {
            var doctor = _clinicCodeFirstContext.Doctors.FirstOrDefault(Dr => Dr.IdDoctor == req.IdDoctor);
            if (doctor != null)
            {
                doctor.FirstName = req.FirstName;
                doctor.LastName = req.LastName;
                doctor.Email = req.Email;

                _clinicCodeFirstContext.Update(doctor);
                _clinicCodeFirstContext.SaveChanges();

                return doctor;
            }
            else
                return null;
        }

    }
}