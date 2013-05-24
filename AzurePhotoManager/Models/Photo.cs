using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzurePhotoManager.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Notes { get; set; }
        public string UserId { get; set; }
    }
}