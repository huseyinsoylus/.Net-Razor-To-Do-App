using System.Text.Json;
using toDoApp.Models;

namespace toDoApp.Services
{
    public class UserLoginService
    {
        public UserLoginService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment;

        //User DB Path
        public string JsonUserDbPath
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "jsonDb", "login.json"); }

        }
        public List<UserLoginModel> GetUser()
        {
            using var json = File.OpenText(JsonUserDbPath);

            //return JsonSerializer.Deserialize<List<ProjectModel>>(json.Re());

            return JsonSerializer.Deserialize<UserLoginModel[]>(json.ReadToEnd()).ToList();

        }
    }
}
