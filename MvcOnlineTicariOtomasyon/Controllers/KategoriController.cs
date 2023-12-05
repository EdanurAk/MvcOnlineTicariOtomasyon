using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int sayfa=1)//başlangıç degerini 1 verdim yani sayfa 1 den başlıcak
        {
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa,4);//her sayfada kaç tane veri olcak 4 ayarladık
            return View(degerler);
        }
        [HttpGet]//sayfa yüklendiği zaman view açıldığı zaman yani burayı calıstır
        public ActionResult KategoriEkle()
        {

            return View();
        }
        [HttpPost]//sayfada bir post işlemi gerçekleştiği zaman burayı çalıştır.
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KategoriGetir(int id)
        {
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGetir",kategori);
        }
        [HttpPost]
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktgr = c.Kategoris.Find(k.KategoriID);
            ktgr.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {//bu işlem sayesinde dropdownlist de seçilen kategorinin ürünleri alt dropdownlistte gelecek.
            Class3 cs = new Class3();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns,"Urunid","UrunAd");
            return View(cs);
        }
        public JsonResult UrunGetir(int p)//dışardan gelen parametre bir id olcak dropdownlist ile seçilen kategorinin id sini tutacak o id ye ait kategorinin ürünlerini seçtirecek 
        {
            var urunlistesi = (from x in c.Uruns//kategori id si p den gelen değere eşit olan ürünleri listeliyor
                               join y in c.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.Urunid.ToString()
                               }).ToList();
            return Json(urunlistesi,JsonRequestBehavior.AllowGet);
        }
    }
}