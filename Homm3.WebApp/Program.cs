using Homm3.Domain.Calculator;
using Homm3.Domain.Model.MapObjects;
using Homm3.Domain.Model.MapPresets;
using Homm3.Domain.Model.Monsters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMonsterFactory, MonsterFactory>();
builder.Services.AddSingleton<IMapObjectFactory, MapObjectFactory>();
builder.Services.AddSingleton<IObjectWithMonsterRewardFactory, ObjectWithMonsterRewardFactory>();
builder.Services.AddSingleton<IHomm3Calculator, Homm3Calculator>();
builder.Services.AddSingleton<IPresetFactory, PresetFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
