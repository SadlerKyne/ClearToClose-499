using ClearToClose.Data;
using Microsoft.EntityFrameworkCore;

namespace ClearToClose.Services;

// Builds the dashboard numbers for one user. Only that user's rows are read.
public class DashboardService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
    public async Task<DashboardSummary> GetSummaryAsync(string userId)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        var incomes = await db.Incomes.Where(i => i.UserId == userId).ToListAsync();
        var bills = await db.Bills.Where(b => b.UserId == userId).ToListAsync();

        // Debt payments get added here once the Debts feature is on main.
        return new DashboardSummary
        {
            Income = IncomeService.MonthlyTotal(incomes),
            Bills = BillService.MonthlyTotal(bills)
        };
    }
}
