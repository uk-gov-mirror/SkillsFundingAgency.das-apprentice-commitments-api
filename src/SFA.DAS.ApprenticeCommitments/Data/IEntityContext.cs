using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public interface IEntityContext<T> where T : class
    {
        DbSet<T> Entitites { get; }

        EntityEntry<T> Add(T entity) => Entitites.Add(entity);

        ValueTask<EntityEntry<T>> AddAsync(T entity, CancellationToken cancellationToken = default)
            => Entitites.AddAsync(entity, cancellationToken);
    }
}