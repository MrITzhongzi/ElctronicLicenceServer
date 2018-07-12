using System;
using System.Collections.Generic;

namespace ElectronicLicenceServer.Models
{
    public partial class Card
    {
        public long Id { get; set; }
        public string CardId { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public long? Uid { get; set; }
        public string Cllx { get; set; }
        public string Hphm { get; set; }
        public string Sfzmhm { get; set; }
    }
}
