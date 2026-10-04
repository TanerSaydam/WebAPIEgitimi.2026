using Carter;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using WebAPIEgitimi.HastaKabulWebAPI.Context;
using WebAPIEgitimi.HastaKabulWebAPI.DTOS;
using WebAPIEgitimi.HastaKabulWebAPI.Models;

namespace WebAPIEgitimi.HastaKabulWebAPI.Modules;

public sealed class PatientModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/patients").WithTags("Patients").RequireRateLimiting("fixed");

        app.MapGet(string.Empty, async (
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var res = await dbContext
                        .Patients.OrderBy(p => p.FirstName)
                        .ToListAsync(cancellationToken);

            return Result<List<Patient>>.Succeed(res);
        }).Produces<Result<List<Patient>>>();

        app.MapGet("/{id:guid}", async (
            Guid id,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var res = await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (res is null)
            {
                return Results.NotFound(Result<Patient>.Failure("Hasta bulunamadı"));
            }
            return Results.Ok(Result<Patient>.Succeed(res));
        }).Produces<Result<Patient>>();

        app.MapPost(string.Empty, async (
            PatientCreateDto request,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            bool isTCNoExists = await dbContext.Patients.AnyAsync(p => p.TCNo == request.TCNo, cancellationToken);
            if (isTCNoExists)
            {
                var res = Result<string>.Failure("Bu TC No ile kayıtlı bir hasta zaten mevcut");
                return Results.BadRequest(res);
            }

            var patient = request.Adapt<Patient>();

            dbContext.Patients.Add(patient);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result<string>.Succeed("Hasta başarıyla kaydedildi"));
        }).Produces<Result<string>>();

        app.MapPut(string.Empty, async (
            PatientUpdateDto request,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var patient = await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (patient is null)
            {
                var res = Result<string>.Failure("Hasta bulunamadı");
                return Results.NotFound();
            }
            request.Adapt(patient);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result<string>.Succeed("Hasta başarıyla güncellendi"));
        }).Produces<Result<string>>();

        app.MapDelete("/{id:guid}", async (
           Guid id,
           ApplicationDbContext dbContext,
           CancellationToken cancellationToken) =>
        {
            var res = await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (res is null)
            {
                return Results.NotFound(Result<string>.Failure("Hasta bulunamadı"));
            }

            res.IsDeleted = true;
            dbContext.Update(res);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result<string>.Succeed("Hasta başarıyla silindi"));
        }).Produces<Result<string>>();
    }
}