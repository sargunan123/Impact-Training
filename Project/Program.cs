
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RequestContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("RequestContext") ?? throw new InvalidOperationException("Connection string 'RequestContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;

builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnections")));

// builder.Services.AddAuthentication(
//     CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(option =>{
//         option.LoginPath="/Admin/Success";
//         option.ExpireTimeSpan=TimeSpan.FromMinutes(20);

//     });


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var _logger=new LoggerConfiguration().
WriteTo.File("C:\\Users\\sargu\\Desktop\\project\\IdentityManagement\\Loger-.log",rollingInterval:RollingInterval.Day).CreateLogger();
 builder.Logging.AddSerilog(_logger);

builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
builder.Services.AddHttpContextAccessor();

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
app.UseAuthentication();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
