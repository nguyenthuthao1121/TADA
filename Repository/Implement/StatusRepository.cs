using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Net;
using System.Runtime.InteropServices;
using System.Globalization;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;
using static System.Reflection.Metadata.BlobBuilder;
using TADA.Dto.Status;

namespace TADA.Repository.Implement;

public class StatusRepository : IStatusRepository
{
    private readonly TadaContext context;
    public StatusRepository(TadaContext context)
    {
        this.context = context;
    }
    
    public int IdForUserConfirm()
    {
        return context.Statuses.Max(status => status.Id);
    }
    public List<StatusDto> GetStatuses()
    {
        int lastStatusId=context.Statuses.Max(status => status.Id);
        return context.Statuses.Where(status=>status.Id<lastStatusId)
            .Select(status => new StatusDto
            {
                Id= status.Id,
                Name= status.Name,
            }).ToList();
    }
}
