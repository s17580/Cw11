using Cw11.ClinicRequest;
using Cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cw11.ClinicServices
{
    public interface IDbClinicService
    {

        Doctor AddDoctor(AddClinicDoctorRequest req);
        Doctor UpdateDoctor(UpdateClinicDoctorRequest req);
        public IEnumerable<Doctor> GetDoctors();
        public string DeleteDoctor(int id);


    }
}
