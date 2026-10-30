using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;
using TaskAPI.Services;

namespace TaskAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TasksController : Controller
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<TaskItem>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = _service.GetById(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public ActionResult<TaskItem> Create(TaskItem task)
    {
        var created = _service.Create(task);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TaskItem task) => 
        _service.Update(id, task) ? NoContent() : NotFound();

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) => _service.Delete(id) ? NoContent() : NotFound();
}