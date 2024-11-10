using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementApp.Controllers;

[Authorize]
public class HomeController : Controller
{
     private readonly ILogger<HomeController> _logger;
     private readonly ITaskService _taskService;

    public HomeController(ILogger<HomeController> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }

    public IActionResult Index(int pageNumber = 1, int pageSize = 10)
    {
        var tasks = _taskService.GetAllTasks()
        .Skip((pageNumber -1) * pageSize)
        .Take(pageSize).ToList();

        return View(tasks);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
