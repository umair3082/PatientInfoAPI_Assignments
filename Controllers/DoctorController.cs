using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientInfoAPI_Assignments.DTOs;
using PatientInfoAPI_Assignments.Entites;
using PatientInfoAPI_Assignments.Utilities;

namespace PatientInfoAPI_Assignments.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public DoctorController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreationDTO doctorDTO)
        {
            if (doctorDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var doctorEntity = mapper.Map<Doctor>(doctorDTO);

            db.Add(doctorEntity);
            await db.SaveChangesAsync();

            return Ok(doctorEntity.DoctorId);
        }

        [HttpPost("GetAllDoctors")]
        public async Task<ActionResult<List<DoctorDTO>>> GetAllDoctors([FromQuery] PaginationDTO paginationDTO, [FromBody] DoctorSearchDTO searchDTO)
        {
            var query = db.Doctors.AsQueryable();
            if (query != null)
            {
                if (searchDTO != null)
                {
                    if (!string.IsNullOrEmpty(searchDTO.DoctorName))
                        query = query.Where(x => x.DoctorName.Contains(searchDTO.DoctorName));
                }
                await HttpContext.InsertParamsPaginationInHeader(query);
                var data = await query
                        .OrderByDescending(x => x.DoctorId)
                        .Paginate(paginationDTO)
                        .ToListAsync();
                var finalLst = mapper.Map<List<DoctorDTO>>(data);
                return finalLst;
            }
            else
                return new List<DoctorDTO>() { };
        }
        [HttpPost("AllDoctors")]
        public async Task<ActionResult<List<DoctorDTO>>> AllDoctors()
        {
            var query = db.Doctors.AsQueryable();
            if (query != null)
            {
                await HttpContext.InsertParamsPaginationInHeader(query);
                var data = await query
                        .OrderByDescending(x => x.DoctorId)
                        .ToListAsync();
                var finalLst = mapper.Map<List<DoctorDTO>>(data);
                return finalLst;
            }
            else
                return new List<DoctorDTO>() { };
        }

        [HttpGet("GetDoctorById/{id}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctorById(int id)
        {
            var doctor = await db.Doctors.FirstOrDefaultAsync(x => x.DoctorId.Equals(id));

            if (doctor == null)
            {
                return NotFound();
            }

            var doctorDTO = mapper.Map<DoctorDTO>(doctor);

            return Ok(doctorDTO);
        }

        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] DoctorDTO updatedDoctorDTO)
        {
            var existingDoctor = await db.Doctors.FirstOrDefaultAsync(x => x.DoctorId.Equals(id));

            if (existingDoctor == null)
            {
                return NotFound();
            }

            mapper.Map(updatedDoctorDTO, existingDoctor);

            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctorToDelete = await db.Doctors.FirstOrDefaultAsync(x => x.DoctorId.Equals(id));

            if (doctorToDelete == null)
            {
                return NotFound();
            }
            db.Remove(doctorToDelete);
            await db.SaveChangesAsync();

            return NoContent();
        }

    }
}
