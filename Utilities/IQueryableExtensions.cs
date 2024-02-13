using PatientInfoAPI_Assignments.DTOs;

namespace PatientInfoAPI_Assignments.Utilities
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationDTO paginationDTO)
        {

            return query
                .Skip((paginationDTO.page - 1) * paginationDTO.pageSize)
                .Take(paginationDTO.pageSize)
                .AsQueryable();
        }
    }
}
