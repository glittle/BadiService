using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadiService.Areas.Badi.Models
{
  public class SunCalc
  {
    private const int DefaultSunsetHour = 18;
    private const int DefaultSunsetMinute = 30;

    /// <summary>
    /// This day, at sunset
    /// </summary>
    /// <param name="input"></param>
    /// <param name="forceRelationToSunset"></param>
    /// <returns></returns>
    public DateTime AtSunset(DateTime input, RelationToSunset forceRelationToSunset = RelationToSunset.Undefined)
    {
      // not actually calculating for now...

      var minute = DefaultSunsetMinute;
      switch (forceRelationToSunset)
      {
        case RelationToSunset.gBeforeSunset:
          minute--;
          break;
        case RelationToSunset.gAfterSunset:
          minute++;
          break;
      }
      return new DateTime(input.Year, input.Month, input.Day, DefaultSunsetHour, minute, 0);
    }
  }
}