using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Class3
    {
        public IEnumerable <SelectListItem> Kategoriler { get; set; }//bu propertyler bir liste formatında olacağı için IEnumerable olarak tutuyoruz.
        public IEnumerable<SelectListItem> Urunler  { get; set; }
    }
}