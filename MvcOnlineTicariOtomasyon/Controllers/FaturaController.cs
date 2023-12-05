using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir", fatura);
        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.Faturaid);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSıraNo = f.FaturaSıraNo;
            fatura.Saat = f.Saat;
            fatura.Tarih = f.Tarih;
            fatura.TeslimAlan = f.TeslimAlan;
            fatura.TeslimEden = f.TeslimEden;
            fatura.VergiDairesi = f.VergiDairesi;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Dinamik()
        {
            //List<SelectListItem> deger3 = (from x in c.Uruns.ToList()
            //                               select new SelectListItem
            //                               {
            //                                   Text = x.UrunAd,
            //                                   Value = x.Urunid.ToString()
            //                               }).ToList();
            //ViewBag.dgr3 = deger3;
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            cs.deger3 = c.Uruns.Distinct().ToList();
            cs.deger4 = c.Carilers.ToList();
            cs.deger5 = c.Personels.ToList();
            return View(cs);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo,
            DateTime Tarih, string VergiDairesi, string Saat, string TeslimEden, string TeslimAlan,
            string Toplam, FaturaKalem[] kalemler)
        {//üzerine kdv ekle toplam yerine yüzdelik dilimle kdvli kdvsiz göster
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            //f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            foreach (var x in kalemler)//x ler benim parametreden gelen değerlerimi tutacak
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;//fk faturakalem tablomdaki propertylerimi tutuyor x ise parametreden gelen değerleri
                fk.BirimFiyat = x.BirimFiyat;
                //fk.Faturaid = x.FaturaKalemid;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                c.FaturaKalems.Add(fk);//fk den gelen degerleri faturakalem tabloma eklesin
            }
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}