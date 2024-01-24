using CPN_StreamFileUpload_MicroService.Filters;
using CPN_StreamFileUpload_MicroService.Middleware;
using CPN_StreamFileUpload_MicroService.Models;
using CPN_StreamFileUpload_MicroService.Utilities;
using Microsoft.AspNetCore.Antiforgery;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    //options.Filters.Add(new GenerateAntiforgeryTokenCookieAttribute());
    options.Filters.Add(new DisableFormValueModelBindingAttribute());
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStorePhysicalFileConfigs(builder.Configuration);
builder.Services.AddMailHostConfigs(builder.Configuration);
builder.Services.AddScoped<IMailService, MailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var cookie = builder.Configuration.GetSection("Cookies").Get<Cookies>();
var antiforgery = app.Services.GetRequiredService<IAntiforgery>();

app.Use((context, next) =>
{
    var tokenSet = antiforgery.GetAndStoreTokens(context);
    context.Response.Cookies.Append(cookie.Key, tokenSet.RequestToken!,
        new CookieOptions { HttpOnly = false, MaxAge = TimeSpan.FromMinutes(cookie.MaxAge) });

    return next(context);
}).UseAuthorization();

app.MapControllers();

app.Run();
