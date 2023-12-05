using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]//bu Authorize ı en tepeye eklersem tüm controllar içindeki actionlar için geçerli olacak.
    public class DepartmanController : Controller
    {
        Context c = new Context();
        // GET: Departman
        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
        [Authorize(Roles ="A")]//yetkisi A olanlar yalnızca departman ekleyebilir.
        [HttpGet]
        public ActionResult DepartmanEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman p)
        {
            c.Departmans.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var dep = c.Departmans.Find(id);
            dep.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var dpt = c.Departmans.Find(id);

            return View("DepartmanGetir",dpt);
        }
        public ActionResult DepartmanGuncelle(Departman p)
        {
            var dpt = c.Departmans.Find(p.Departmanid);
            dpt.DepartmanAd = p.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanDetay(int id)
        {
          var degerler= c.Personels.Where(x => x.Departmanid == id).ToList();
            var dpt = c.Departmans.Where(x => x.Departmanid == id).Select(x=>x.DepartmanAd).FirstOrDefault();
            ViewBag.d = dpt;
            return View(degerler);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Personelid == id).OrderByDescending(y => y.Tarih).ToList();
            var sts = c.SatisHarekets.Where(x => x.Personelid == id).Select(y => y.Personel.PersonelAd +"  "+ y.Personel.PersonelSoyad ).FirstOrDefault();
            ViewBag.dpers = sts;
            return View(degerler);
        }
         
    }
}