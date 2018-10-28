using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TR_Central_Bank_Currency_Rates.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Index()
        {
            return View();
        }

        public void WriteLog(string message)
        {
            var path = HttpRuntime.AppDomainAppPath + @"\Log\";
            var filename = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".log";
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                StreamWriter logWriter = new StreamWriter(path + filename);
                logWriter.WriteLine(message);
                logWriter.Close();
                logWriter.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception ("Log Write Error! Error details: " + ex.Message);
            }
        }
    }
}