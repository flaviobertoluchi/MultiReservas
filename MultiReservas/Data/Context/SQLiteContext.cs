using Microsoft.EntityFrameworkCore;

namespace MultiReservas.Data.Context
{
    public class SQLiteContext(DbContextOptions options) : BaseContext(options) { }
}
