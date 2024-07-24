using Microsoft.EntityFrameworkCore;

namespace EmailReminder.Models
{
    public class ReminderContext:DbContext
    {
        public DbSet<Reminder> Reminders { get; set; }
        public ReminderContext() : base()
        {

        }
        public ReminderContext(DbContextOptions<ReminderContext> options) : base(options) { }
    }
}
