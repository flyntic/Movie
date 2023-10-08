using Movie.Options;
using Movie.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
// Add services to the container.
builder.Services.AddControllersWithViews();

//Console.WriteLine("Key     : " + builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("ApiKey"));
//Console.WriteLine("BaseUrl : " + builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("BaseUrl"));

//Console.WriteLine("Key     : " + builder.Configuration["ConnectionStrings:ApiKey"]);
//Console.WriteLine("BaseUrl : " + builder.Configuration["ConnectionStrings:BaseUrl"]);



//builder.Services.AddSingleton();
//builder.Services.AddScoped();


builder.Services.Configure<MovieApiOptions>(options =>
{
    options.ApiKey = builder.Configuration["ConnectionStrings:ApiKey"];
    options.BaseUrl = builder.Configuration["ConnectionStrings:BaseUrl"];
});

builder.Services.AddScoped<IMovieApiService,MovieApiService>();
builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
