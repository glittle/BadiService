using System;
using System.Collections.Generic;

namespace BadiService.Areas.Badi.Models
{
  public class BadiMonthInfo
  {
    public BadiMonthInfo(BadiDate badiDate)
    {
      ArabicName = new BadiNames("en").MonthArabic(badiDate.Month);
      LocalName = new BadiNames("en").MonthMeaning(badiDate.Month);
    }

    public string LocalName { get; set; }

    public string ArabicName { get; set; }

  }

//  public class BadiNames
//  {
//    public string Culture { get; set; }
//    public string[] MonthMeaning { get; set; }
//    public string[] ArabicWord { get; set; }
//
//    private Dictionary<string, string[]> 
//
//    public static BadiNames Get(string culture)
//    {
//      return null;
//    }
//  }
}