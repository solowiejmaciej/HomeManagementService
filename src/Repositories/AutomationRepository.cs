#region

using System.Linq.Expressions;
using HomeManagementService.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace HomeManagementService.Repositories;

public class AutomationRepository : IAutomationRepository
{
    private readonly AutomationsDbContext _context;

    public AutomationRepository(AutomationsDbContext context)
    {
        _context = context;
    }

    public async Task Add(Automation? automation)
    {
        var res = await _context.Automations.AddAsync(automation);
        await _context.SaveChangesAsync();
        Console.WriteLine(res);
    }

    public async Task<List<Automation>?> GetAll()
    {
        var res = await _context.Automations.ToListAsync();
        return res;
    }

    public async Task<List<Automation?>?> GetByCondition(Expression<Func<Automation?, bool>> expression)
    {
        return await _context.Automations.Where(expression).ToListAsync();
    }

    public async Task<Automation?> GetById(Guid id)
    {
        return await _context.Automations.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task RemoveAsync(Guid requestId)
    {
        var automation = _context.Automations.FirstOrDefault(a => a.Id == requestId);
        if (automation != null)
        {
            _context.Automations.Remove(automation);
            await _context.SaveChangesAsync();
        }
    }
}

public interface IAutomationRepository
{
    Task Add(Automation? automation);
    Task<List<Automation>?> GetAll();
    Task<List<Automation?>?> GetByCondition(Expression<Func<Automation?, bool>> expression);
    Task<Automation?> GetById(Guid id);
    Task RemoveAsync(Guid requestId);
}