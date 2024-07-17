using Microsoft.EntityFrameworkCore;

namespace MultiReservas.Data.Context
{
    public class OracleContext(DbContextOptions options) : BaseContext(options) { }
}
