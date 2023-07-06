using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mesajlaşma_Platformu.ViewModels
{
    public class AlicilarModel
    {
        public int id { get; set; }
        public int mesajId { get; set; }
        public int aliciId { get; set; }
        public List<int> aliciIds { get; set; }
        public MesajModel mesajBilgi { get; set; }
        public UyeModel aliciBilgi{ get; set; }

    }
}