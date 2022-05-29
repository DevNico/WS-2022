using Ardalis.Specification;

namespace ServiceReleaseManager.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
