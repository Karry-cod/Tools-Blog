using Net.Codecrete.QrCodeGenerator;
using System.Text;
using Tools.Helper.Extensions;

namespace Tools.Helper
{
    public class QrCodeHelper
    {

        /// <summary>
        /// 生成二维码（SVG）
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string CreateQrCode_Svg(string content, string url = "FileUpload/QrCode/")
        {
            string path = Directory.GetCurrentDirectory() + "/" + url + DateTime.Now.ToString("yyyyMMddHHmmss") + ".svg";
            try {
                var qr = QrCode.EncodeText(content, QrCode.Ecc.High);
                string svg = qr.ToSvgString(4, "green", "white");
                File.WriteAllText(path, svg, Encoding.UTF8);
            } catch (Exception ex) {
                return "false：" + ex.Message;
            }
            return path;
        }

        /// <summary>
        /// 生成二维码（PNG）
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string CreateQrCode_Png(string content, string url = "FileUpload/QrCode/")
        {
            string path = Directory.GetCurrentDirectory() + "/" + url + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            try {
                var qr = QrCode.EncodeText(content, QrCode.Ecc.High);
                qr.SaveAsPng(path, 10, 3,
                // 45aae5
                // ffffff
                 foreground: System.Drawing.Color.White,
                 background: System.Drawing.Color.Black
                );
            } catch (Exception ex) {
                return "false：" + ex.Message;
            }
            return path;
        }
    }
}
