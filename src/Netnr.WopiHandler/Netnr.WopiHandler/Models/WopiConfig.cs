using System;
using static System.Web.Configuration.WebConfigurationManager;

namespace Netnr.WopiHandler
{
    public class WopiConfig
    {
        public static string WopiPath { get; set; } = AppSettings["WopiPath"].ToString();
        public static string FilesRequestPath { get; set; } = AppSettings["FilesRequestPath"].ToString();
        public static string FoldersRequestPath { get; set; } = AppSettings["FoldersRequestPath"].ToString();
        public static string ContentsRequestPath { get; set; } = AppSettings["ContentsRequestPath"].ToString();
        public static string ChildrenRequestPath { get; set; } = AppSettings["ChildrenRequestPath"].ToString();
        public static string LocalStoragePath
        {
            get
            {
                var lsp = AppSettings["LocalStoragePath"].ToString();
                if (string.IsNullOrWhiteSpace(lsp))
                {
                    lsp = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/").TrimEnd('/') + "/upload/";
                }
                return lsp;
            }
            set { }
        }
        public static string BreadcrumbBrandName { get; set; } = AppSettings["BreadcrumbBrandName"].ToString();
    }
}