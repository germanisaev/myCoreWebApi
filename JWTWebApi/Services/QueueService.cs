using JWTWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApi.Services
{
    public interface IQueueService
    {
        Task<int> CreateQueueAsync(Grooming model);
    }
    public class QueueService : IQueueService
    {
        private SalonApiContext _context;

        public QueueService(SalonApiContext context)
        {
            _context = context;
        }
        public async Task<int> CreateQueueAsync(Grooming model)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Veterinar", model.VeterinarId));
            parameter.Add(new SqlParameter("@AppointmnetDate", model.Appointment));
            parameter.Add(new SqlParameter
            {
                ParameterName = "@retVal",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Output
            });
            
            int retVal = 0;
            var result = await _context.Database.ExecuteSqlCommandAsync(@"EXEC sp_QueueExist 
                                                                                @Veterinar, 
                                                                                @AppointmnetDate, 
                                                                                @retVal OUTPUT", 
                                                                                parameter.ToArray());
            retVal = Convert.ToInt32(parameter[2].Value);
            
            return retVal;
        }
    }
}
