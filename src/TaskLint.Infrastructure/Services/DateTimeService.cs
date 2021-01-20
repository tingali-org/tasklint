using System;

using TaskLint.Application.Common.Interfaces;

namespace TaskLint.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
