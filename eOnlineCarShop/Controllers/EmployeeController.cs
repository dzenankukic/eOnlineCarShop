using Data_CS.Data;
using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUsers() // metoda koja vraca podatke za Angular
        {
            var model = _db.User
                .Select(s => new ShowUsersVM {
                userid = s.Id,
                firstname = s.FirstName,
                lastname = s.LastName,
                birthdate = s.BirthDate.ToString(),
                username = s.UserName,
                email = s.Email,
                emailconfirmed = s.EmailConfirmed,
                phonenumber = s.PhoneNumber,
                phonenumberconfirmed = s.PhoneNumberConfirmed
            }).ToList();

            return Ok(model);
        }

        /*
         Implementirati sl funkcije
         
        Update, Delete

         */
    }
}
