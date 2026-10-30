using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;
using TaskAPI.Services;

namespace TaskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _service.GetByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {
        var created = await _service.CreateAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem task)
        => await _service.UpdateAsync(id, task) ? NoContent() : NotFound();

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}