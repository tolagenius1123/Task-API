using TaskAPI.Models;

namespace TaskAPI.Services;

public class TaskService : ITaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public List<TaskItem> GetAll() => _tasks;
    public TaskItem? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);
    
    public TaskItem Create(TaskItem task)
    {
        task.Id = _nextId++;
        task.CreatedAt = DateTime.UtcNow;
        _tasks.Add(task);
        return task;
    }

    public bool Update(int id, TaskItem task)
    {
        var existing = GetById(id);
        if (existing is null) return false;

        existing.Title = task.Title;
        existing.IsCompleted = task.IsCompleted;
        return true;
    }

    public bool Delete(int id)
    {
        var task = GetById(id);
        if (task is null) return false;
        _tasks.Remove(task);
        return true;
    }
}