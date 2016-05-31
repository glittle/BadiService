using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using BadiService.Areas.Badi.Models;

namespace BadiService.Areas.Badi.Controllers
{
  public class GetController : Controller
  {
    /// <summary>
    /// Get information for today
    /// </summary>
    /// <param name="gYear">Gregorian year</param>
    /// <param name="gMonth">Gregorian month</param>
    /// <param name="gDay">Gregorian day</param>
    /// <param name="options"></param>
    /// <returns></returns>
    public ActionResult Today(int gYear = 0, int gMonth = 0, int gDay = 0, string options = null)
    {
      var gDate = gYear == 0 || gMonth == 0 || gDay == 0 ? DateTime.Today : new DateTime(gYear, gMonth, gDay);
      var bDate = new BadiCalc().GetBadiDate(gDate, RelationToSunset.gBeforeSunset);
      return new JsonResult()
      {
        Data = bDate,
        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        ContentEncoding = Encoding.UTF8
      };
    }
  }
}
