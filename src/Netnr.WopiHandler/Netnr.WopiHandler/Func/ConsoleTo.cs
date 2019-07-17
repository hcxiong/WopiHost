using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Netnr.WopiHandler
{
    public class ConsoleTo
    {
        /// <summary>
        /// 写入错误信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="isFull">是否全部信息，默认false</param>
        public static void Log(Exception ex, bool isFull = false)
        {
            var msg = ExceptionGet(ex, isFull);
            Log(msg);
        }

        /// <summary>
        /// 写入消息
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(string msg)
        {
            var dt = DateTime.Now;
            var path = "/logs/" + dt.ToString("yyyyMM") + "/";
            path = HttpContext.Current.Server.MapPath(path);
            WriteText(msg, path, "console_" + dt.ToString("yyyyMMdd") + ".log");
        }

        /// <summary>
        /// 获取异常信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="isFull">是否包含堆栈所有信息，默认 false</param>
        /// <returns></returns>
        private static string ExceptionGet(Exception ex, bool isFull = false)
        {
            var en = Environment.NewLine;
            var st = ex.StackTrace;
            if (!isFull)
            {
                st = st.Replace(en, "^").Split('^')[0];
            }

            string msg = string.Join(en, new List<string>()
            {
                $"====日志记录时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}",
                $"消息内容：{ex.Message}",
                $"引发异常的方法：{st}{en}"
            });

            if (ex.InnerException != null)
            {
                msg += ExceptionGet(ex.InnerException, isFull);
            }

            return msg;
        }

        /// <summary>
        /// 流写入
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">物理目录</param>
        /// <param name="fileName">文件名</param>
        /// <param name="isAppend">默认追加，false覆盖</param>
        public static void WriteText(string content, string path, string fileName, bool isAppend = true)
        {
            FileStream fs;

            //检测目录
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                fs = new FileStream(path + fileName, FileMode.Create);
            }
            else
            {
                //文件是否存在 创建 OR 追加
                if (!File.Exists(path + fileName))
                {
                    fs = new FileStream(path + fileName, FileMode.Create);
                }
                else
                {
                    FileMode fm = isAppend ? FileMode.Append : FileMode.Truncate;
                    fs = new FileStream(path + fileName, fm);
                }
            }

            //流写入
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(content);
            sw.Close();
            fs.Close();
        }
    }
}
