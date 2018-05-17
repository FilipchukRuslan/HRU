using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BAL.MailKit;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace WebApp.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMessage()
        {
            EmailService emailService = new EmailService();
            emailService.SendEmailAsync();
            return RedirectToAction("Index");
        }
    }
}