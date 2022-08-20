using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using toDoApp.Models;
using toDoApp.Services;

namespace toDoApp.Pages
{
    public class LoginModel : PageModel
    {
        public const string SessionKeyUserName = "_UserName";
        public const string SessionKeyPass = "_Pass";

        public UserLoginService _userLoginServis;
        public LoginModel(UserLoginService userLoginService)
        {
            this._userLoginServis = userLoginService;
        }

        //Post ile alacağım datamın tutalacağı model
        [BindProperty]
        public UserLoginModel LoginUser { get; set; }

        public List<UserLoginModel> Userss;
        public IActionResult? OnGet() //sayfa yüklendiğinde
        {

            if (HttpContext.Session.GetString("_UserName") != null)
            {
                return RedirectToPage("/View");
            }
            else
            {
                return null;
            }
        }

        [TempData]
        public string StatusMessageLogin { get; set; }
        [TempData]
        public string StatusMessageLoginTitle { get; set; }

        public IActionResult OnPostForm()
        {
            if (LoginUser.userName != null && LoginUser.password != null)
            {
                Userss = _userLoginServis.GetUser();
                foreach (var user in Userss)
                {
                    if (user.userName == LoginUser.userName && user.password == LoginUser.password)
                    {
                        if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyUserName)))
                        {
                            HttpContext.Session.SetString(SessionKeyUserName, user.userName);
                            HttpContext.Session.SetString(SessionKeyPass, user.password);
                            return RedirectToPage("/View");
                        }

                    }
                }
                StatusMessageLoginTitle = "Bad News";
                StatusMessageLogin = "Username or password is incorrect, try again.";
                return RedirectToPage("/Login");
            }
            else
            {
                StatusMessageLoginTitle = "Bad News";
                StatusMessageLogin = "You entered an empty value. Please fill in the fields.";
                return RedirectToPage("/Login");
            }
        }
    }
}
