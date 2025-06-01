using Microsoft.EntityFrameworkCore;
using TimeSheet.Data.Data;
using Quartz;
using TimeSheet_Module.Services.Jobs;
//using System.Text.Json.Serialization;
using System.Net;
using TimeSheet_Module.Services.Implementations;
using TimeSheet_Module.Services.Interfaces;
using Serilog;


// Replace the existing Serilog configuration block with the following:

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});

builder.Services.AddTransient<PostData>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddQuartz(q =>
{
    var MondayJobKey = new JobKey("MondayJob");
    q.AddJob<MondayJob>(MondayJobKey);

    q.AddTrigger(trigger => trigger.ForJob(MondayJobKey).WithIdentity("MondayMorning-trigger").WithCronSchedule("0 1 0 ? * Mon")); //every Monday Morning
    q.AddTrigger(trigger => trigger.ForJob(MondayJobKey).WithIdentity("MondayNight-trigger").WithCronSchedule("0 00 22 ? * Mon")); //every Monday Night //AutoSubmit at 10:00 PM

    var SaturdayJobKey = new JobKey("SaturdayJob"); // send reminder mails to fill their timesheet
    q.AddJob<Saturday>(SaturdayJobKey);

    q.AddTrigger(trigger => trigger.ForJob(SaturdayJobKey).WithIdentity("SaturdayJob-trigger").WithCronSchedule("0 1 0 ? * Sat")); //every Saturday Morning

    var BatchJobKey = new JobKey("BatchJob"); //get projects from bc, get working hours of employees
    q.AddJob<BatchJob>(BatchJobKey);

    q.AddTrigger(trigger => trigger.ForJob(BatchJobKey).WithIdentity("BatchJob-trigger").WithCronSchedule("0 0/30 * * * ?")); //every 30 minutes
    q.AddTrigger(trigger => trigger.ForJob(BatchJobKey).WithIdentity("EveningJob-trigger").WithCronSchedule("0 30 23 * * ?")); //every Evening
});
// Add Quartz hosted service
builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true); //if shutdown, then will wait for job to execute then exit.

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "CookieAuthentication";  // Set cookie authentication as the default scheme
    options.DefaultChallengeScheme = "CookieAuthentication";  // Set challenge scheme for unauthenticated users
})
.AddCookie("CookieAuthentication", options =>
{
    options.LoginPath = "/Account/";  // Path to login
    options.AccessDeniedPath = "/Account/AccessDenied";  // Path for unauthorized users

    //setting the session timeout for security( login time will overwrite it)
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Expire after 30 minutes
    options.SlidingExpiration = true; // Refresh expiration on activity
});

builder.Services.AddAuthorization();

builder.Services.AddHttpClient("BC_Client",client =>
{
    client.Timeout = TimeSpan.FromMinutes(10); // Increase timeout to 10 minutes
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        Credentials = new NetworkCredential("VTS", "**Noida123$", "NAVBCSERVER") // API authentication credentials
    };
});

builder.Services.AddHttpClient("Zing_HR", client =>
{
    client.Timeout = TimeSpan.FromMinutes(10); // Increase timeout to 10 minutes
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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
