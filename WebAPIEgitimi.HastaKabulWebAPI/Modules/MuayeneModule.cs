using Carter;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using WebAPIEgitimi.HastaKabulWebAPI.Context;
using WebAPIEgitimi.HastaKabulWebAPI.DTOS;
using WebAPIEgitimi.HastaKabulWebAPI.Models;

namespace WebAPIEgitimi.HastaKabulWebAPI.Modules;

public sealed class MuayeneModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group
            .MapGroup("muayeneler")
            .WithTags("Muayeneler")
            .RequireRateLimiting("fixed");

        app.MapGet(string.Empty, async (
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var res = await dbContext
                        .Muayeneler
                        //.Include(i => i.Patient)
                        .Select(s => new MuayeneDto
                        {
                            Id = s.Id,
                            PatientId = s.PatientId,
                            PatientName = s.Patient!.FirstName + s.Patient.LastName,
                            CreatedAt = s.CreatedAt,
                            Status = s.PatientStatus.Value,
                            TaburcuDate = s.TaburcuDate
                        })
                        .OrderByDescending(p => p.CreatedAt)
                        .ToListAsync(cancellationToken);

            return Result<List<MuayeneDto>>.Succeed(res);
        }).Produces<Result<List<MuayeneDto>>>();

        app.MapGet("/{id:guid}", async (
            Guid id,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var res = await dbContext.Muayeneler
            .Where(x => x.Id == id)
            .Select(s => new
            {
                Id = s.Id,
                PatientId = s.PatientId,
                PatientName = s.Patient!.FirstName + s.Patient.LastName,
                CreatedAt = s.CreatedAt,
                Status = s.PatientStatus.Value,
                TaburcuDate = s.TaburcuDate,
                Epikriz = s.Epikriz
            })
            .FirstOrDefaultAsync(cancellationToken);

            if (res is null)
            {
                return Results.NotFound(Result<object>.Failure("Muayene bulunamadı"));
            }
            return Results.Ok(Result<object>.Succeed(res));
        }).Produces<Result<object>>();

        app.MapPost(string.Empty, async (
            MuayeneCreateDto request,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var muayene = request.Adapt<Muayene>();

            dbContext.Muayeneler.Add(muayene);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result<string>.Succeed("Muayene başarıyla kaydedildi"));
        }).Produces<Result<string>>();

        app.MapPut(string.Empty, async (
            MuayeneUpdatedto request,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var muayene = await dbContext.Muayeneler.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (muayene is null)
            {
                var res = Result<string>.Failure("Muayene bulunamadı");
                return Results.NotFound(res);
            }
            request.Adapt(muayene);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result<string>.Succeed("Muayene başarıyla güncellendi"));
        }).Produces<Result<string>>();

        app.MapDelete("/{id:guid}", async (
           Guid id,
           ApplicationDbContext dbContext,
           CancellationToken cancellationToken) =>
        {
            var muayene = await dbContext.Muayeneler.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (muayene is null)
            {
                var res = Result<string>.Failure("Muayene bulunamadı");
                return Results.NotFound(res);
            }

            if (muayene.PatientStatus == PatientStatusEnum.TaburcuOldu)
            {
                var res = Result<string>.Failure("Taburcu olmuş hastanın muayene bilgisi silinemez");
                return Results.BadRequest(res);
            }

            muayene.IsDeleted = true;
            dbContext.Update(muayene);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result<string>.Succeed("Muayene başarıyla silindi"));
        }).Produces<Result<string>>();

        app.MapGet("/muayene-al/{id:guid}", async (
           Guid id,
           ApplicationDbContext dbContext,
           CancellationToken cancellationToken) =>
        {
            var muayene = await dbContext.Muayeneler.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (muayene is null)
            {
                var res = Result<string>.Failure("Muayene bulunamadı");
                return Results.NotFound(res);
            }

            if (muayene.PatientStatus == PatientStatusEnum.MuayeneOluyor)
            {
                var res = Result<string>.Failure("Hasta zaten muayene oluyor");
                return Results.BadRequest(res);
            }

            muayene.PatientStatus = PatientStatusEnum.MuayeneOluyor;
            dbContext.Update(muayene);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result<string>.Succeed("Hasta muayene olmaya başladı"));
        }).Produces<Result<string>>();

        app.MapPut("/taburcu-et", async (
           MuayeneTaburcuEtDto request,
           ApplicationDbContext dbContext,
           CancellationToken cancellationToken) =>
        {
            var muayene = await dbContext.Muayeneler.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (muayene is null)
            {
                var res = Result<string>.Failure("Muayene bulunamadı");
                return Results.NotFound(res);
            }

            if (muayene.PatientStatus == PatientStatusEnum.TaburcuOldu)
            {
                var res = Result<string>.Failure("Hasta zaten taburcu olmuş");
                return Results.BadRequest(res);
            }

            muayene.PatientStatus = PatientStatusEnum.TaburcuOldu;
            muayene.Epikriz = request.Epikriz;
            muayene.TaburcuDate = DateTimeOffset.Now;
            dbContext.Update(muayene);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result<string>.Succeed("Hasta taburcu oldu"));
        }).Produces<Result<string>>();
    }
}
