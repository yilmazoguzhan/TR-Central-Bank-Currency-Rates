using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace TR_Central_Bank_Currency_Rates.Controllers
{
    public class GetXmlParams
    {
        public string ShortDate { get; set; }
    }

    public class CurrencyConvertParams
    {
        public string Currency { get; set; }
    }

    public class ConvertHelperController : ApiController
    {
        //public class 
        LogController logController = new LogController();

        [HttpGet]
        public IHttpActionResult GetTodayXml()
        {
            try
            {
                string xmlUrl = "http://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlUrl);
                return Json(new { state = true, content = xmlDoc.InnerXml });
            }
            catch (Exception ex)
            {
                logController.WriteLog(ex.Message);
                return Json(new { state = false, content = "Merkez Bankası xml bilgisi alınırken hata meydana geldi!" });
            }
        }

        [HttpPost]
        public IHttpActionResult GetXml([FromBody] GetXmlParams getXmlParams)
        {
            if(string.IsNullOrEmpty(getXmlParams.ShortDate) || getXmlParams.ShortDate == "" || getXmlParams.ShortDate.Length != 8)
            {
                return Json(new { state = false, content = "Tarih sekiz karakter ve ddMMyyyy formatında olmalıdır." });
            }
            else
            {
                string urlPart = getXmlParams.ShortDate.Substring(4, 4) + getXmlParams.ShortDate.Substring(2, 2);

                try
                {
                    string xmlUrl = "http://www.tcmb.gov.tr/bilgiamackur/"+ urlPart + "/" + getXmlParams.ShortDate + ".xml";
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlUrl);
                    return Json(new { state = true, content = xmlDoc.InnerXml });
                }
                catch (Exception ex)
                {
                    logController.WriteLog(ex.Message);
                    return Json(new { state = false, content = "Merkez Bankası ilgili tarihin xml bilgisi alınırken hata meydana geldi!" });
                }
            }
        }

        [HttpPost]
        public IHttpActionResult CurrencyConvert([FromBody] CurrencyConvertParams currencyConvertParams)
        {
            string[] CurrencyTypes = { "USD", "AUD", "DKK", "EUR", "GBP", "CHF", "SEK", "CAD", "KWD", "NOK", "SAR", "JPY", "BGN", "RON", "RUB", "IRR", "CNY", "PKR", "QAR", "XDR" };
            
            if(CurrencyTypes.Contains(currencyConvertParams.Currency))
            {
                try
                {
                    string xmlUrl = "http://www.tcmb.gov.tr/kurlar/today.xml";
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlUrl);
                    XmlNodeList currencyInfo = xmlDoc.SelectNodes("//Currency[@CurrencyCode='" + currencyConvertParams.Currency + "']");
                    return Json(new { state = true, content = currencyInfo });
                }
                catch (Exception ex)
                {
                    logController.WriteLog(ex.Message);
                    return Json(new { state = false, content = "Merkez Bankası xml bilgisi alınırken hata meydana geldi!" });
                }
            }
            else
            {
                string currencyTotal = string.Empty;
                foreach (string currency in CurrencyTypes)
                {
                    currencyTotal = currencyTotal + " - " + currency;
                }
                currencyTotal = currencyTotal.Substring(3, currencyTotal.Length - 3);
                return Json(new { state = false, content = "Geçerli bir döviz türü girmeniz gerekmektedir. Döviz türleri '" + currencyTotal + "'" });
            }
        }
    }
}
