using Microsoft.EntityFrameworkCore;
using TaskAPI.Data;
using TaskAPI.Models;

namespace TaskAPI.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;

    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TaskItem>> GetAllAsync()
        => await _db.Tasks.ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(int id)
        => await _db.Tasks.FindAsync(id);

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        task.CreatedAt = DateTime.UtcNow;
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();   // the INSERT happens here
        return task;                    // EF Core has filled in task.Id
    }

    public async Task<bool> UpdateAsync(int id, TaskItem task)
    {
        var existing = await _db.Tasks.FindAsync(id);
        if (existing is null) return false;

        existing.Title = task.Title;
        existing.IsCompleted = task.IsCompleted;
        await _db.SaveChangesAsync();   // the UPDATE happens here
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Tasks.FindAsync(id);
        if (existing is null) return false;

        _db.Tasks.Remove(existing);
        await _db.SaveChangesAsync();   // the DELETE happens here
        return true;
    }
}