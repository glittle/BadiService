namespace BadiService.Areas.Badi.Models
{
  public class BadiDayInfo
  {
    public BadiDayInfo(BadiDate badiDate)
    {
      ArabicName = new BadiNames("en").MonthArabic(badiDate.Day);
      LocalName = new BadiNames("en").MonthMeaning(badiDate.Day);

    }

    public string LocalName { get; set; }

    public string ArabicName { get; set; }
  }
}