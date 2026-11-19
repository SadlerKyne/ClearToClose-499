using ClearToClose.Data;
using Microsoft.EntityFrameworkCore;

namespace ClearToClose.Services;

// Add, edit, delete and list income. Every method takes the logged-in user's id
// and only touches that user's rows, so no one can see or change someone else's income.
public class IncomeService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
    public async Task<List<Income>> GetAllAsync(string userId)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        return await db.Incomes
            .Where(i => i.UserId == userId)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public async Task AddAsync(string userId, string name, decimal monthlyAmount)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        db.Incomes.Add(new Income { UserId = userId, Name = name.Trim(), MonthlyAmount = monthlyAmount });
        await db.SaveChangesAsync();
    }

    // Returns false if the income doesn't exist or belongs to another user.
    public async Task<bool> UpdateAsync(string userId, int id, string name, decimal monthlyAmount)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        var income = await db.Incomes.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
        if (income is null) return false;

        income.Name = name.Trim();
        income.MonthlyAmount = monthlyAmount;
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string userId, int id)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        var income = await db.Incomes.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
        if (income is null) return false;

        db.Incomes.Remove(income);
        await db.SaveChangesAsync();
        return true;
    }

    // Total monthly income. This is the number the DTI will be based on.
    public static decimal MonthlyTotal(IEnumerable<Income> incomes) => incomes.Sum(i => i.MonthlyAmount);
}
