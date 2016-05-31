using System.Web.Mvc;

namespace BadiService.Areas.Badi
{
    public class BadiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Badi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Badi_today",
                "Today/{gYear}/{gMonth}/{gDay}/{options}",
                new {
                  controller = "Get",
                  action = "Today",
                  gYear = UrlParameter.Optional,
                  gMonth = UrlParameter.Optional,
                  gDay = UrlParameter.Optional,
                  options = UrlParameter.Optional }
            );

            context.MapRoute(
                "Badi_default",
                "Badi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}