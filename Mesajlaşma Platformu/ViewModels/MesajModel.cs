using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mesajlaşma_Platformu.ViewModels
{
    public class MesajModel
    {
        public int mesajId { get; set; }
        public string icerik { get; set; }
        public System.DateTime mesajTarihi { get; set; }
        public int kimdenId { get; set; }
        public Nullable<int> grupId { get; set; }
        public UyeModel kimdenBilgi { get; set; }
        public GrupModel grupBilgi { get; set; }
    }
}