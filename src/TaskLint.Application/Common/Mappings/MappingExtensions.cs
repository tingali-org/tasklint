﻿using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TaskLint.Application.Common.Models;

namespace TaskLint.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PagedResult<TDestination>> PagedResultAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PagedResult<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}
