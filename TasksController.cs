using Microsoft.AspNetCore.Mvc;
using TaskManagementSystemm.Domain;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class TasksController : Controller
{
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        [ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _repo;

    public TasksController(ITaskRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = _repo.GetById(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        _repo.Add(task);
        _repo.Save();
        return Ok(task);
    }

    [HttpPut]
    public IActionResult Update(TaskItem task)
    {
        _repo.Update(task);
        _repo.Save();
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repo.Delete(id);
        _repo.Save();
        return Ok();
    }
}
// GET → يفتح الصفحة
[HttpGet]
public IActionResult Create()
{
    return View();
}
// POST → يستقبل البيانات
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(TaskItem task)
{
    _repo.Add(task);
    _repo.Save();

    return RedirectToAction("Index");
}