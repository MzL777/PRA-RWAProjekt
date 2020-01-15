using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicSite.Models
{
    public class JelovnikPreGenerateData
    {

        [Required(ErrorMessage = "Niste odabrali broj obroka!")]
        public int BrojObroka { get; set; }

        [Required(ErrorMessage = "Niste odabrali datum!")]
        [DataType(dataType: DataType.Date, ErrorMessage = "U polje datum mora biti upisan valjani datum!")]
        public DateTime Datum { get; set; }

        public Jelovnik Jelovnik { get; set; }
    }
}