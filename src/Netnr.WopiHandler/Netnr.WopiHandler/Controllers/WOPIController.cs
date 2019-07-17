using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Netnr.WopiHandler.Controllers
{
    public class WOPIController : Controller
    {
        public void Files()
        {
            if (Request.Url.AbsolutePath == "/" || Request.Url.AbsolutePath.TrimEnd('/') + "/" == WopiConfig.WopiPath + WopiConfig.FilesRequestPath)
            {
                var dm = new List<string>()
                {
                    "<pre>",
                    "<code>/wopi/files/{path+filename}?access_token={token}&UserId={id}&UserName={name}</code>",                    
                    "<code>/wopi/files/word.docx?access_token=123&UserId=admin&UserName=管理员</code>",
                    "",
                    "所有的中文需先编码传参，WOPISrc参数再整体编码，即编码两次",
                    "保存写入有延时，约30秒左右",
                    "需赋予根目录有读写权限",
                    "</pre>"
                };

                Response.Write(string.Join(Environment.NewLine, dm));
                Response.ContentType = "text/html";
            }
            else
            {
                var context = ((HttpApplication)(HttpContext.GetService(typeof(HttpApplication)))).Context;
                new WopiHandler().ProcessRequest(context);
            }
        }
    }
}