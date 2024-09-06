using hackador4.Services;
using Telegram.Bot;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHostedService<WebhookConfig>();
builder.Services.AddHttpClient("tgwebhook")
    .AddTypedClient<ITelegramBotClient>(
        httpClient => new TelegramBotClient("7505959570:AAEa2MtBT3f579qcsyqOTQcQ2AsYK5TqCGg", httpClient)
    );
builder.Services.AddScoped<HandleUpdate>();
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{   
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseEndpoints( endpoints => {
    var token = "7505959570:AAEa2MtBT3f579qcsyqOTQcQ2AsYK5TqCGg";
    app.MapControllerRoute(
        name: "tgwebhook",
        pattern: $"bot/{token}",
        new { controller = "Webhook", action = "Post" }
    )
});
app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
