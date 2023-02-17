using Microsoft.EntityFrameworkCore;

namespace PgEfCore.Bug.DuplicateSubqueries;

public class Database : DbContext
{
    public DbSet<FirstEntity> FirstEntities { get; set; } = null!;
    public DbSet<FirstEntityInclude> FirstEntityIncludes { get; set; } = null!;
    public DbSet<SecondEntity> SecondEntities { get; set; } = null!;

    public Database(DbContextOptions<Database> options) : base(options)
    {
    }
}
