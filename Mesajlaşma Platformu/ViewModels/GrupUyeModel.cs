using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mesajlaşma_Platformu.ViewModels
{
    public class GrupUyeModel
    {
        public int grupUyeId { get; set; }
        public int grupId { get; set; }
        public List<int> uyeIds { get; set; }
        public int uyeId { get; set; }
        public GrupModel grupBilgi { get; set; }
        public UyeModel uyeBilgi { get; set; }
    }
}