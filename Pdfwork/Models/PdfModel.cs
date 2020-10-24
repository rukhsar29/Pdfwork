using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Pdfwork.Models
{
    public class PdfModel
    {
        [DisplayName("Upload File")]
        public string PdfPath { get; set; }
        public HttpPostedFileBase PdfFile { get; set; }
    }
}