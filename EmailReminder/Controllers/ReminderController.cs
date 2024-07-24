using EmailReminder.Models;
//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmailReminder.Controllers
{
    public class ReminderController : Controller
    {
        private static List<Reminder> reminders = new List<Reminder>();

        // GET: Reminder
        public ActionResult Index()
        {
            return View(reminders);
        }

        // GET: Reminder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reminder/Create
        [HttpPost]
        public ActionResult Create(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                reminder.Id = reminders.Count > 0 ? reminders.Max(r => r.Id) + 1 : 1;
                reminders.Add(reminder);
                return RedirectToAction("Index");
            }
            return View(reminder);
        }

        // GET: Reminder/Edit/5
        public ActionResult Edit(int id)
        {
            var reminder = reminders.FirstOrDefault(r => r.Id == id);
            if (reminder == null)
            {
                return HttpNotFound();
            }
            return View(reminder);
        }

        // POST: Reminder/Edit/5
        [HttpPost]
        public ActionResult Edit(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                var existingReminder = reminders.FirstOrDefault(r => r.Id == reminder.Id);
                if (existingReminder != null)
                {
                    existingReminder.Title = reminder.Title;
                    existingReminder.ReminderDateTime = reminder.ReminderDateTime;
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }
            return View(reminder);
        }

        // GET: Reminder/Delete/5
        public ActionResult Delete(int id)
        {
            var reminder = reminders.FirstOrDefault(r => r.Id == id);
            if (reminder == null)
            {
                return HttpNotFound();
            }
            return View(reminder);
        }

        // POST: Reminder/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var reminder = reminders.FirstOrDefault(r => r.Id == id);
            if (reminder != null)
            {
                reminders.Remove(reminder);
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }

}
