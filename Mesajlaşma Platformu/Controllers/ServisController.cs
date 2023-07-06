using Mesajlaşma_Platformu.Models;
using Mesajlaşma_Platformu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mesajlaşma_Platformu.Controllers
{
    public class ServisController : ApiController
    {
        MesajlasmaPlatformuEntities db = new MesajlasmaPlatformuEntities();
        SonucModel sonuc = new SonucModel();
        #region Uye
        [HttpGet]
        [Route("api/uyelistele")]
        public List<UyeModel> UyeListele()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                kullaniciAdi = x.kullaniciAdi,
                adSoyad = x.adSoyad,
                sifre = x.sifre,
                mail = x.mail,
                telefon = x.telefon,
                yetki = x.yetki
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uye.Where(s => s.uyeId == uyeId).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                kullaniciAdi = x.kullaniciAdi,
                adSoyad = x.adSoyad,
                sifre = x.sifre,
                mail = x.mail,
                telefon = x.telefon,
                yetki = x.yetki
            }).FirstOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.kullaniciAdi == model.kullaniciAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Üye Kayıtlıdır";
                return sonuc;
            }
            Uye yeni = new Uye();
            yeni.kullaniciAdi = model.kullaniciAdi;
            yeni.adSoyad = model.adSoyad;
            yeni.sifre = model.sifre;
            yeni.mail = model.mail;
            yeni.telefon = model.telefon;
            yeni.yetki = model.yetki;
            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni üye Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı";
                return sonuc;
            }
            kayit.kullaniciAdi = model.kullaniciAdi;
            kayit.adSoyad = model.adSoyad;
            kayit.sifre = model.sifre;
            kayit.mail = model.mail;
            kayit.telefon = model.telefon;
            kayit.yetki = model.yetki;
            sonuc.islem = true;
            sonuc.mesaj = "Üye Güncellendi";
            db.SaveChanges();
            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı";
                return sonuc;
            }
            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }
        #endregion

        #region Grup
        [HttpGet]
        [Route("api/gruplistele")]
        public List<GrupModel> GrupListele()
        {
            List<GrupModel> liste = db.Grup.Select(x => new GrupModel()
            {
                grupId = x.grupId,
                groupAdi = x.groupAdi,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/grupbyid/{grupId}")]
        public GrupModel GrupById(int grupId)
        {
            GrupModel kayit = db.Grup.Where(s => s.grupId == grupId).Select(x => new GrupModel()
            {
                grupId = x.grupId,
                groupAdi = x.groupAdi,
            }).FirstOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/grupekle")]
        public SonucModel GrupEkle(GrupModel model)
        {
            if (db.Grup.Count(s => s.groupAdi == model.groupAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Adı Kayıtlıdır";
                return sonuc;
            }
            Grup yeni = new Grup();
            yeni.groupAdi = model.groupAdi;
            db.Grup.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Grup Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/grupduzenle")]
        public SonucModel GrupDuzenle(GrupModel model)
        {
            Grup kayit = db.Grup.Where(s => s.grupId == model.grupId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunamadı";
                return sonuc;
            }
            kayit.groupAdi = model.groupAdi;
            sonuc.islem = true;
            sonuc.mesaj = "Grup Güncellendi";
            db.SaveChanges();
            return sonuc;
        }

        [HttpDelete]
        [Route("api/grupsil/{grupId}")]
        public SonucModel GrupSil(int grupId)
        {
            Grup kayit = db.Grup.Where(s => s.grupId == grupId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunamadı";
                return sonuc;
            }
            db.Grup.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Grup Silindi";
            return sonuc;
        }
        #endregion

        #region GrupUye
        [HttpGet]
        [Route("api/grupuyelistele")]
        public List<GrupUyeModel> GrupUyeListele()
        {
            List<GrupUyeModel> liste = db.GrupUye.Select(x => new GrupUyeModel()
            {
                grupUyeId = x.grupUyeId,
                grupId = x.grupId,
                grupBilgi = new GrupModel
                {
                    grupId = x.Grup.grupId,
                    groupAdi = x.Grup.groupAdi
                },
                uyeBilgi = new UyeModel
                {
                    uyeId = x.Uye.uyeId,
                    kullaniciAdi = x.Uye.kullaniciAdi,
                    adSoyad = x.Uye.adSoyad,
                    sifre = x.Uye.sifre,
                    mail = x.Uye.mail,
                    telefon = x.Uye.telefon,
                    yetki = x.Uye.yetki
                }
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/grupuyebyid/{grupUyeId}")]
        public GrupUyeModel GrupUyeById(int grupUyeId)
        {
            GrupUyeModel kayit = db.GrupUye.Where(s => s.grupUyeId == grupUyeId).Select(x => new GrupUyeModel()
            {
                grupUyeId = x.grupUyeId,
                grupId = x.grupId,
                grupBilgi = new GrupModel
                {
                    grupId = x.Grup.grupId,
                    groupAdi = x.Grup.groupAdi
                },
                uyeBilgi = new UyeModel
                {
                    uyeId = x.Uye.uyeId,
                    kullaniciAdi = x.Uye.kullaniciAdi,
                    adSoyad = x.Uye.adSoyad,
                    sifre = x.Uye.sifre,
                    mail = x.Uye.mail,
                    telefon = x.Uye.telefon,
                    yetki = x.Uye.yetki
                }
            }).FirstOrDefault();
            return kayit;
        }

        [HttpGet]
        [Route("api/grupbyuye/{uyeid}")]
        public List<GrupUyeModel> GrupListeleByUye(int uyeid)
        {
            List<GrupUyeModel> liste = db.GrupUye.Where(s => s.uyeId == uyeid).Select(x => new GrupUyeModel()
            {
                grupId = x.grupId,
                uyeId = x.uyeId
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.uyeBilgi = UyeById(kayit.uyeId);
                kayit.grupBilgi = GrupById(kayit.grupId);
            }
            return liste;
        }
        [HttpGet]
        [Route("api/uyebygrup/{grupid}")]
        public List<GrupUyeModel> UyeListeleByGrup(int grupid)
        {
            List<GrupUyeModel> liste = db.GrupUye.Where(s => s.grupId == grupid).Select(x => new GrupUyeModel()
            {
                grupId = x.grupId,
                uyeId = x.uyeId
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.uyeBilgi = UyeById(kayit.uyeId);
                kayit.grupBilgi = GrupById(kayit.grupId);
            }
            return liste;
        }




        [HttpPost]
        [Route("api/grupuyeekle")]
        public SonucModel GrupUyeEkle(GrupUyeModel model)
        {
            foreach (var uyeId in model.uyeIds)
            {
                GrupUye yeni = new GrupUye();
                yeni.grupId = model.grupId;
                yeni.uyeId = uyeId;
                db.GrupUye.Add(yeni);
            }
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Grup Oluşturuldu";
            return sonuc;
        }

        [HttpPut]
        [Route("api/grupuyeduzenle")]
        public SonucModel GrupUyeDuzenle(GrupUyeModel model)
        {
            var grupUyeleri = db.GrupUye.Where(s => s.grupId == model.grupId).ToList();

            if (grupUyeleri == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunamadı";
                return sonuc;
            }

            db.GrupUye.RemoveRange(grupUyeleri);

            foreach (var uyeId in model.uyeIds)
            {
                GrupUye yeni = new GrupUye();
                yeni.grupId = model.grupId;
                yeni.uyeId = uyeId;
                db.GrupUye.Add(yeni);
            }

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Grup Güncellendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/grupuyesil/{grupUyeId}")]
        public SonucModel GrupUyeSil(int grupUyeId)
        {
            GrupUye kayit = db.GrupUye.Where(s => s.grupUyeId == grupUyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Üye Bulunamadı";
                return sonuc;
            }
            if (db.GrupUye.Count(s => s.grupId == kayit.grupId) > 1)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu grupta başka üyeler olduğu için silinemez";
                return sonuc;
            }
            db.GrupUye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Grup Üye Silindi";
            return sonuc;
        }
        #endregion

        #region Mesaj

        [HttpGet]
        [Route("api/mesajlistele")]
        public List<MesajModel> MesajListele()
        {
            List<MesajModel> liste = db.Mesaj.Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                icerik = x.icerik,
                mesajTarihi = x.mesajTarihi,
                kimdenId = x.kimdenId,
                grupId = x.grupId,
                kimdenBilgi = db.Uye.Where(u => u.uyeId == x.kimdenId).Select(u => new UyeModel()
                {
                    uyeId = u.uyeId,
                    kullaniciAdi = u.kullaniciAdi,
                    adSoyad = u.adSoyad,
                    sifre = u.sifre,
                    mail = u.mail,
                    telefon = u.telefon,
                    yetki = u.yetki
                }).FirstOrDefault(),
                grupBilgi = db.Grup.Where(g => g.grupId == x.grupId).Select(g => new GrupModel()
                {
                    grupId = g.grupId,
                    groupAdi = g.groupAdi
                }).FirstOrDefault()
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/mesajbyid/{mesajId}")]
        public MesajModel MesajById(int mesajId)
        {
            MesajModel mesaj = db.Mesaj.Where(s => s.mesajId == mesajId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                icerik = x.icerik,
                mesajTarihi = x.mesajTarihi,
                kimdenId = x.kimdenId,
                grupId = x.grupId,
                kimdenBilgi = db.Uye.Where(u => u.uyeId == x.kimdenId).Select(u => new UyeModel()
                {
                    uyeId = u.uyeId,
                    kullaniciAdi = u.kullaniciAdi,
                    adSoyad = u.adSoyad,
                    sifre = u.sifre,
                    mail = u.mail,
                    telefon = u.telefon,
                    yetki = u.yetki
                }).FirstOrDefault(),
                grupBilgi = db.Grup.Where(g => g.grupId == x.grupId).Select(g => new GrupModel()
                {
                    grupId = g.grupId,
                    groupAdi = g.groupAdi
                }).FirstOrDefault()
            }).FirstOrDefault();
            return mesaj;
        }

        [HttpGet]
        [Route("api/mesajlistelebykimden/{kimdenId}")]
        public List<MesajModel> MesajListeleByKimden(int kimdenId)
        {
            List<MesajModel> liste = db.Mesaj.Where(s => s.kimdenId == kimdenId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                icerik = x.icerik,
                mesajTarihi = x.mesajTarihi,
                kimdenId = x.kimdenId,
                grupId = x.grupId,
                kimdenBilgi = db.Uye.Where(u => u.uyeId == x.kimdenId).Select(u => new UyeModel()
                {
                    uyeId = u.uyeId,
                    kullaniciAdi = u.kullaniciAdi,
                    adSoyad = u.adSoyad,
                    sifre = u.sifre,
                    mail = u.mail,
                    telefon = u.telefon,
                    yetki = u.yetki
                }).FirstOrDefault(),
                grupBilgi = db.Grup.Where(g => g.grupId == x.grupId).Select(g => new GrupModel()
                {
                    grupId = g.grupId,
                    groupAdi = g.groupAdi
                }).FirstOrDefault()
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/mesajlistelebygrup/{grupId}")]
        public List<MesajModel> MesajListeleByGrup(int grupId)
        {
            List<MesajModel> liste = db.Mesaj.Where(s => s.grupId == grupId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                icerik = x.icerik,
                mesajTarihi = x.mesajTarihi,
                kimdenId = x.kimdenId,
                grupId = x.grupId,
                kimdenBilgi = db.Uye.Where(u => u.uyeId == x.kimdenId).Select(u => new UyeModel()
                {
                    uyeId = u.uyeId,
                    kullaniciAdi = u.kullaniciAdi,
                    adSoyad = u.adSoyad,
                    sifre = u.sifre,
                    mail = u.mail,
                    telefon = u.telefon,
                    yetki = u.yetki
                }).FirstOrDefault(),
                grupBilgi = db.Grup.Where(g => g.grupId == x.grupId).Select(g => new GrupModel()
                {
                    grupId = g.grupId,
                    groupAdi = g.groupAdi
                }).FirstOrDefault()
            }).ToList();
            return liste;
        }

        //MESAJ EKLEME YAPARKEN GRUPID BOŞ GİRERSE DEMEK TOPLU BİR MESAJ GÖNDERİYOR EĞER GRUP İD VARSA DEMEK GRUBA MESAJ ATIYOR
        [HttpPost]
        [Route("api/mesajekle")]
        public SonucModel MesajEkle(MesajModel model)
        {
            Mesaj yeni = new Mesaj();
            yeni.icerik = model.icerik;
            yeni.mesajTarihi = model.mesajTarihi;
            yeni.kimdenId = model.kimdenId;
            yeni.grupId = model.grupId;
            db.Mesaj.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Mesaj Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/mesajduzenle")]
        public SonucModel MesajDuzenle(MesajModel model)
        {
            Mesaj kayit = db.Mesaj.Where(s => s.mesajId == model.mesajId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunamadı";
                return sonuc;
            }

            kayit.icerik = model.icerik;
            kayit.mesajTarihi = model.mesajTarihi;
            kayit.kimdenId = model.kimdenId;
            kayit.grupId = model.grupId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/mesajsil/{mesajId}")]
        public SonucModel MesajSil(int mesajId)
        {
            Mesaj kayit = db.Mesaj.Where(s => s.mesajId == mesajId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunamadı";
                return sonuc;
            }

            db.Mesaj.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silindi";
            return sonuc;
        }

        #endregion

        #region TopluMesaj

        [HttpGet]
        [Route("api/alicilarlistele")]
        public List<AlicilarModel> AlicilarListele()
        {
            List<AlicilarModel> liste = db.Alicilar.Select(x => new AlicilarModel()
            {
                id = x.id,
                mesajId = x.mesajId,
                aliciId = x.aliciId,
                mesajBilgi = db.Mesaj.Where(m => m.mesajId == x.mesajId).Select(m => new MesajModel()
                {
                    mesajId = m.mesajId,
                    icerik = m.icerik,
                    mesajTarihi = m.mesajTarihi,
                    kimdenId = m.kimdenId,
                    grupId = m.grupId,
                    kimdenBilgi = db.Uye.Where(u => u.uyeId == m.kimdenId).Select(u => new UyeModel()
                    {
                        uyeId = u.uyeId,
                        kullaniciAdi = u.kullaniciAdi,
                        adSoyad = u.adSoyad,
                        sifre = u.sifre,
                        mail = u.mail,
                        telefon = u.telefon,
                        yetki = u.yetki
                    }).FirstOrDefault(),
                    grupBilgi = db.Grup.Where(g => g.grupId == m.grupId).Select(g => new GrupModel()
                    {
                        grupId = g.grupId,
                        groupAdi = g.groupAdi
                    }).FirstOrDefault()
                }).FirstOrDefault(),
                aliciBilgi = db.Uye.Where(u => u.uyeId == x.aliciId).Select(u => new UyeModel()
                {
                    uyeId = u.uyeId,
                    kullaniciAdi = u.kullaniciAdi,
                    adSoyad = u.adSoyad,
                    sifre = u.sifre,
                    mail = u.mail,
                    telefon = u.telefon,
                    yetki = u.yetki
                }).FirstOrDefault()
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/alicilarbymesajid/{mesajId}")]
        public List<AlicilarModel> AlicilarByMesajId(int mesajId)
        {
            List<AlicilarModel> liste = db.Alicilar.Where(a => a.mesajId == mesajId).Select(x => new AlicilarModel()
            {
                id = x.id,
                mesajId = x.mesajId,
                aliciId = x.aliciId,
                mesajBilgi = db.Mesaj.Where(m => m.mesajId == x.mesajId).Select(m => new MesajModel()
                {
                    mesajId = m.mesajId,
                    icerik = m.icerik,
                    mesajTarihi = m.mesajTarihi,
                    kimdenId = m.kimdenId,
                    grupId = m.grupId,
                    kimdenBilgi = db.Uye.Where(u => u.uyeId == m.kimdenId).Select(u => new UyeModel()
                    {
                        uyeId = u.uyeId,
                        kullaniciAdi = u.kullaniciAdi,
                        adSoyad = u.adSoyad,
                        sifre = u.sifre,
                        mail = u.mail,
                        telefon = u.telefon,
                        yetki = u.yetki
                    }).FirstOrDefault(),
                    grupBilgi = db.Grup.Where(g => g.grupId == m.grupId).Select(g => new GrupModel()
                    {
                        grupId = g.grupId,
                        groupAdi = g.groupAdi
                    }).FirstOrDefault()
                }).FirstOrDefault(),
                aliciBilgi = db.Uye.Where(u => u.uyeId == x.aliciId).Select(u => new UyeModel()
                {
                    uyeId = u.uyeId,
                    kullaniciAdi = u.kullaniciAdi,
                    adSoyad = u.adSoyad,
                    sifre = u.sifre,
                    mail = u.mail,
                    telefon = u.telefon,
                    yetki = u.yetki
                }).FirstOrDefault()
            }).ToList();
            return liste;
        }



        [HttpPost]
        [Route("api/alicilarekle")]
        public SonucModel AlicilarEkle(AlicilarModel model)
        {
            foreach (var aliciId in model.aliciIds)
            {
                Alicilar yeni = new Alicilar();
                yeni.mesajId = model.mesajId;
                yeni.aliciId = aliciId;
                db.Alicilar.Add(yeni);
            }
            db.SaveChanges();
            
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Gruba Gönderildi";
            return sonuc;
        }

        #endregion

    }
}
