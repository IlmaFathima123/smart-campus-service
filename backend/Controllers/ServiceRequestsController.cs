using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRequestsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ServiceRequestsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/ServiceRequests
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequests()
    {
        return await _context.ServiceRequests
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    // POST: api/ServiceRequests
    [HttpPost]
    public async Task<ActionResult<ServiceRequest>> CreateServiceRequest(
        ServiceRequest request)
    {
        request.Status = "Pending";
        request.CreatedAt = DateTime.UtcNow;

        _context.ServiceRequests.Add(request);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetServiceRequests),
            new { id = request.Id },
            request);
    }
}