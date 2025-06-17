
using StartFromScratch.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<List<Employe>>((s) => new List<Employe>()
        {
            new Employe(){Nom="Mauras", Prenom="Dominique", Actif=true, DateEntree=DateTime.Now, Matricule="007", Salaire=1000000000},
            new Employe(){Nom="Gates", Prenom="Bill", Actif=true, DateEntree=DateTime.Now, Matricule="009", Salaire=100000000},
            new Employe(){Nom="Waine", Prenom="John", Actif=false, DateEntree=DateTime.Now, Matricule="005", Salaire=1000000}
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
