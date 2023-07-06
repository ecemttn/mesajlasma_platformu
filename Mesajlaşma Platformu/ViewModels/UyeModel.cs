using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mesajlaşma_Platformu.ViewModels
{
    public class UyeModel
    {
        public int uyeId { get; set; }
        public string kullaniciAdi { get; set; }
        public string adSoyad { get; set; }
        public string sifre { get; set; }
        public string mail { get; set; }
        public string telefon { get; set; }
        public int yetki { get; set; }
    }
}