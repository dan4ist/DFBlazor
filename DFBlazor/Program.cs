using DFBlazor.Data;
using DFBlazor.Repos;
using DFBlazor.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// MSIDENTITY
var cs = builder.Configuration.GetConnectionString("DFDB");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(cs));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();
//MSIDENTITY

//CLAIMS
builder.Services.AddAuthorization(options => {
    options.AddPolicy("SupportedCityPolicy", policy => policy.RequireClaim("city", "dc"));
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
});
//CLAIMS


builder.Services.AddTransient<INoteRepo, NoteRepo>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<NoteService>();
//builder.Services.AddHttpClient();


//ChatGPT
builder.Services.AddSingleton<ChatGPTService>(cp => {
    var config = cp.GetRequiredService<IConfiguration>();
    var apiUrl = config.GetValue<string>("ChatGPTSettings:ApiURL");
    var apiKey = config.GetValue<string>("ChatGPTSettings:ApiKey");
    return new ChatGPTService(apiUrl, apiKey);
});

builder.Services.AddFluxor(o => {
    o.ScanAssemblies(typeof(Program).Assembly);
    o.UseReduxDevTools(rdt =>
    {
        rdt.Name = "DFBlazor";
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( !app.Environment.IsDevelopment() ) {
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//auth middleware (routing then authentication then authorization) MSIDENTITY
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
