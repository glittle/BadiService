using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BadiService.Areas.Badi.Controllers
{
  public class TodayController : ApiController
  {
    /// <summary>
    /// Get information for today
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public string Get(int year, int month, int day, string options)
    {
      return "value";
    }
  }
}
