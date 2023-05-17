using Microsoft.EntityFrameworkCore;

namespace Web_Projekat_PR111_2019.Data
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
    }
}
