using System.ComponentModel.DataAnnotations;

namespace PgEfCore.Bug.DuplicateSubqueries;

public class FirstEntityInclude
{
    [Key] public Guid Id { get; set; }
    public string Col1 { get; set; } = null!;
    public string Col2 { get; set; } = null!;
    public string Col3 { get; set; } = null!;
    public string Col4 { get; set; } = null!;
    public string Col5 { get; set; } = null!;
}
