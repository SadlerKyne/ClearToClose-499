using ClearToClose.Data;
using Microsoft.EntityFrameworkCore;

namespace ClearToClose.Services;

// Add, edit, delete and list bills. Every method takes the logged-in user's id
// and only touches that user's rows.
public class BillService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
    // Bills sorted by the day of the month they are due.
    public async Task<List<Bill>> GetAllAsync(string userId)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        return await db.Bills
            .Where(b => b.UserId == userId)
            .OrderBy(b => b.DueDay)
            .ThenBy(b => b.Name)
            .ToListAsync();
    }

    public async Task AddAsync(string userId, Bill bill)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        db.Bills.Add(new Bill
        {
            UserId = userId,
            Name = bill.Name.Trim(),
            Amount = bill.Amount,
            DueDay = bill.DueDay,
            Category = bill.Category
        });
        await db.SaveChangesAsync();
    }
}
