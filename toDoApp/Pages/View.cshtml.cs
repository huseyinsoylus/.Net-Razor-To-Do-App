using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using toDoApp.Models;
using toDoApp.Services;

namespace toDoApp.Pages
{
    public class ViewModel : PageModel
    {
        public ViewModel(JsonDbService JsonDbService)
        {
            _jsonDbService = JsonDbService;
        }
        JsonDbService _jsonDbService;

        public List<JsonDbModel> toDos;

        //public void OnGet()
        //{
        //    toDos = _jsonDbService.GetToDos();
        //}
        public IActionResult? OnGet()
        {
            if (HttpContext.Session.GetString("_UserName") != null)
            {
                toDos = _jsonDbService.GetToDos();
                return null;
            }
            else
            {
                return RedirectToPage("/Login");
            }
            //return RedirectToPage("/Login");

        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("_UserName");
            HttpContext.Session.Remove("_Pass");
            //HttpContext.Session.Remove("*");
            return RedirectToPage("/Index");
        }

        [BindProperty]
        public string postToDoId { get; set; }
        [TempData]
        public string StatusMessageDelComp { get; set; }
        [TempData]
        public string StatusMessageDelCompTitle { get; set; }
        public IActionResult OnPostDelete()
        {
            //Console.WriteLine("{0}",postToDoId);
            _jsonDbService.DeleteToDo(Convert.ToInt32(postToDoId), HttpContext);
            StatusMessageDelComp = "The ToDo with id "+ postToDoId +" has been deleted.";
            StatusMessageDelCompTitle = "success";
            return RedirectToPage("/View");
        }

        public IActionResult OnPostComplete()
        {
            _jsonDbService.CompleteToDo(Convert.ToInt32(postToDoId), HttpContext);
            StatusMessageDelComp = "The ToDo with id " + postToDoId + " has been updated.";
            StatusMessageDelCompTitle = "success";
            return RedirectToPage("/View");
        }
    }
}
