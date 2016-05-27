using System;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace BadiService.Areas.Badi.Models
{
  [DebuggerDisplay("{Year}.{Month}.{Day} {BeforeAfterMidnight}")]
  public class BadiDate : IEquatable<BadiDate>
  {
    public BadiDate(int year, int month, int days, BeforeAfterMidnight beforeAfterMidnight)
    {
      Year = year;
      Month = month;
      Day = days;
      BeforeAfterMidnight = beforeAfterMidnight;
    }

    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }

    public BeforeAfterMidnight BeforeAfterMidnight { get; set; }

    public override string ToString()
    {
      return Year + "." + Month + "." + Day + " " + BeforeAfterMidnight;
    }

    public bool Equals(BadiDate other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Year == other.Year && Month == other.Month && Day == other.Day && BeforeAfterMidnight == other.BeforeAfterMidnight;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      var other = obj as BadiDate;
      return other != null && Equals(other);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = Year;
        hashCode = (hashCode * 397) ^ Month;
        hashCode = (hashCode * 397) ^ Day;
        hashCode = (hashCode * 397) ^ (int) BeforeAfterMidnight;
        return hashCode;
      }
    }

    public static bool operator ==(BadiDate left, BadiDate right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(BadiDate left, BadiDate right)
    {
      return !Equals(left, right);
    }
  }
}