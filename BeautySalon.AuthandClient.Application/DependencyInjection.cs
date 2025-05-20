using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using AutoMapper;
using BeautySalon.AuthandClient.Application.Interfaces;

namespace BeautySalon.AuthandClient.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication (this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}