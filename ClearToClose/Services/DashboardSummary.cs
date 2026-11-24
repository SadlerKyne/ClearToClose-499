namespace ClearToClose.Services;

// The monthly numbers shown on the dashboard.
public class DashboardSummary
{
    public decimal Income { get; init; }
    public decimal Bills { get; init; }
    public decimal DebtPayments { get; init; }

    // What is left each month after bills and debt payments.
    public decimal MoneyLeft => Income - Bills - DebtPayments;
}
