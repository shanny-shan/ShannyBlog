using blog_common.Context;
using blog_pojo;
using blog_pojo.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace blog_db;

public class DbSaveInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        FillField(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        FillField(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void FillField(DbContext db)
    {
        var uid = BaseContext.GetCurrentId();
        var now = DateTime.Now;

        foreach (var entry in db.ChangeTracker.Entries<TimeEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreateTime = now;
                entry.Entity.UpdateTime = now;

            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdateTime = now;
            }
        }
    }
}