using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace TR_Central_Bank_Currency_Rates.Controllers
{
    public class ConvertHelperController : ApiController
    {
        //public class 
        LogController logController = new LogController();

        [HttpGet]
        public IHttpActionResult GetTodayXml()
        {
            try
            {
                string todayXmlUrl = "http://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(todayXmlUrl);
                return Json(new { state = true, content = xmlDoc.InnerXml });
            }
            catch (Exception ex)
            {
                logController.WriteLog(ex.Message);
                return Json(new { state = false, content = "Merkez Bankası xml bilgisi alınırken hata meydana geldi!" });
            }
        }
    }
}
