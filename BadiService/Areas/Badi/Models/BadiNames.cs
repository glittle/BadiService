namespace BadiService.Areas.Badi.Models
{
  public class BadiNames
  {
    public BadiNames(string culture)
    {
    }

    public string MonthMeaning(int num)
    {
      return
        "Intercalary Days,Splendor,Glory,Beauty,Grandeur,Light,Mercy,Words,Perfection,Names,Might,Will,Knowledge,Power,Speech,Questions,Honor,Sovereignty,Dominion,Loftiness"
          .Split(',')[num];
    }
    public string MonthArabic(int num)
    {
      return
        "Ayyám-i-Há,Bahá,Jalál,Jamál,`Azamat,Núr,Rahmat,Kalimát,Kamál,Asmá’,`Izzat,Mashíyyat,`Ilm,Qudrat,Qawl,Masá'il,Sharaf,Sultán,Mulk,`Alá’"
          .Split(',')[num];
    }
  }
}