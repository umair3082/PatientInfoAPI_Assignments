using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientInfoAPI_Assignments.DTOs;
using PatientInfoAPI_Assignments.Entites;
using PatientInfoAPI_Assignments.Utilities;

namespace PatientInfoAPI_Assignments.Controllers
{
    [Route("api/Disease")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public DiseaseController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        [HttpPost("CreateDisease")]
        public IActionResult CreateDisease([FromBody] DiseaseCreationDTO diseaseDTO)
        {
            if (diseaseDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var diseaseEntity = mapper.Map<Disease>(diseaseDTO);

            db.Add(diseaseEntity);
            db.SaveChanges();

            return Ok(diseaseEntity.DiseaseId);
        }

        [HttpGet("GetAllDiseases")]
        public async Task<ActionResult<List<DiseaseDTO>>> GetAllDiseases([FromQuery] PaginationDTO paginationDTO)
        {
            var query = db.Diseases.AsQueryable();
            if (query != null)
            {
                //if (!string.IsNullOrEmpty(searchDTO.DiseaseName))
                //    query = query.Where(x => x.DiseaseName.Contains(searchDTO.DiseaseName));

                await HttpContext.InsertParamsPaginationInHeader(query);
                var data = await query
                        .OrderByDescending(x => x.DiseaseId)
                        .Paginate(paginationDTO)
                        .ToListAsync();
                var finalLst = mapper.Map<List<DiseaseDTO>>(data);
                return finalLst;
            }
            else
                return new List<DiseaseDTO>() { };
        }

        [HttpGet("GetTotalDiseases")]
        public async Task<ActionResult<List<DiseaseDTO>>> GetAllDiseases()
        {
            var query = db.Diseases.AsQueryable();
            if (query != null)
            {
                await HttpContext.InsertParamsPaginationInHeader(query);
                var data = await query
                        .OrderByDescending(x => x.DiseaseId)
                        .ToListAsync();
                var finalLst = mapper.Map<List<DiseaseDTO>>(data);
                return finalLst;
            }
            else
                return new List<DiseaseDTO>() { };
        }

        [HttpPost("SearchDiseasesByName")]
        public async Task<ActionResult<List<DiseaseDTO>>> SearchDiseasesByName([FromQuery] PaginationDTO paginationDTO, [FromForm] DiseaseSearchDTO searchDTO)
        {
            var query = db.Diseases.AsQueryable();
            if (query != null)
            {
                if (!string.IsNullOrEmpty(searchDTO.DiseaseName))
                    query = query.Where(x => x.DiseaseName.Contains(searchDTO.DiseaseName));

                await HttpContext.InsertParamsPaginationInHeader(query);
                var data = await query
                        .OrderByDescending(x => x.DiseaseId)
                        .Paginate(paginationDTO)
                        .ToListAsync();
                var finalLst = mapper.Map<List<DiseaseDTO>>(data);
                return finalLst;
            }
            else
                return new List<DiseaseDTO>() { };
        }

        [HttpGet("GetDiseaseById/{id}")]
        public async Task<IActionResult> GetDiseaseById(int id)
        {
            var disease = await db.Diseases.FirstOrDefaultAsync(x => x.DiseaseId == id);

            if (disease == null)
            {
                return NotFound();
            }

            var diseaseDTO = mapper.Map<DiseaseDTO>(disease);

            return Ok(diseaseDTO);
        }

        [HttpPut("UpdateDisease/{id}")]
        public async Task<IActionResult> UpdateDisease(int id, [FromBody] DiseaseDTO updatedDiseaseDTO)
        {
            var existingDisease = await db.Diseases.FirstOrDefaultAsync(x => x.DiseaseId == id);

            if (existingDisease == null)
            {
                return NotFound();
            }

            mapper.Map(updatedDiseaseDTO, existingDisease);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteDisease/{id}")]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            var diseaseToDelete = await db.Diseases.FirstOrDefaultAsync(x => x.DiseaseId == id);

            if (diseaseToDelete == null)
            {
                return NotFound();
            }

            db.Remove(diseaseToDelete);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
