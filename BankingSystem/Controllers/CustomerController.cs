using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankingSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpPost]
        public JsonResult Add([FromBody]Account account)
        {
            Response response = new Response();
            response.errorMessage = String.Empty;
            response.Successfull = false;
            response = _customerService.AddCustomer(account, response);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Remove([FromBody]Account account)
        {
            Response response = new Response();
            response.errorMessage = String.Empty;
            response.Successfull = false;
            response = _customerService.RemoveCustomer(account, response);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Authenticate([FromBody]User user)
        {
            Response response = new Response();
            response.errorMessage = String.Empty;
            response.Successfull = false;
            response = _customerService.Authenticate(user, response);
            return Json(response);
        }

        [HttpPost]
        public JsonResult PasswordReset([FromBody]User user)
        {
            Response response = new Response();
            response.errorMessage = String.Empty;
            response.Successfull = false;
            response = _customerService.PasswordReset(user, response);
            return Json(response);
        }
    }
}