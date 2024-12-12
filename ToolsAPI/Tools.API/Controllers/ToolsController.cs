
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Tools.Common;
using Tools.Common.Collection;
using Tools.Helper;
using Tools.Model;

namespace Tools.API.Controllers
{
    /// <summary>
    /// 工具管理
    /// </summary>
    public class ToolsController : BaseControllerr
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public ToolsController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> CreateQrCode(string content, string? type = "png") {
            // 判断生成类型
            var types = new List<string>() { "png", "svg" };
            if (!types.Contains(type ?? "")) {
                return await ApiResult.Fail(message: "文件类型错误");
            }

            string url = _configuration.GetSection("FileUpload:QrCode").Value ?? "";
            FileHelper.CreateFolder(url);

            string path = type.ToLower() switch { 
                "svg" => QrCodeHelper.CreateQrCode_Svg(content, url),
                "png" => QrCodeHelper.CreateQrCode_Png(content, url)
            };

            if (path.Contains("false")) {
                return await ApiResult.Fail(message: "系统异常，图片生成失败," + path);
            }

            var data = OSSHelper.Upload_Path(new List<string>() { path }, folder: "tempFiles/");
            FileHelper.ClearFolder(url);

            return await ApiResult.Ok(data: data);
        }

        /// <summary>
        /// 生成Gif - 屏幕抓帧
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string CreateGifByScreen() {
            string url = _configuration.GetSection("FileUpload:Gif").Value ?? "";
            string tempUrl = _configuration.GetSection("FileUpload:Temp").Value ?? "";
            FileHelper.CreateFolder(url);
            FileHelper.CreateFolder(tempUrl);
            return _environment.ContentRootPath + GifHelper.CreateGifByScreen(url, tempUrl);
        }

        /// <summary>
        /// 生成Gif - 合成图片
        /// </summary>
        /// <param name="urls">图片地址集</param>
        /// <returns></returns>
        [HttpPost]
        public string CreateGifByImgs(List<string> urls)
        {
            string url = _configuration.GetSection("FileUpload:Gif").Value ?? "";
            string tempUrl = _configuration.GetSection("FileUpload:Temp").Value ?? "";
            return _environment.ContentRootPath + GifHelper.CreateGifByImgs(urls, url, tempUrl, 1422, 800);
        }

        /// <summary>
        /// 批量下载文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DownloadFiles(List<IFormFile> files) {
            string tempUrl = _configuration.GetSection("FileUpload:Temp").Value ?? "";
            string envPath = _environment.ContentRootPath;

            return await FileHelper.DownloadFiles(files, tempUrl, envPath);
        }

        #region 语音识别功能
        ///// <summary>
        ///// 识别文字转语音
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task SpeechText(string text) {
        //    using (SpeechSynthesizer synthesizer = new SpeechSynthesizer()) {
        //        // 设置语音音量
        //        synthesizer.Volume = 100;

        //        // 设置语音速度
        //        synthesizer.Rate = 1;

        //        // 获取系统上所有安装的语音
        //        var voice = synthesizer.GetInstalledVoices();

        //        // 设置音色，自动使用系统语音
        //        synthesizer.SelectVoice(voice[0].VoiceInfo.Name);

        //        // 合成文本并播放
        //        await Task.Run(() => {
        //            synthesizer.Speak(text);
        //        });
        //    }
        //}

        ///// <summary>
        ///// 识别语音转文字
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<string> SpeakTransToText() {
        //    string text = "以下是识别到的语音：";
        //    // 创建一个语音识别引擎
        //    using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine())
        //    {
        //        // 设置语音识别的语法
        //        recognizer.LoadGrammar(new DictationGrammar());

        //        // 设置语音输入为默认音频设备
        //        recognizer.SetInputToDefaultAudioDevice();

        //        // 开始异步识别过程
        //        await Task.Run(() => {
        //            recognizer.RecognizeAsync(RecognizeMode.Multiple);

        //            // 当识别到语音时触发的事件
        //            recognizer.SpeechRecognized += (sender, e) =>
        //            {
        //                // 打印识别到的文本
        //                text += e.Result.Text;

        //                if (text.Length > 15)
        //                {
        //                    recognizer.RecognizeAsyncStop();
        //                    return;
        //                }
        //            };
        //        });
        //    }
        //    return text;
        //}
        #endregion

        /// <summary>
        /// 测试异常捕获
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task Test(string ip) {
            AMAPHelper.GetAddressByIP(ip);
        }
    }
}
