using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interfaces;
using BAL.MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Model.DB;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMessage(string UserEmail, string Message, string Phone, string Name)
        {
            string ssword = "hrumailservice123";
            string serviceMail = "hrumailservice@gmail.com";
            string ToEmail = "Filipchukruslan@rambler.ru";
            EmailService emailService = new EmailService();
            emailService.SendEmailAsync(ssword, serviceMail, ToEmail, UserEmail, Message, Phone, Name);
            return RedirectToAction("Index");
        }
    }
}