using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using PgEfCore.Bug.DuplicateSubqueries;

var rootSp = new ServiceCollection()
    .AddDbContext<Database>(opts =>
    {
        var cs = new NpgsqlConnectionStringBuilder
        {
            Database = "efcorebug",
            Host = "localhost",
            Port = 5432,
            Username = "postgres",
            Password = "postgres"
        };

        opts.LogTo(Console.WriteLine);
        opts.UseNpgsql(cs.ToString());
    })
    .BuildServiceProvider(true);

using var sp = rootSp.CreateScope();

var db = sp.ServiceProvider.GetRequiredService<Database>();

await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();

Console.WriteLine("#############################################################");
Console.WriteLine("######################  NOT DOING IT ########################");
Console.WriteLine("#############################################################");
await db.FirstEntities
    .Include(x => x.ToInclude)
    .Join(
        db.SecondEntities,
        first => first.SecondEntityId,
        second => second.FirstEntityId,
        (first, second) => new {first, second}
    )
    .ToListAsync();


Console.WriteLine("#############################################################");
Console.WriteLine("########################  DOING IT ##########################");
Console.WriteLine("#############################################################");
await db.FirstEntities
    .Include(x => x.ToInclude)
    .Join(
        db.SecondEntities,
        first => first.SecondEntityId,
        second => second.FirstEntityId,
        (first, second) => new {
            first,
            haha1 = first.ComputedProperty1,
            haha2 = first.ComputedProperty2,
            haha3 = first.ComputedProperty3,
        }
    )
    .ToListAsync();
