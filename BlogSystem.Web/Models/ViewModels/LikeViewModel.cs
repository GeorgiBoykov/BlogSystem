using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Web.Models.ViewModels
{
    public class LikeViewModel
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string UserId { get; set; }

        public string IpAddress { get; set; }
    }
}