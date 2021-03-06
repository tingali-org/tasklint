﻿using MediatR;

using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

using TaskLint.Application.Common.Interfaces;
using TaskLint.Application.Common.Models;
using TaskLint.Domain.Common;

namespace TaskLint.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private const string PublishInformationMessage = "Publishing domain event. Event - {event}";
        private readonly ILogger<DomainEventService> _logger;
        private readonly IPublisher _mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            _logger.LogInformation(PublishInformationMessage, domainEvent.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}
