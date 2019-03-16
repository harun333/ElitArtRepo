using ElitArt.Data;
using ElitArt.Services;
using ElitArt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitArt.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IElitRepository _repository;

        public AppController(IMailService mailService,IElitRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            //var results = _context.Products.ToList();
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("harun@kohnic.com", model.Subject, $"Form:{model.Name}-{model.Email},Message: {model.Message} ");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }

        //[Authorize]
        public IActionResult Shop()
        {
            return View();
        }

    }
}
