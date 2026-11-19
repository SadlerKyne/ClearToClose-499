namespace ClearToClose.Services;

// Debt-to-income (DTI) math. DTI is monthly debt payments divided by
// monthly income, shown as a percent. Lenders use it to decide if you
// can take on a mortgage.
public static class DtiCalculator
{
    public const decimal ReadyLimit = 36m;
    public const decimal CloseLimit = 43m;

    // Returns the DTI percent rounded to one decimal place,
    // or null when there is no income to divide by.
    public static decimal? Percent(decimal monthlyDebtPayments, decimal monthlyIncome)
    {
        if (monthlyIncome <= 0) return null;
        return Math.Round(monthlyDebtPayments / monthlyIncome * 100m, 1);
    }

    // Ready: 36% or less. Close: over 36% up to 43%. Not Yet: over 43%.
    public static string Status(decimal? percent) => percent switch
    {
        null => "No income yet",
        <= ReadyLimit => "Ready",
        <= CloseLimit => "Close",
        _ => "Not Yet"
    };

    // CSS class for the status badge.
    public static string StatusClass(decimal? percent) => Status(percent) switch
    {
        "Ready" => "dti-ready",
        "Close" => "dti-close",
        "Not Yet" => "dti-not-yet",
        _ => "dti-none"
    };
}
