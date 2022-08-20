using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using toDoApp.Models;
using toDoApp.Services;

namespace toDoApp.Pages
{
    public class RegisterModel : PageModel
    {
        public const string SessionKeyUserName = "_UserName";
        public const string SessionKeyPass = "_Pass";

        public UserRegisterService _userRegisterService;

        public RegisterModel(UserRegisterService userRegisterService)
        {
           this._userRegisterService = userRegisterService;
        }

        //Post ile alacağım datamın tutalacağı model
        [BindProperty]
        public UserRegisterModel NewUser { get; set; }

        public void OnGet() //sayfa yüklendiğinde
        {
            
        }

        [TempData]
        public string StatusMessageRegister { get; set; }

        [TempData]
        public string StatusMessageRegisterTitle { get; set; }

        public IActionResult OnPostForm()
        {
            int unique = 0;
            List<UserRegisterModel> allUsers = _userRegisterService.GetUser();

            if(NewUser.userName != null && NewUser.password != null)
            {
                foreach(var user in allUsers)
                {
                    if(user.userName == NewUser.userName)
                    {
                        unique = 1;
                    }
                }
                if (unique == 0)
                {
                    _userRegisterService.NewUser(NewUser);
                    return RedirectToPage("/Login", StatusMessageRegister);
                }
                else
                {
                    StatusMessageRegisterTitle = "Bad News!";
                    StatusMessageRegister = "There is someone with the name you want to register.";
                    return RedirectToPage("/Register");
                }
            }
            else
            {
                StatusMessageRegisterTitle = "Bad News!";
                StatusMessageRegister = "To register, please fill in all the fields completely.";
                HttpContext.Session.SetString("RegisterStatus", "Success");
                return RedirectToPage("/Register");
            }
            
        }
    }
}
