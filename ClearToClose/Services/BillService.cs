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

    // Returns false if the bill doesn't exist or belongs to another user.
    public async Task<bool> UpdateAsync(string userId, int id, Bill changes)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        var bill = await db.Bills.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        if (bill is null) return false;

        bill.Name = changes.Name.Trim();
        bill.Amount = changes.Amount;
        bill.DueDay = changes.DueDay;
        bill.Category = changes.Category;
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string userId, int id)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        var bill = await db.Bills.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        if (bill is null) return false;

        db.Bills.Remove(bill);
        await db.SaveChangesAsync();
        return true;
    }

    // The current month as text, like "2026-11". A bill counts as paid
    // only when its PaidMonth matches this, so every bill resets each month.
    public static string CurrentMonth() => DateTime.Today.ToString("yyyy-MM");

    // Marks a bill paid for the current month, or unmarks it.
    public async Task<bool> SetPaidAsync(string userId, int id, bool paid)
    {
        using var db = await dbFactory.CreateDbContextAsync();
        var bill = await db.Bills.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        if (bill is null) return false;

        bill.PaidMonth = paid ? CurrentMonth() : null;
        await db.SaveChangesAsync();
        return true;
    }

    public static bool IsPaidThisMonth(Bill bill) => bill.PaidMonth == CurrentMonth();

    // Total of all bills for one month.
    public static decimal MonthlyTotal(IEnumerable<Bill> bills) => bills.Sum(b => b.Amount);

    // Total of the bills not paid yet this month.
    public static decimal StillDue(IEnumerable<Bill> bills) =>
        bills.Where(b => !IsPaidThisMonth(b)).Sum(b => b.Amount);
}
