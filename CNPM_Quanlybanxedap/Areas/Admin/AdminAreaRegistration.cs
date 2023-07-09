using System.Web.Mvc;

namespace CNPM_Quanlybanxedap.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "TrangChu", id = UrlParameter.Optional }
            );
        }
    }
}