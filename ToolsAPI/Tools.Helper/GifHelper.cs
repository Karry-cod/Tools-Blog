using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Gif;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helper
{
    /// <summary>
    /// Gif帮助类
    /// </summary>
    public class GifHelper
    {
        /// <summary>
        /// 截取屏幕获取帧
        /// </summary>
        /// <param name="Left"></param>
        /// <param name="Top"></param>
        /// <param name="Length"></param>
        /// <param name="Width"></param>
        /// <param name="INCR"></param>
        public static void CaptureScreenArea(int Left, int Top, int Length, int Width, ref int INCR, string tempUrl)
        {
            Bitmap bitmap = new Bitmap(Length, Width);
            //通过参数选择屏幕的区域
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(Left, Top, Length, Width);

            if (Directory.Exists(tempUrl))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
                }
                //保存到上面的临时文件夹中
                bitmap.Save($@"{tempUrl}/{INCR}.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                Directory.CreateDirectory(tempUrl);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
                }

                bitmap.Save($@"{tempUrl}/{INCR}.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            INCR++;
        }

        /// <summary>
        /// 生成Gif - 屏幕抓帧
        /// </summary>
        /// <param name="url">gif存放路径</param>
        /// <param name="tempUrl">屏幕帧临时存放路径</param>
        /// <returns></returns>
        public static string CreateGifByScreen(string url, string tempUrl) {
            int fps = 20;   //每秒截图的数量

            int Secs = 3;   //持续录制的时长

            int INCR = 1;

            for (int i = 1; i <= Secs * fps; i++)
            {
                CaptureScreenArea(0, 0, 1920, 1080, ref INCR, tempUrl); //截取屏幕左上角200x200的区域
                Thread.Sleep(50);    //请注意这里SleepTimex(Secs*fps) = Secs(秒)
                                     //Sleep时间和fps的公式为SleepTime = 1000/fps(毫秒)
            }
            //用一个数组加载上面保存再TempDir文件夹下的截图
            SixLabors.ImageSharp.Image[] imgs = new SixLabors.ImageSharp.Image[Secs * fps];

            for (int i = 0; i < Secs * fps; i++)
            {
                imgs[i] = SixLabors.ImageSharp.Image.Load($@"{tempUrl}/{i + 1}.png");
            }

            //创建Gif对象
            Image<Rgba32> gif = new Image<Rgba32>(1920, 1080);
            //获取Gif头帧的Meta信息
            GifFrameMetadata meta = gif.Frames.RootFrame.Metadata.GetGifMetadata();

            meta.FrameDelay = 0;//设置头帧开始播放的延迟

            foreach (SixLabors.ImageSharp.Image im in imgs)
            {   //设置加载图片的Gif格式的Meta信息
                GifFrameMetadata temp_meta = im.Frames.RootFrame.Metadata.GetGifMetadata();
                temp_meta.FrameDelay = 10;//梁振之前的间隔时间：（0~100,数值越小越快）

                gif.Frames.AddFrame(im.Frames.RootFrame); //添加到Gif中
            }


            gif.Metadata.GetGifMetadata().RepeatCount = 0; //设置重复次数，0则一直循环
            string path = url + DateTime.Now.ToString("yyyyMMddHHmmss") + ".gif"; // 存放路径
            gif.SaveAsGif(path);

            Directory.Delete(tempUrl, true);
            return path;
        }

        /// <summary>
        /// 生成Gif - 合同图片
        /// </summary>
        /// <param name="files">图片集</param>
        /// <param name="url">gif存放路径</param>
        /// <param name="tempUrl">图片的临时存放路径</param>
        public static string CreateGifByImgs(List<string> urls, string url, string tempUrl, int pxX, int pxY)
        {
            //用一个数组加载上面保存再TempDir文件夹下的截图
            SixLabors.ImageSharp.Image[] imgs = new SixLabors.ImageSharp.Image[urls.Count];

            for (int i = 0; i < urls.Count; i++)
            {
                imgs[i] = SixLabors.ImageSharp.Image.Load(urls[i]);
            }

            //创建Gif对象
            Image<Rgba32> gif = new Image<Rgba32>(pxX, pxY);
            //获取Gif头帧的Meta信息
            GifFrameMetadata meta = gif.Frames.RootFrame.Metadata.GetGifMetadata();

            meta.FrameDelay = 0;//设置头帧开始播放的延迟

            foreach (SixLabors.ImageSharp.Image im in imgs)
            {   //设置加载图片的Gif格式的Meta信息
                GifFrameMetadata temp_meta = im.Frames.RootFrame.Metadata.GetGifMetadata();
                temp_meta.FrameDelay = 10;//梁振之前的间隔时间：（0~100,数值越小越快）

                gif.Frames.AddFrame(im.Frames.RootFrame); //添加到Gif中
            }


            gif.Metadata.GetGifMetadata().RepeatCount = 0; //设置重复次数，0则一直循环
            string path = url + DateTime.Now.ToString("yyyyMMddHHmmss") + ".gif"; // 存放路径
            gif.SaveAsGif(path);

            Directory.Delete(tempUrl, true);
            return path;
        }
    }
}
