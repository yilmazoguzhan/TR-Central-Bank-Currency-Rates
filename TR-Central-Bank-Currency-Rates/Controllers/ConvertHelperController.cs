using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TR_Central_Bank_Currency_Rates.Controllers
{
    public class ConvertHelperController : ApiController
    {
        //public class 
        LogController logController = new LogController();
        [HttpGet]
        public IHttpActionResult GetToday()
        {
            logController.WriteLog("Metoda girildi.");
            return Json(new { test = "test"});
        }
    }
}
