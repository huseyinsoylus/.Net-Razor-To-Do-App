using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using toDoApp.Models;
using toDoApp.Services;

namespace toDoApp.Pages
{
    public class AddToDoModel : PageModel
    {
        public JsonDbService jsonDbService;
        public AddToDoModel(JsonDbService jsonDbService)
        {

            this.jsonDbService = jsonDbService;
        }

        [BindProperty] //Veriler bind edilmediği bağlanmadığı sürece geçerli değildir.
        public JsonDbModel toDo { get; set; }

        public List<JsonDbModel> toDos;
        public int end_id { get; set; }
        public string SessionKeyName { get; private set; }

        public IActionResult? OnGet()
        {
            if (HttpContext.Session.GetString("_UserName") != null)
            {
                toDos = jsonDbService.GetToDos();
                foreach (var toDos in toDos)
                {
                    if (toDos.toDoUsername == HttpContext.Session.GetString("_UserName"))
                        end_id = toDos.toDoId;
                    //Console.Write("{0}", end_id);
                }
                return null;
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }
        [TempData]
        public string StatusMessageAddToDo { get; set; }
        [TempData]
        public string StatusMessageAddToDoTitle { get; set; }


        public IActionResult OnPostForm()
        {
            if(toDo.toDoName != null && toDo.toDoUsername != null && toDo.toDoDescription != null)
            {
                StatusMessageAddToDo = "To do has been successfully added.";
                StatusMessageAddToDoTitle = "success";
                jsonDbService.AddToDo(toDo);
                return RedirectToPage("/AddToDo");
            }
            else
            {
                StatusMessageAddToDo = "Fill in the blank fields.";
                StatusMessageAddToDoTitle = "warning";
                jsonDbService.AddToDo(toDo);
                return RedirectToPage("/AddToDo");
            }
        }
    }
}
