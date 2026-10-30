using TaskAPI.Models;

namespace TaskAPI.Services;

public interface ITaskService
{
    List<TaskItem> GetAll();
    TaskItem? GetById(int id);
    TaskItem Create(TaskItem task);
    bool Update(int id, TaskItem task);
    bool Delete(int id);
}