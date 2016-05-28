using System;

namespace BadiService.Areas.Badi.Models
{
  public class BadiCalc
  {
    public const int DefaultSunsetHour = 18;
    public const int DefaultSunsetMinute = 30;
    private PresetKnowledge _presetKnowledge;

    private PresetKnowledge PresetKnowledge => _presetKnowledge ?? (_presetKnowledge = new PresetKnowledge());


    public BadiDate GetBadiDate(DateTime gDate, RelationToSunset relationToSunset)
    {
      // when extended to consider real sunset, can remove the "relationToSunset" parameter
      // not dealing with sunset. Force to before or after sunset...
      gDate = new DateTime(gDate.Year, gDate.Month, gDate.Day, DefaultSunsetHour,
        DefaultSunsetMinute + (relationToSunset == RelationToSunset.gDayAfterSunset ? 5 : -5), 0);


      // if gDate > sunset offset = 0
      var afterSunsetOffset = relationToSunset == RelationToSunset.gDayAfterSunset ? 1 : 0;

      var bDayRelationToMidnight = relationToSunset == RelationToSunset.gDayAfterSunset ? RelationToMidnight.bDayBeforeMidnight : RelationToMidnight.bDayAfterMidnight;

      var year = gDate.Year - 1844;
      int month;
      int day;
      var nawRuz = GetNawRuz(gDate.Year);
      var isAfterStartOfNawRuz = gDate >= nawRuz;
      if (isAfterStartOfNawRuz)
      {
        // forward: March - Dec
        var dayOfBadiYear = gDate.DayOfYear - nawRuz.DayOfYear + afterSunsetOffset;
        var month0 = (int)Math.Floor(dayOfBadiYear / 19D);

        year++;
        day = dayOfBadiYear - month0 * 19;
        month = month0 + 1;
      }
      else
      {
        // back: Jan - March
        var lastDec31 = new DateTime(gDate.Year - 1, 12, 31);
        var lastNawRuz = GetNawRuz(gDate.Year - 1, true);

        var dayOfBadiYear = gDate.DayOfYear + lastDec31.DayOfYear - lastNawRuz.DayOfYear + afterSunsetOffset;
        var month0 = (int)Math.Floor(dayOfBadiYear / 19D);
        day = dayOfBadiYear - month0 * 19;
        month = month0 + 1;

        if (month >= 19)
        {
          // In ayyam-i-ha or Loftiness
          var firstDayOfLoftiness = nawRuz.AddDays(-19);
          if (gDate < firstDayOfLoftiness)
          {
            // in ayyam-i-ha
            month = 0;
          }
          else
          {
            // in Loftiness
            month = 19;
            day = 1 + gDate.DayOfYear - firstDayOfLoftiness.DayOfYear + afterSunsetOffset;

            if (day == 20)
            {
              // after sunset on last day
              year++;
              month = 1;
              day = 1;
            }
          }
        }
      }

      return new BadiDate(year, month, day, bDayRelationToMidnight);
    }

    public DateTime GetGDate(int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight = RelationToMidnight.bDayAfterMidnight,
      bool autoFix = false)
    {
      if (bMonth < 0)
      {
        if (autoFix)
        {
          bMonth = 1;
        }
        else
        {
          throw new ApplicationException("Invalid Badi month: " + bMonth);
        }
      }

      if (bMonth > 19)
      {
        if (autoFix)
        {
          bMonth = 19;
        }
        else
        {
          throw new ApplicationException("Invalid Badi month: " + bMonth);
        }
      }

      if (bDay < 1)
      {
        if (autoFix)
        {
          bDay = 1;
        }
        else
        {
          throw new ApplicationException("Invalid Badi day: " + bDay);
        }
      }
      if (bDay > 19)
      {
        if (autoFix)
        {
          bDay = 19;
        }
        else
        {
          throw new ApplicationException("Invalid Badi day: " + bDay);
        }
      }

      DateTime answer;

      var gYear = bYear + 1843;

      switch (bMonth)
      {
        case 0:
        case 19:
          var nextNawRuz = GetNawRuz(gYear + 1, true);
          var startOfAla = nextNawRuz.AddDays(-19);

          if (bMonth == 0)
          {
            var firstDayOfAyyamiHa = GetGDate(bYear, 18, 19).AddDays(1);
            var lastDayOfAyyamiHa = GetGDate(bYear, 19, 1).AddDays(-1);

            var numDaysInAyyamiHa = DaysBetween(firstDayOfAyyamiHa, lastDayOfAyyamiHa);
            if (bDay > numDaysInAyyamiHa)
            {
              if (autoFix)
              {
                bDay = numDaysInAyyamiHa;
              }
              else
              {
                throw new ApplicationException("Invalid day for Ayyam-i-Ha: " + bDay);
              }
            }
          }
          answer = startOfAla.AddDays(bDay - 1);
          break;

        default:
          var nawRuz = GetNawRuz(gYear, true);
          var beforeMidnightOffset = relationToMidnight == RelationToMidnight.bDayAfterMidnight ? 1 : 0;
          answer = nawRuz.AddDays((bMonth - 1) * 19 + (bDay - 1 + beforeMidnightOffset));
          break;
      }

      // if calcuating sunset, get sunset for this date!

      return answer;
    }

    public DateTime GetNawRuz(int gYear, bool atNoon = false)
    {
      // the first moment of the new year... usually on March 19 sunset or March 20 sunset
      var nawRuz = new DateTime(
        gYear,
        3,
        20 + PresetKnowledge.GetNawRuzOffset(gYear - 1843),
        atNoon ? 12 : DefaultSunsetHour,
        atNoon ? 0 : DefaultSunsetMinute,
        0
        );

      //TODO calculate sunset
      //if not dateOnlyNoTime

      return nawRuz;
    }

    public int GetBadiYear(DateTime gDate)
    {
      return gDate.Year - 1843 - (IsAfterStartOfNawRuz(gDate) ? 0 : 1);
    }


    public bool IsAfterStartOfNawRuz(DateTime gDate)
    {
      return gDate >= GetNawRuz(gDate.Year);
    }

    private int DaysBetween(DateTime d1, DateTime d2)
    {
      return 1 + (int)Math.Round(Math.Abs((d1 - d2).TotalDays));
    }
  }
}