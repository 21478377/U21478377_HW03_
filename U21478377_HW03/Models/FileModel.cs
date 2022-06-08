using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace U21478377_HW03.Models
{
    public class FileModel
    {
        [Required]
        public string FileName { get; set; }
        private static List<FileModel> files;
         public List<FileModel> files = new List<FileModel>();
        files = _FileModel.GetFilesList();  
            FileModel.Studentmodel = files;  
             return private RedirectToAction("Files");
    }
}
