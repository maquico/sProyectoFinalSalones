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
            ViewModel viewModel = new ViewModel();
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                viewModel.objetoSalone = db.Salones.Find(id);
                viewModel.salones = db.Salones.AsEnumerable();
                viewModel.transaccion = db.Transacciones.AsEnumerable();
                Transaccione transaccion = new Transaccione();
                viewModel.objetoTransaccion = transaccion;
                if (viewModel.objetoSalone == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", viewModel.objetoTransaccion.Cliente_Id);
            }
           
            return View(viewModel);
        }

        // POST: Transacciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaInicio,FechaFin,Cliente_Id,Salon_Id")] Transaccione transaccion1, ViewModel viewModel, string id)
        {
            bool choque = false;
            viewModel.objetoTransaccion = transaccion1;
            viewModel.objetoTransaccion.FechaInicio = viewModel.objetoTransaccion.FechaInicio.ToLocalTime();
            viewModel.objetoTransaccion.FechaFin = viewModel.objetoTransaccion.FechaFin.ToLocalTime();
            viewModel.transaccion = db.Transacciones.AsNoTracking().AsEnumerable();
            foreach (Transaccione transaccion in viewModel.transaccion)
            {
                if ((viewModel.objetoTransaccion.FechaInicio >= transaccion.FechaInicio && viewModel.objetoTransaccion.FechaInicio <= transaccion.FechaFin) || (viewModel.objetoTransaccion.FechaFin >= transaccion.FechaInicio && viewModel.objetoTransaccion.FechaFin <= transaccion.FechaFin))
                {
                    choque = true;
                    return RedirectToAction("Lista","Salones");
                    
                }
            }
            if (ModelState.IsValid && choque == false)
            {
                viewModel.objetoTransaccion.Salon_Id = id;
                viewModel.objetoTransaccion.Id = Guid.NewGuid().ToString();
                db.Transacciones.Add(viewModel.objetoTransaccion);
                db.SaveChanges();

                return RedirectToAction("Lista", "Salones");
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", viewModel.objetoTransaccion.Cliente_Id);
            ViewBag.Salon_Id = new SelectList(db.Salones, "Id", "Nombre", viewModel.objetoTransaccion.Salon_Id);
            return View(viewModel);

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
            bool error = false;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                error = true;

            }
            if (error)
            {
                return RedirectToAction("Error", "Salones");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
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
