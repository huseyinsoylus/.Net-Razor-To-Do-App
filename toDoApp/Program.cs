using toDoApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<JsonDbService>();
builder.Services.AddTransient<UserLoginService>();
builder.Services.AddTransient<UserRegisterService>();


//Session State
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30*60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//

//builder.Services.AddRazorPages(options =>
//{
//    options.RootDirectory = "/HomePage";
//    //options.Conventions.AuthorizeFolder("/MyPages/Admin
//});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Session
app.UseSession();

app.MapRazorPages();

app.Run();
