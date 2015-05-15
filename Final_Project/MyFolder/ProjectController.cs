﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Web.Mvc;  
// need to add reference to System.Web
using System.Net;       // need to add reference to System.Net
using System.Net.Http;  // need to add reference to System.Net.Http
using Newtonsoft.Json;  // need to add reference to System.Json
using System.Threading;
using System.Web.UI.HtmlControls;


namespace Final_Project.Controllers
{
    
    public class ProjectController : Controller
    {
        private HttpClient client = new HttpClient();
        private HttpRequestMessage message;
        private string urlBase;
        private HttpResponseMessage response = new HttpResponseMessage();
        protected System.Web.UI.HtmlControls.HtmlInputFile File1 = new HtmlInputFile();
        public string status { get; set; }
        //
        // GET: /Project/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Projectdownload()
        {
            ViewBag.Message = "Get all the available files:";
            ViewBag.Filenames = getAvailableFiles();
            
            return View();
        }
        public ActionResult Singlefiledownload(string id)
        {
            downLoadFile(id);
            return View();
        }
        public ActionResult Projectupload()
        {
            //string uploadFile = "foobar.txt";
            //upLoadFile(uploadFile);
            return View();
        }

        //public ActionResult Singlefileupload(object sender, System.EventArgs e)
        [HttpPost]
        public ActionResult Singlefileupload(HttpPostedFileBase file)
        {
            //HttpPostedFile postedfile;

            
                //HttpPostedFile postedFile = File1.PostedFile;       
            //postedFile = Request.Files.Get(0);
                int contentLength = file.ContentLength;
                string contentType = file.ContentType;
                ViewBag.filename = "xxx";
                string fileName = file.FileName;
                string uploadFile = fileName;

                //// extract only the fielname
                //var fileName = Path.GetFileName(file.FileName);
                //// store the file inside ~/App_Data/uploads folder
                //var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);

                upLoadFile(uploadFile, file);
                
            
            
            return View();
        }

        string[] getAvailableFiles()
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            urlBase = "http://localhost:61626/api/Projectdownload";
            message.RequestUri = new Uri(urlBase);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response1 = task.Result;
            response = task.Result;
            status = response.ReasonPhrase;
            string[] files = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(response1.Content.ReadAsStringAsync().Result);
            return files;
        }
        int openServerDownLoadFile(string fileName)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            urlBase = "http://localhost:61626/api/Projectdownload";
            string urlActn = "?fileName=" + fileName + "&open=download";
            ViewBag.urlactn = urlActn;
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            status = response.ReasonPhrase;
            ViewBag.message4 = "Opened Server Download file";
            return (int)response.StatusCode;
        }
        //----< open file on client for writing >------------------------------

        FileStream openClientDownLoadFile(string fileName)
        {
            ViewBag.Message2 = "Opened Client Download File";
            string path = "../../DownLoad/";

            FileStream down;
            try
            {
                down = new FileStream(path + fileName, FileMode.OpenOrCreate);
            }
            catch
            {
                return null;
            }
            ViewBag.Message2 = "Opened Client Download File";
            return down;
        }
        byte[] getFileBlock(FileStream down, int blockSize)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            urlBase = "http://localhost:61626/api/Projectdownload";
            string urlActn = "?blockSize=" + blockSize.ToString();
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            Task<byte[]> taskb = response.Content.ReadAsByteArrayAsync();
            byte[] Block = taskb.Result;
            status = response.ReasonPhrase;
            return Block;
        }
        void downLoadFile(string filename)
        {
            Console.Write("\n  Attempting to download file {0} ", filename);
            Console.Write("\n ------------------------------------------\n");

            FileStream down;
            Console.Write("\n  Sending Get request to open file");
            Console.Write("\n ----------------------------------");
            int status = openServerDownLoadFile(filename);
            ViewBag.openserverfilebackstatus = status;
           
            if (status >= 400)
                return;
            down = openClientDownLoadFile(filename);

            Console.Write("\n  Sending Get requests for block from file");
            Console.Write("\n ------------------------------------------");
            while (true)
            {
                int blockSize = 512;
                byte[] Block = getFileBlock(down, blockSize);

                if (Block.Length == 0 || blockSize <= 0)
                    break;
                down.Write(Block, 0, Block.Length);
                if (Block.Length < blockSize)    // last block
                    break;
            }
        }


        int openServerUpLoadFile(string fileName)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            urlBase = "http://localhost:61626/api/Projectdownload";
            string urlActn = "?fileName=" + fileName + "&open=upload";
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            status = response.ReasonPhrase;
            return (int)response.StatusCode;
        }
        //----< open file on client for Reading >------------------------------

        FileStream openClientUpLoadFile(string fileName)
        {
            string path = "../UpLoad/";

            FileStream up;
            try
            {
                up = new FileStream(path + fileName, FileMode.Open);
            }
            catch
            {
                return null;
            }
            return up;
        }
        void putBlock(byte[] Block)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            message.Content = new ByteArrayContent(Block);
            message.Content.Headers.Add("Content-Type", "application/http;msgtype=request");
            string urlActn = "?blockSize=" + Block.Count().ToString();
            urlBase = "http://localhost:61626/api/Projectdownload";
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            status = response.ReasonPhrase;
        }
        void closeServerFile()
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            string urlActn = "?fileName=dontCare.txt&open=close";
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            status = response.ReasonPhrase;
        }
        void upLoadFile(string filename, HttpPostedFileBase file)
        {
           

           int rq = openServerUpLoadFile(filename);
           // FileStream up = openClientUpLoadFile(filename);
            Stream up = file.InputStream;
            const int upBlockSize = 512;
            byte[] upBlock = new byte[upBlockSize];
            int bytesRead = upBlockSize;
            while (bytesRead == upBlockSize)
            {
                bytesRead = up.Read(upBlock, 0, upBlockSize);
                if (bytesRead < upBlockSize)
                {
                    byte[] temp = new byte[bytesRead];
                    for (int i = 0; i < bytesRead; ++i)
                        temp[i] = upBlock[i];
                    upBlock = temp;
                }
               
                putBlock(upBlock);
              
            }

  
            closeServerFile();
 
            up.Close();
        }
    }

}