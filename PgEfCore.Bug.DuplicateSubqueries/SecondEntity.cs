using System.ComponentModel.DataAnnotations;

namespace PgEfCore.Bug.DuplicateSubqueries;

public class SecondEntity
{
    [Key] public Guid Id { get; set; }
    public Guid FirstEntityId { get; set; }
}
