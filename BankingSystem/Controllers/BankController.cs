using System;
using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));
        }

        [HttpPost]
        public JsonResult AccountSummary([FromBody]Account account)
        {
            Response response = new Response();
            response.errorMessage = String.Empty;
            response.Successfull = false;
            response = _bankService.AccountSummary(account, response);
            return Json(response);
        }

        [HttpPost]
        public JsonResult AddTransaction([FromBody]Transaction transaction)
        {
            Response response = new Response();
            response.errorMessage = String.Empty;
            response.Successfull = false;
            response = _bankService.AddTransaction(transaction, response);
            return Json(response);
        }

        [HttpPost]
        public JsonResult ViewTransactions([FromBody]Account account)
        {
            Response response = new Response();
            response.errorMessage = String.Empty;
            response.Successfull = false;
            response = _bankService.ViewTransactions(account, response);
            return Json(response);
        }

        [HttpPost]
        public JsonResult DateTransactions([FromBody]Transaction transaction)
        {
            Response response = new Response();
            response.errorMessage = String.Empty;
            response.Successfull = false;
            response = _bankService.DateTransactions(transaction, response);
            return Json(response);
        }

        [HttpPost]
        public JsonResult ChequeBook([FromBody]Account account)
        {
            Response response = new Response();
            response.errorMessage = String.Empty;
            response.Successfull = false;
            response = _bankService.ChequeBook(account, response);
            return Json(response);
        }
    }
}