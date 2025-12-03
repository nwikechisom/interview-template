using System.ComponentModel.DataAnnotations;

namespace GoBorderless.Models;

public class FeedbackRequest
{
    [Required, Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    public string Comment { get; set; }
}