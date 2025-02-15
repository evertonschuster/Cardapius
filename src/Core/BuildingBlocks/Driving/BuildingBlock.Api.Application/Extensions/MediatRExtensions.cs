﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlock.Api.Application.Extensions
{
    public static class MediatRExtensions
    {
        public static void AddApplicationMediatr(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
