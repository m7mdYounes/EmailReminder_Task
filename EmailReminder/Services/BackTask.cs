using EmailReminder.Models;
using Hangfire;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmailReminder.Services
{
    public class BackTask
    {
        private static List<Reminder> reminders = new List<Reminder>(); // Mock data source

        public static void ScheduleReminderEmails()
        {
            RecurringJob.AddOrUpdate("check-reminders", () => CheckAndSendReminders(), Cron.Minutely);
        }

        public static async Task CheckAndSendReminders()
        {
            var now = DateTime.Now;
            var dueReminders = reminders.Where(r => r.ReminderDateTime <= now).ToList();

            var emailService = new EmailService();

            foreach (var reminder in dueReminders)
            {
                await emailService.SendEmailAsync("recipient@example.com", reminder.Title, "This is your reminder.");
                reminders.Remove(reminder); // Remove or mark the reminder as sent
            }
        }
    }


}

