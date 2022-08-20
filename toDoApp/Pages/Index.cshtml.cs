using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using toDoApp.Models;
using toDoApp.Services;

namespace toDoApp.Pages
{
    public class IndexModel : PageModel
    {


        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger, JsonDbService JsonDbService)
        {
            _logger = logger;
            _jsonDbService = JsonDbService;
        }
        JsonDbService _jsonDbService;

        public List<JsonDbModel> toDos;


        public IActionResult? OnGet()
        {
            //var userName = HttpContext.Session.GetString(SessionKeyUserName);
            //var Pass = HttpContext.Session.GetString(SessionKeyPass);
            //toDos = _jsonDbService.GetToDos();
            //return RedirectToPage("View");
            return null;
        }

    }
}