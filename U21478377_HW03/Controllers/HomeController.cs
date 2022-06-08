using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using U21478377_HW03.Models;
using System.IO;
using Grpc.Core;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using Microsoft.Build.Tasks;

namespace U21478377_HW03.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Home()
        {
            return View();
        }
     
       //file upload
        //save into image file
        [HttpPost]
        public IActionResult Home(IFormFile fileSave)
        {
            if (fileSave != null)
            {
                if (fileSave.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(fileSave.FileName);

                    //Assigning Unique Filename (Guid)
                    var UniqueFName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExt = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(UniqueFName, fileExt);

                    //check file type
                    string[] AllowedDocExtensions = new string[] { ".sdf", ".docx", ".dat", ".pdf", ".txt" };
                    string[] AllowedImageExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".psd" };
                    string[] AllowedVideoExtensions = new string[] { ".mp4", ".mp5", ".mkv", ".KINE", ".wmv" };

                    try
                    {
                        //doc
                        if (AllowedDocExtensions.Contains(fileSave.FileName.Substring(fileSave.FileName.LastIndexOf('.'))))
                        {
                            // store the file inside ~/Media/Documents folder
                            // Combines two strings into a path.
                            var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Media", "Docuement")).Root + $@"\{newFileName}";


                            using (FileStream fs = System.IO.File.Create(filepath))
                            {
                                fileSave.CopyTo(fs);
                                fs.Flush();
                            }
                        }

                        //image
                        if (AllowedImageExtensions.Contains(fileSave.FileName.Substring(fileSave.FileName.LastIndexOf('.'))))
                        {
                            // store the file inside ~/Media/Documents folder
                            // Combines two strings into a path.
                            var filepath =
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Media", "Images")).Root + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(filepath))
                            {
                                fileSave.CopyTo(fs);
                                fs.Flush();
                            }
                        }

                        //video
                        if (AllowedVideoExtensions.Contains(fileSave.FileName.Substring(fileSave.FileName.LastIndexOf('.'))))
                        {
                            // store the file inside ~/Media/Documents folder
                            // Combines two strings into a path.
                            var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Media", "Videos")).Root + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(filepath))
                            {
                                fileSave.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                    catch (Exception y)
                    {
                        ViewBag.Message = "ERROR" + y.Message.ToString();
                    }

                }
            }
            else
            {
                ViewBag.Message = "You have not selected a file to upload";
            }

            // redirect back to the home page= home action
            return RedirectToAction("Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Files()
        {
            //use FileModel model
            return View();


        }
        [HttpGet]
        //delete files
        
        public ActionResult DeleteFile(FileModel files)
        {
            //return file view
            return RedirectToAction("Files");

        }
        public ActionResult DeleteFile(int vid)
        {
            //return video view
            return RedirectToAction("Video");
        }

        public FileResult iDownload(string ImageName)
        {
            var FileVirtualPath = "~/Media/Images/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }
        public FileResult dDownload(string DocName)
        {
            var FileVirtualPath = "~/Media/Documents/" + DocName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }
        public FileResult vDownload(string VidName)
        {
            var FileVirtualPath = "~/Media/Videos/" + VidName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }
        public IActionResult Images()
        {
            return View();
        }
        public IActionResult Videos()
        {
            return View();
        }
        public IActionResult AboutMe()
        {
            return View();
        }
       
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
