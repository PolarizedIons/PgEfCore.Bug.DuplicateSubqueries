# npgsql + efcore bug

## Description

When doing an: 
 - include on a collection
 - a join on that
 - access a "computed" property on something in the join
 - while also returning that "something" directly

The `left join` to get the include gets duplicated by `N + 1` amount of times,
where `N` is the amount of computed properties

## Example

With the following linq:
```csharp
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
```

the generated SQL is:

```postgresql
SELECT f."Id", f."SecondEntityId", s."Id", f0."Id", f0."Col1", f0."Col2", f0."Col3", f0."Col4", f0."Col5", f0."FirstEntityId", s."FirstEntityId", f1."Id", f1."Col1", f1."Col2", f1."Col3", f1."Col4", f1."Col5", f1."FirstEnt
ityId", f2."Id", f2."Col1", f2."Col2", f2."Col3", f2."Col4", f2."Col5", f2."FirstEntityId", f3."Id", f3."Col1", f3."Col2", f3."Col3", f3."Col4", f3."Col5", f3."FirstEntityId"
      FROM "FirstEntities" AS f
      INNER JOIN "SecondEntities" AS s ON f."SecondEntityId" = s."FirstEntityId"
      LEFT JOIN "FirstEntityIncludes" AS f0 ON f."Id" = f0."FirstEntityId"
      LEFT JOIN "FirstEntityIncludes" AS f1 ON f."Id" = f1."FirstEntityId"
      LEFT JOIN "FirstEntityIncludes" AS f2 ON f."Id" = f2."FirstEntityId"
      LEFT JOIN "FirstEntityIncludes" AS f3 ON f."Id" = f3."FirstEntityId"
      ORDER BY f."Id", s."Id", f0."Id", f1."Id", f2."Id"
```

## Issue link
https://github.com/npgsql/efcore.pg/issues/2655
