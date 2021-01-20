using System;

using Template.Application.Common.Interfaces;

namespace Template.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
