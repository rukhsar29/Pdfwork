using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Pdfwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

namespace Pdfwork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(PdfModel pdf)
        {
            //string FilePath = Server.MapPath("pdf.PdfFile");
            string FileName = System.IO.Path.GetFileNameWithoutExtension(pdf.PdfFile.FileName);
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
            pdf.PdfPath = UploadPath + FileName;
            string FilePath = pdf.PdfPath;
            pdf.PdfFile.SaveAs(pdf.PdfPath);
            StringBuilder sb = new StringBuilder();
            //if (File.Exists(FilePath))
            //{
            var db = new pdfworkEntities();
            pdftext pd = new pdftext();

            using (PdfReader reader = new PdfReader(FilePath))
            {
                ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string ExtractedData = string.Empty;
                    ExtractedData = PdfTextExtractor.GetTextFromPage(reader, i);

                    //string[] lines = ExtractedData.Split('\n');

                    //foreach (string line in lines)
                    //{
                    //    pd.lines = line;
                    //    db.pdftext.Add(pd);
                    //    db.SaveChanges();
                    //}
                    List<string> list = new List<string>();
                    list.AddRange(ExtractedData.Split('\n'));
                   for(int j = 0; j < list.Count; j++)
                    {
                        pd.lines = list[j];
                          db.pdftext.Add(pd);
                          db.SaveChanges();

                    }


                }

            }
           
            //}
           
            return View();
        }


    }
}