using System;
using System.Collections.Generic;

namespace ElectronicLicenceServer.Models
{
    public partial class User
    {
        public string IdNum { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Xsz { get; set; }
        public bool? Jsz { get; set; }
        public int Id { get; set; }
    }
}
