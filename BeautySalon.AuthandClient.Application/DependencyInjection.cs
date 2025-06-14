using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using AutoMapper;
using BeautySalon.AuthandClient.Application.Interfaces;
using BeautySalon.AuthandClient.Application.Validators;
using FluentValidation.AspNetCore;

namespace BeautySalon.AuthandClient.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication (this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddValidatorsFromAssemblyContaining<RegisterClientValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginQueryValidator>();
        services.AddFluentValidationAutoValidation();

        return services;
    }
}