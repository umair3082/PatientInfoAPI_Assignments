using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientInfoAPI_Assignments.DTOs;
using PatientInfoAPI_Assignments.Entites;
using PatientInfoAPI_Assignments.Utilities;

namespace PatientInfoAPI_Assignments.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public PatientController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpPost("CreatePatient")]
        public async Task<ActionResult> CreatePatient([FromBody] PatientCreateionDTO patientDTO)
        {
            if (patientDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var patientEntity = mapper.Map<Patient>(patientDTO);

            db.Add(patientEntity);
            await db.SaveChangesAsync();

            return Ok(patientEntity.PatientId);
        }
        [HttpPut]
        [Route("UpdatePatient/{id:int}")]
        public async Task<ActionResult> UpdatePatient(int id, [FromBody] PatientUpdateDTO patientDTO)
        {
            var data = await db.Patients.FirstOrDefaultAsync(x => x.PatientId.Equals(id));
            if (data == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(patientDTO.Name))
            {
                data.Name = patientDTO.Name;
            }
            if (!string.IsNullOrEmpty(patientDTO.Age.ToString()))
            {
                data.Age = patientDTO.Age != null ? Convert.ToInt32(patientDTO.Age) : 0;
            }
            if (!string.IsNullOrEmpty(patientDTO.PhoneNumber))
            {
                data.PhoneNumber = patientDTO.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(patientDTO.Gender))
            {
                data.Gender = patientDTO.Gender;
            }
            if (!string.IsNullOrEmpty(patientDTO.Address))
            {
                data.Address = patientDTO.Address;
            }

            //0300-9693673
            //Syed Kashif Abbas
            //3 patti spin hacks
            //mapper.Map(patientDTO, data);
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("GetAllPatient")]
        public async Task<ActionResult<List<PatientDTO>>> GetAllPatient([FromQuery] PaginationDTO paginationDTO, [FromBody] PatientSearchDTO searchDTO)
        {
            var query = db.Patients.AsQueryable();
            if (query != null)
            {
                if (!string.IsNullOrEmpty(searchDTO.Name))
                    query = query.Where(x => x.Name.Contains(searchDTO.Name));
                if (!string.IsNullOrEmpty(searchDTO.Age.ToString()) && searchDTO.Age !=0)
                    query = query.Where(x => x.Age.Equals(searchDTO.Age));
                if (!string.IsNullOrEmpty(searchDTO.PhoneNumber))
                    query = query.Where(x => x.PhoneNumber.Contains(searchDTO.PhoneNumber));
                if (!string.IsNullOrEmpty(searchDTO.Gender))
                    query = query.Where(x => x.Gender.Contains(searchDTO.Gender));
                if (!string.IsNullOrEmpty(searchDTO.Address))
                    query = query.Where(x => x.Address.Contains(searchDTO.Address));

                await HttpContext.InsertParamsPaginationInHeader(query);
                var data = await query
                        .OrderByDescending(x => x.PatientId)
                        .Paginate(paginationDTO)
                        .ToListAsync();
                var finalLst = mapper.Map<List<PatientDTO>>(data);
                return finalLst;
            }
            else
                return new List<PatientDTO>() { };
        }
        [HttpPost("AllPatients")]
        public async Task<ActionResult<List<PatientDTO>>> AllPatients()
        {
            var query = db.Patients.AsQueryable();
            if (query != null)
            {
                await HttpContext.InsertParamsPaginationInHeader(query);
                var data = await query
                        .OrderByDescending(x => x.PatientId)
                        .ToListAsync();
                var finalLst = mapper.Map<List<PatientDTO>>(data);
                return finalLst;
            }
            else
                return new List<PatientDTO>() { };
        }
        [HttpDelete]
        [Route("DeletePatient/{id:int}")]
        public async Task<ActionResult> DeletePatients(int id)
        {
            var data = await db.Patients.FirstOrDefaultAsync(x => x.PatientId.Equals(id));
            if (data == null)
            {
                return NotFound();
            }
            db.Remove(data);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("CreateVisit")]
        public async Task<ActionResult> CreateVisit([FromBody] VisitCreatationDTO visitCreatationDTO)
        {
            if (visitCreatationDTO == null)
            {
                return BadRequest("Invalid data");  
            }

            var visitEntity = mapper.Map<Visit>(visitCreatationDTO);

            db.Add(visitEntity);
            await db.SaveChangesAsync();

            return Ok(visitEntity.VisitId);
        }


        [HttpPost("VisitsList")]
        public async Task<ActionResult<List<VisitDTO>>> VisitList([FromQuery] PaginationDTO paginationDTO,
                [FromBody] VisitSearchDTO? searchDTO
            )
        {
            var query = db.Visits
                          .Include(x=>x.Patient)
                          .Include(x=>x.ConsultingDoctor)
                          .Include(x=>x.DiseaseNavigation)
                          .AsQueryable();
            if (query != null)
            {
                if (searchDTO != null)
                {
                    if (!string.IsNullOrEmpty(searchDTO.PatientName))
                        query = query.Where(x => x.Patient.Name.Contains(searchDTO.PatientName));
                    if (!string.IsNullOrEmpty(searchDTO.Age))
                        query = query.Where(x => x.Patient.Age.Equals(int.Parse(searchDTO.Age)));
                    if (!string.IsNullOrEmpty(searchDTO.PhoneNumber))
                        query = query.Where(x => x.Patient.PhoneNumber.Contains(searchDTO.PhoneNumber));
                    if (!string.IsNullOrEmpty(searchDTO.Gender))
                        query = query.Where(x => x.Patient.Gender.Equals(searchDTO.Gender));
                    if (!string.IsNullOrEmpty(searchDTO.Address))
                        query = query.Where(x => x.Patient.Address.Contains(searchDTO.Address));
                    if (!string.IsNullOrEmpty(searchDTO.Disease))
                        query = query.Where(x => x.DiseaseNavigation.DiseaseName.Contains(searchDTO.Disease));
                    if (!string.IsNullOrEmpty(searchDTO.Doctor))
                        query = query.Where(x => x.ConsultingDoctor.DoctorName.Contains(searchDTO.Doctor));

                }

                await HttpContext.InsertParamsPaginationInHeader(query);
                var data = await query
                        .OrderByDescending(x => x.VisitId)
                        .Paginate(paginationDTO)
                        .ToListAsync();
                var finalLst = mapper.Map<List<VisitDTO>>(data);
                return finalLst;
            }
            else
                //return NoContent();
            return new List<VisitDTO>() { };
        }

    }
}
