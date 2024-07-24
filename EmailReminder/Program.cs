using EmailReminder.Models;
using EmailReminder.Services;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EmailReminder
{

    public class Program
    {
        public static void Main(string[] args)
        {

            



            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ReminderContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cn"));
            });

            builder.Services.AddHangfire(configuration => configuration
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("cn")));

            builder.Services.AddHangfireServer();

            builder.Services.AddSingleton<EmailService>();


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();



            BackTask.ScheduleReminderEmails();
        }
    }
   

}
