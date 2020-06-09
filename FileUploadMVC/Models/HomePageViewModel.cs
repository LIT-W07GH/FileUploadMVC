using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileUploadMVC.Data;

namespace FileUploadMVC.Models
{
    public class HomePageViewModel
    {
        public List<UploadedImage> Images { get; set; }
    }
}
