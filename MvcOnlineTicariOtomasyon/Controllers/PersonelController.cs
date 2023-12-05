using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        Context c = new Context();
        // GET: Personel
        public ActionResult Index()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            //sunucuya yüklenmiş en az bir dosya olup olmadığını kontrol eder.
            if (Request.Files.Count > 0)//eğer bir dosya tutuyor isem yani isteklerim arasında bir dosya var ise
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);//dosyanın ismini al hangi dosyanın ismini alıcam. Request de tutmus olduğum o hafızadaki değerin filename ni al. 
                //string uzanti = Path.GetExtension(Request.Files[0].FileName);//yukardaki kod ile dosya adını alıyor bu satırda yazılan kodda ise uzantıyı alıyor getextension ile.
                string yol = "~/Image/" + dosyaadi; //+uzanti da olması gerekiyordu aslında ama dosya yolundan jpeg geldiği için bir daha dosya yolunu eklemedik
                Request.Files[0].SaveAs(Server.MapPath(yol));//hafızaya almış olduğum bu dosyayı farklı kaydet. Nereye kaydediceksin serverdaki sunucu içerisindeki mappath(yol) değişkeninden gelen değere 
                p.PersonelGorsel= "/Image/" + dosyaadi;//veritabanına personelgörseli kaydetmek için gerekicek olan değer.
            }
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();


            ViewBag.dgr1 = deger1;
            var prs = c.Personels.Find(id);

            return View("PersonelGetir", prs);
        }
        public ActionResult PersonelGuncelle(Personel p)
        {
            if (Request.Files.Count > 0)//eğer bir dosya tutuyor isem yani isteklerim arasında bir dosya var ise
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);//dosyanın ismini al hangi dosyanın ismini alıcam. Request de tutmus olduğum o hafızadaki değerin filename ni al. 
                //string uzanti = Path.GetExtension(Request.Files[0].FileName);//yukardaki kod ile dosya adını alıyor bu satırda yazılan kodda ise uzantıyı alıyor getextension ile.
                string yol = "~/Image/" + dosyaadi;
                Request.Files[0].SaveAs(Server.MapPath(yol));//hafızaya almış olduğum bu dosyayı farklı kaydet. Nereye kaydediceksin serverdaki sunucu içerisindeki mappath(yol) değişkeninden gelen değere 
                p.PersonelGorsel = "/Image/" + dosyaadi ;//veritabanına personelgörseli kaydetmek için gerekicek olan değer.
            }
            var prsn = c.Personels.Find(p.Personelid);
            prsn.PersonelAd = p.PersonelAd;
            prsn.PersonelSoyad = p.PersonelSoyad;
            prsn.PersonelGorsel = p.PersonelGorsel;
            prsn.Departmanid = p.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }
    }
}