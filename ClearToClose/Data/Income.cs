using System.ComponentModel.DataAnnotations;

namespace ClearToClose.Data;

// One income source for a user, like a paycheck or a side job.
public class Income
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be 100 characters or less.")]
    public string Name { get; set; } = "";

    [Range(typeof(decimal), "0.01", "10000000", ErrorMessage = "Monthly amount must be greater than 0.")]
    public decimal MonthlyAmount { get; set; }

    // The Identity user who owns this income. Every query filters on this.
    public string UserId { get; set; } = "";
}
