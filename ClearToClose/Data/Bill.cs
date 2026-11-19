using System.ComponentModel.DataAnnotations;

namespace ClearToClose.Data;

// A monthly bill, like rent or the electric bill.
public class Bill
{
    // The categories a bill can have. The Bills page shows these in a dropdown.
    public static readonly string[] Categories =
    [
        "Housing", "Utilities", "Insurance", "Phone and Internet",
        "Transportation", "Food", "Subscriptions", "Other"
    ];

    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be 100 characters or less.")]
    public string Name { get; set; } = "";

    [Range(typeof(decimal), "0.01", "10000000", ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }

    // Day of the month the bill is due (1-31).
    [Range(1, 31, ErrorMessage = "Due day must be between 1 and 31.")]
    public int DueDay { get; set; } = 1;

    [Required(ErrorMessage = "Pick a category.")]
    public string Category { get; set; } = "";

    // The month this bill was last paid, like "2026-11". Null if never paid.
    public string? PaidMonth { get; set; }

    // The Identity user who owns this bill. Every query filters on this.
    public string UserId { get; set; } = "";
}
