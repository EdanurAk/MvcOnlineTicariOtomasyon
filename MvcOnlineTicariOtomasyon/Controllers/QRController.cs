using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string kod)
        {
            using (MemoryStream ms=new MemoryStream())//memorystream genelde resim oluşturma resim çizme resim üstüne yazı yazma gibi komutarda kullanılır
            {
                QRCodeGenerator koduret = new QRCodeGenerator();
                QRCodeGenerator.QRCode karekod = koduret.CreateQrCode(kod,QRCodeGenerator.ECCLevel.Q);
                using(Bitmap resim=karekod.GetGraphic(10))//10 dediğim gelen qr kodun çözünürlüğü 10 iyi bir değer.
                {
                    resim.Save(ms, ImageFormat.Png);//resmi ms bağlı olarak kaydedicek  ve png olarak kaydetsin diyorum
                    ViewBag.karekodimage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());//base64 arrayleri metine çeviriyo
                }
                        }
            return View();
        }
    }
}