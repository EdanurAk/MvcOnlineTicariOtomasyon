using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Index2()
        {
            var grafikciz = new Chart(600, 600);
            grafikciz.AddTitle("Kategori-Ürün Stok Sayısı").AddLegend("Stok").AddSeries
                ("Değerler", xValue: new[] { "Mobilya", "Ofis Eşyaları", "Bilgisayar" }, yValues: new[]
                {85,66,98}).Write();
            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        }

        Context c = new Context();
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAd));//xvalue içine urunaddan gelen değerleri ekler
            sonuclar.ToList().ForEach(y => yvalue.Add(y.Stok));//yvalue içerisine de stok miktaırını ekle
            var grafik = new Chart(width: 800, height: 800)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Index4()
        {

            return View();
        }

        public ActionResult VisualizeUrunResult()
        {

            return Json(UrunListesi(), JsonRequestBehavior.AllowGet);//googlechartlardaki görselleştirmeye ulaşabilmek için JsonRequrstBehavior kullanmak gerekiyor.
        }
        public List<Sinif1> UrunListesi()
        {
            List<Sinif1> snf = new List<Sinif1>();
            snf.Add(new Sinif1()//amacımız sınıf1 üzerinde bulunan propertylerimize snf aracılığı ile veri aktarımı yapmak
            {
                urunad = "Bilgisayar",
                stok = 120
            });
            snf.Add(new Sinif1()
            {
                urunad = "Beyaz Eşya",
                stok = 150
            });
            snf.Add(new Sinif1()
            {
                urunad = "Beyaz Eşya",
                stok = 150
            });
            snf.Add(new Sinif1()
            {
                urunad = "Mobilya",
                stok = 70
            });
            snf.Add(new Sinif1()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 180
            });
            snf.Add(new Sinif1()
            {
                urunad = "Mobil Cihazlar",
                stok = 90
            });
            return snf;
        }
        public ActionResult Index5()
        {

            return View();
        }
        public ActionResult VisualizeUrunResult2()
        {

            List<Sinif2> snf = new List<Sinif2>();
            using (var context = new Context())
            {
                snf = c.Uruns.Select(x => new Sinif2 //x ürünler tablonuzdaki değerleri tutuyor
                {
                    urn = x.UrunAd,//urunler tablosundaki ürün adları sinif2 ye atıyor
                    stk = x.Stok//urunler tablosundaki stokları sinif2 ye atıyor
                }).ToList();

            }

            return Json(snf, JsonRequestBehavior.AllowGet);

        }



        public ActionResult Index6()
        {

            return View();
        }
        public ActionResult Index7()
        {

            return View();
        }
    }
}