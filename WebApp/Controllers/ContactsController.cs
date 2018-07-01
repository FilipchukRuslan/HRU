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
        private readonly IAbstractInfoManager abstractInfoManager;
        public ContactsController(
           IAbstractInfoManager abstractInfoManager)
        {
            this.abstractInfoManager = abstractInfoManager;
        }
        public IActionResult Index()
        {
            AbstractInfo info = null;
            if (abstractInfoManager.GetAll().Count() < 1)
            {
                info = new AbstractInfo() { Title = "по умолчанию", Text = "09826259810" };
            }
            else
            {
                info = abstractInfoManager.Get().LastOrDefault();
            }
            return View(new StartPageViewModel()
            {
                Info = info
            });
        }

        public IActionResult SendMessage(string UserEmail, string Message, string Phone, string Name)
        {
            string ssword = "hrumailservice123";
            string serviceMail = "hrumailservice@gmail.com";
            string ToEmail = "east.hr.group@gmail.com";
            EmailService emailService = new EmailService();
            emailService.SendEmailAsync(ssword, serviceMail, ToEmail, UserEmail, Message, Phone, Name);
            return RedirectToAction("Index");
        }
    }
}