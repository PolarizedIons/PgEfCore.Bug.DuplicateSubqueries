using System.ComponentModel.DataAnnotations;

namespace PgEfCore.Bug.DuplicateSubqueries;

public class FirstEntity
{
    [Key] public Guid Id { get; set; }
    public Guid SecondEntityId { get; set; }
    public int ComputedProperty1 => 0;
    public int ComputedProperty2 => 0;
    public int ComputedProperty3 => 0;

    public ICollection<FirstEntityInclude> ToInclude { get; set; } = null!;
}
