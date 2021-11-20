using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EXAMEN_FINAL1.Models;
using System.Threading;



using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
namespace EXAMEN_FINAL1.Controllers
{
    public class PlantillaController : Controller
    {
        public static Plantilla PlantillaSeleccionada;
        public static string Nombre = "";
        public static string Asunto = "";
        // GET: Plantilla
        public ActionResult Index()
        {
            CONTACTOSEntities DB = new CONTACTOSEntities();
            return View(DB.Plantilla.ToList());

        }

        // GET: Plantilla/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Plantilla/Create
        [HttpPost]
        public ActionResult Create(Plantilla a)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {

                db.Plantilla.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        // GET: Plantilla/Edit/5
        public ActionResult Edit(int id)
        {
            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {
                Plantilla al = db.Plantilla.Find(id);
                return View(al);
            }
        }

        // POST: Plantilla/Edit/5
        [HttpPost]
        public ActionResult Edit(Plantilla a)
        {
            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {
                Plantilla cont = db.Plantilla.Find(a.id);
                cont.Nombre = a.Nombre;
                cont.Contenido = a.Contenido;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: Plantilla/Delete/5
        public ActionResult Delete(int id)
        {
            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {
                Plantilla cont = db.Plantilla.Find(id);
                db.Plantilla.Remove(cont);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult SetMensaje()
        {

            ViewData["Asunto"] = Nombre;
            ViewData["Contenido"] = Asunto;
            return View();
        }

        [HttpPost]
        public ActionResult SetMensaje(Plantilla a)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {
                PlantillaSeleccionada = a;
                Nombre = a.Nombre;
                Asunto = a.Contenido;
                db.SaveChanges();
                return RedirectToAction("Index", "Contactos");
            }

        }

        public ActionResult SelectPlantilla(int id)
        {
            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {
                Plantilla cont = db.Plantilla.Find(id);
                Nombre = cont.Nombre;
                Asunto = cont.Contenido;
                db.SaveChanges();
                return RedirectToAction("SetMensaje");
            }
        }

        public ActionResult EnvioFinaliza()
        {
            return View();
        }


        public ActionResult CreateDocument()
        {
            //Create an instance of PdfDocument.

            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {
                db.CONTACTO.ToList();
                foreach (CONTACTO Recorre in db.CONTACTO.ToList())
                {
                    Doc(Recorre.NOMBRE,Recorre.EMAIL);
                }
                

            }

            return View();
        }

        public void Doc(string nombre, string correo)
        {
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for the page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Draw the text
                graphics.DrawString("\n\n\n\n\nEstimad@ "+nombre+"\n"+correo+"\n\n"+Nombre+"\n\n"+Asunto, font, PdfBrushes.Black, new PointF(0, 0));

                // Open the document in browser after saving it
                HttpContext.ApplicationInstance.Response.Clear();
                HttpContext.ApplicationInstance.Response.BufferOutput = false;
                document.Save(nombre+".pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
                document.Close(true);

            }

        }
    }
}
