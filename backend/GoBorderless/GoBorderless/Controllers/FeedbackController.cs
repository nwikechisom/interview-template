using System.ComponentModel.DataAnnotations;
using GoBorderless.Data;
using GoBorderless.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoBorderless.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/feedback")]
public class FeedbackController: ControllerBase
{
    private readonly GetBorderlessDbContext _context;

    public FeedbackController(GetBorderlessDbContext dbContext)
    {
        _context = dbContext;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostData(FeedbackRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var feedback = new Feedback
            {
                Rating = request.Rating,
                Comment = request.Comment,
            };
        
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
        
            return Ok(new {Message = "Feedback added successfully", FeedbackId = feedback.Id});
        }
        catch (Exception e)
        {
            return BadRequest(new {Message = e.Message});
        }

    }

    [HttpGet]
    public async Task<IActionResult> GetFeedback()
    {
        var feedbacks = await _context.Feedbacks.ToListAsync();
        if (!feedbacks.Any())
        {
            return Ok(new {Message = "No feedbacks found", Feedbacks = new List<Feedback>()});
        }
        return Ok(new {Message = "Feedbacks retrieved successfully", Feedbacks = feedbacks});
    }
}
