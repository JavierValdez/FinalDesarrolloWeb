using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EXAMEN_FINAL1.Models;
namespace EXAMEN_FINAL1.Controllers
{
    public class ContactosController : Controller
    {
        // GET: Contactos
        public ActionResult Index()
        {
            CONTACTOSEntities DB = new CONTACTOSEntities();
            return View(DB.CONTACTO.ToList());
        }

        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(CONTACTO a)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {

                db.CONTACTO.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }


        public ActionResult Editar(int id)
        {
            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {
                CONTACTO al = db.CONTACTO.Find(id);
                return View(al);
            }
        }
        [HttpPost]
        public ActionResult Editar(CONTACTO a)
        {
            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {
                CONTACTO cont = db.CONTACTO.Find(a.ID);
                cont.DIRECCION = a.DIRECCION;
                cont.EMAIL = a.EMAIL;
                cont.NOMBRE = a.NOMBRE;
                cont.TELEFONO = a.TELEFONO;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (CONTACTOSEntities db = new CONTACTOSEntities())
            {
                CONTACTO cont = db.CONTACTO.Find(id);
                db.CONTACTO.Remove(cont);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


    }
}