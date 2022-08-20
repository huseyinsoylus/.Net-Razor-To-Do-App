using System.Text.Json;
using toDoApp.Models;

namespace toDoApp.Services
{
    public class JsonDbService
    {
        public JsonDbService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment;

        //Data DB Path
        public string JsonDbPath
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "jsonDb", "jsonDb.json"); }

        }
        public List<JsonDbModel> GetToDos()
        {
            using var json = File.OpenText(JsonDbPath);

            //return JsonSerializer.Deserialize<List<ProjectModel>>(json.Re());

            return JsonSerializer.Deserialize<JsonDbModel[]>(json.ReadToEnd()).ToList();

        }
        public void AddToDo(JsonDbModel newToDo)
        {
            List<JsonDbModel> toDos = GetToDos();

            toDos.Add(newToDo);

            using var json = File.OpenWrite(JsonDbPath);
            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<JsonDbModel>>(jsonwriter, toDos);
        }

        public void DeleteToDo(int deleteToDo_Id, HttpContext httpContext)
        {
            //Kullanıcılara göre silmek zorundayız.
            List<JsonDbModel> toDos = GetToDos();
            JsonDbModel query = toDos.FirstOrDefault(x => (x.toDoUsername == httpContext.Session.GetString("_UserName") & x.toDoId == deleteToDo_Id));
            if (query != null)
            {
                Console.WriteLine("Delete İçerisi");
                toDos.Remove(query);
                using var json = File.Create(JsonDbPath);
                Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
                JsonSerializer.Serialize<List<JsonDbModel>>(jsonwriter, toDos);
            }

        }

        public void CompleteToDo(int completeToDo_Id, HttpContext httpContext)
        {
            //Console.WriteLine("Complate içerisindeyiz");
            List<JsonDbModel> toDos = GetToDos();
            JsonDbModel query = toDos.FirstOrDefault(x => (x.toDoUsername == httpContext.Session.GetString("_UserName") & x.toDoId == completeToDo_Id));
            //JsonDbModel query2 = toDos.FirstOrDefault(x => x.toDoId == completeToDo_Id);

            if (query != null)
            {
                int lastToDoStatus = toDos[toDos.FindIndex(x => (x.toDoId == completeToDo_Id) & x.toDoUsername == httpContext.Session.GetString("_UserName"))].toDoStatus;
                if (lastToDoStatus == 1)
                    toDos[toDos.FindIndex(x => (x.toDoId == completeToDo_Id) & x.toDoUsername == httpContext.Session.GetString("_UserName"))].toDoStatus = 0;
                else
                    toDos[toDos.FindIndex(x => (x.toDoId == completeToDo_Id) & x.toDoUsername == httpContext.Session.GetString("_UserName"))].toDoStatus = 1;


                using var json = File.Create(JsonDbPath);
                Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
                JsonSerializer.Serialize<List<JsonDbModel>>(jsonwriter, toDos);
            }
        }
    }
}
