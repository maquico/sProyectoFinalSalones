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
    public class TransaccionesController : Controller
    {
        private DS2_ProyectoFinalSalonesDBEntities3 db = new DS2_ProyectoFinalSalonesDBEntities3();

        // GET: Transacciones
        public ActionResult Index()
        {
            var transacciones = db.Transacciones.Include(t => t.Cliente).Include(t => t.Salone);
            return View(transacciones.ToList());
        }

        // GET: Transacciones/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccione transaccione = db.Transacciones.Find(id);
            if (transaccione == null)
            {
                return HttpNotFound();
            }
            return View(transaccione);
        }

        // GET: Transacciones/Create
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salone salones = db.Salones.Find(id);
            Transaccione transaccion = new Transaccione();
            if (salones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", transaccion.Cliente_Id);
            return View(transaccion);
        }

        // POST: Transacciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaInicio,FechaFin,Cliente_Id,Salon_Id")] Transaccione transaccione, string id)
        {
            if (ModelState.IsValid)
            {
                transaccione.Salon_Id = id;
                transaccione.Id = Guid.NewGuid().ToString();
                db.Transacciones.Add(transaccione);
                db.SaveChanges();
                return RedirectToAction("Lista", "Salones");
            }

            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", transaccione.Cliente_Id);
            ViewBag.Salon_Id = new SelectList(db.Salones, "Id", "Nombre", transaccione.Salon_Id);
            return View(transaccione);
        }

        // GET: Transacciones/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccione transaccione = db.Transacciones.Find(id);
            if (transaccione == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", transaccione.Cliente_Id);
            ViewBag.Salon_Id = new SelectList(db.Salones, "Id", "Nombre", transaccione.Salon_Id);
            return View(transaccione);
        }

        // POST: Transacciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaInicio,FechaFin,Cliente_Id,Salon_Id")] Transaccione transaccione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccione).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", transaccione.Cliente_Id);
            ViewBag.Salon_Id = new SelectList(db.Salones, "Id", "Nombre", transaccione.Salon_Id);
            return View(transaccione);
        }

        // GET: Transacciones/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccione transaccione = db.Transacciones.Find(id);
            if (transaccione == null)
            {
                return HttpNotFound();
            }
            return View(transaccione);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Transaccione transaccione = db.Transacciones.Find(id);
            db.Transacciones.Remove(transaccione);
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
