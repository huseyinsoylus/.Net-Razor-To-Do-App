using System.Text.Json;
using toDoApp.Models;

namespace toDoApp.Services
{
    public class UserRegisterService
    {
        public UserRegisterService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment;

        //User DB Path
        public string JsonUserDbPath
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "jsonDb", "login.json"); }

        }
        public List<UserRegisterModel> GetUser()
        {
            using var json = File.OpenText(JsonUserDbPath);

            //return JsonSerializer.Deserialize<List<ProjectModel>>(json.Re());

            return JsonSerializer.Deserialize<UserRegisterModel[]>(json.ReadToEnd()).ToList();

        }
        public void NewUser(UserRegisterModel newUser)
        {
            List<UserRegisterModel> users = GetUser();

            users.Add(newUser);

            using var json = File.OpenWrite(JsonUserDbPath);
            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<UserRegisterModel>>(jsonwriter, users);
        }
    }
}
