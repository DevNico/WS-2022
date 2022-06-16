using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReleaseManager.SharedKernel;

public abstract class EntityBase
{
  private readonly List<DomainEventBase> _domainEvents = new();
  public int Id { get; set; }

  public DateTime UpdatedAt { get; set; }

  public DateTime CreatedAt { get; set; }

  [NotMapped]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvent(DomainEventBase domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }

  internal void ClearDomainEvents()
  {
    _domainEvents.Clear();
  }
}
