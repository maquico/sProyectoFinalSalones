using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinalSalones;

namespace ProyectoFinalSalones.Controllers
{
    public class SalonesController : Controller
    {
        private DS2_ProyectoFinalSalonesDBEntities db = new DS2_ProyectoFinalSalonesDBEntities();

        // GET: Salones
        public ActionResult Index()
        {
            var salones = db.Salones.Include(s => s.Propietario).Include(s => s.Tipo);
            return View(salones.ToList());
        }

        // GET: Salones/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salone salone = db.Salones.Find(id);
            if (salone == null)
            {
                return HttpNotFound();
            }
            return View(salone);
        }

        // GET: Salones/Create
        public ActionResult Create()
        {
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre");
            ViewBag.Tipo_Id = new SelectList(db.Tipoes, "Id", "Nombre");
            return View();
        }

        // POST: Salones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Superficie,Direccion,Precio,Tipo_Id,Disponibilidad,Descripcion,Imagen,Propietario_Id")] Salone salone)
        {
            if (ModelState.IsValid)
            {
                db.Salones.Add(salone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre", salone.Propietario_Id);
            ViewBag.Tipo_Id = new SelectList(db.Tipoes, "Id", "Nombre", salone.Tipo_Id);
            return View(salone);
        }

        // GET: Salones/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salone salone = db.Salones.Find(id);
            if (salone == null)
            {
                return HttpNotFound();
            }
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre", salone.Propietario_Id);
            ViewBag.Tipo_Id = new SelectList(db.Tipoes, "Id", "Nombre", salone.Tipo_Id);
            return View(salone);
        }

        // POST: Salones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Superficie,Direccion,Precio,Tipo_Id,Disponibilidad,Descripcion,Imagen,Propietario_Id")] Salone salone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre", salone.Propietario_Id);
            ViewBag.Tipo_Id = new SelectList(db.Tipoes, "Id", "Nombre", salone.Tipo_Id);
            return View(salone);
        }

        // GET: Salones/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salone salone = db.Salones.Find(id);
            if (salone == null)
            {
                return HttpNotFound();
            }
            return View(salone);
        }

        // POST: Salones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Salone salone = db.Salones.Find(id);
            db.Salones.Remove(salone);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
