using Microsoft.EntityFrameworkCore;

namespace MultiReservas.Data.Context
{
    public class PostgreSQLContext(DbContextOptions options) : BaseContext(options) { }
}
