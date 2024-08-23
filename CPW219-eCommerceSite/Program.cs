using CPW219_eCommerceSite.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VideoGameContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

/// <summary>
/// Allow session access in Views
/// This line adds the IHttpContextAccessor service to the application. The IHttpContextAccessor service provides access to the HttpContext object,
/// which represents the current HTTP request. This service is used to access the session data in the application.
/// </summary>
// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); same as below
builder.Services.AddHttpContextAccessor();

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-8.0#configure-session-state
/// <summary>
/// Explanation: This line adds a service to the application that enables caching in memory. In the context of session state, 
/// this cache is used to temporarily store session data. The DistributedMemoryCache is a simple, in-memory cache that is used 
/// when you want to store data across requests, such as user-specific data (like shopping cart contents) during their session.
/// </summary>
// Add session - Part 1 of 2
builder.Services.AddDistributedMemoryCache();

/// <summary>
/// Explanation: This line adds the session services to your application. By calling AddSession(), you're enabling the ASP.NET
/// Core session middleware, which allows you to store and retrieve data (like user preferences or temporary data) on a per-user
/// basis during a session. Sessions are typically used to track user state and data across multiple requests within the same browsing session.
/// </summary>
builder.Services.AddSession();

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

/// <summary>
/// Explanation: This line adds the session middleware to the HTTP request pipeline. It means that
/// the application will now use sessions. The session middleware reads and writes session data for
/// each request, enabling you to access session data within your controllers and views. By placing
/// app.UseSession() in the middleware pipeline, you're ensuring that session state is available to be used in your application.
/// </summary>
// Add session - Part 2 of 2
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
