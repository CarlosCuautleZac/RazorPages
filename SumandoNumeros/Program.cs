var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var app = builder.Build();

app.UseFileServer();
app.MapRazorPages();

app.Run();
