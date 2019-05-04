using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.SeedWork
{
    public abstract class Entity
    {
        // DDD Patterns comment
        // Use domain events to explicitly implement side effects of changes within your domain.
        // In other words, Use domain events to explicitly implement side effects across multiple aggregates.
        // A domain event is, something that happened in the domain that you want other parts of the same domain (in-process) to be aware of.
        // An important benefit of domain events is that side effects can be expressed explicitly.
        // It’s important to ensure that, just like a database transaction, either all the operations related to a domain event finish successfully or none of them do.
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }



    }
}
