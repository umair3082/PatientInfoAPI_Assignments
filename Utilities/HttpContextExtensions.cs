using Microsoft.EntityFrameworkCore;

namespace PatientInfoAPI_Assignments.Utilities
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParamsPaginationInHeader<T>(this HttpContext http,
                                                                   IQueryable<T> query)
        {
            if (http == null)
            {
                throw new ArgumentNullException(nameof(http));
            }
            double count = await query.CountAsync();
            http.Response.Headers.Add("TotalRecords", count.ToString());
        }
    }
}
