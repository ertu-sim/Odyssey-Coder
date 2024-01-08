using BlogApp.Data.Abstrack;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration;
    var connection = config.GetConnectionString("sql");
    options.UseSqlServer(connection);

    //tüm bu kodlar yerine options.UseSqlServer(builder.configuration["ConnectionStrings: sql"]) yazarak da bağlantı kurulabilir

});
builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagsRepository, EfTagsRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
options.LoginPath = "/Users/Login"
);
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.TestVerileriniDoldur(app);

//localhost://Posts/web-programlama
//localhost://Posts/php

// app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "post_details",
    pattern: "Posts/Details/{Url}",
    defaults: new { Controller = "Posts", Action = "Details" }
);
app.MapControllerRoute(
    name: "post_By_Tag",
    pattern: "Posts/Tag/{tag}",
    defaults: new { Controller = "Posts", Action = "Index" }
);
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
