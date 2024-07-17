using Microsoft.EntityFrameworkCore;

namespace MultiReservas.Data.Context
{
    public class MySQLContext(DbContextOptions options) : BaseContext(options) { }
}
