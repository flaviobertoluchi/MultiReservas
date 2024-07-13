using Microsoft.EntityFrameworkCore;

namespace MultiReservas.Data.Context
{
    public class SqlServerContext(DbContextOptions options) : BaseContext(options) { }
}
