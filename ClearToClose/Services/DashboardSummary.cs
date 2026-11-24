namespace ClearToClose.Services;

// The monthly numbers shown on the dashboard.
public class DashboardSummary
{
    public decimal Income { get; init; }
    public decimal Bills { get; init; }
    public decimal DebtPayments { get; init; }
    public int IncomeCount { get; init; }
    public int BillCount { get; init; }

    // True for a new user who hasn't added any income or bills yet.
    public bool IsEmpty => IncomeCount == 0 && BillCount == 0;

    // What is left each month after bills and debt payments.
    public decimal MoneyLeft => Income - Bills - DebtPayments;

    // DTI percent from debt payments and income, or null with no income.
    public decimal? DtiPercent => DtiCalculator.Percent(DebtPayments, Income);
}
