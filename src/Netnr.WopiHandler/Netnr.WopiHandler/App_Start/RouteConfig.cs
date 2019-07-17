using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Netnr.WopiHandler
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //静态文件启用路由
            routes.RouteExistingFiles = true;

            //阻止直接访问资源文件  
            routes.MapRoute(name: "wopiFiles",
                url: "wopi/files/{*filename}",
                defaults: new { controller = "WOPI", action = "Files" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "WOPI", action = "Files", id = UrlParameter.Optional }
            );
        }
    }
}
