using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Class1
    {
        public IEnumerable<Urun> Deger1 { get; set; }//deger1 urunu tutacak
        public IEnumerable<Detay> Deger2 { get; set; }//deger2 ise detaydan gelecek olan degerleri bir koleksiyon olarak tutuyo olacak
    }
}