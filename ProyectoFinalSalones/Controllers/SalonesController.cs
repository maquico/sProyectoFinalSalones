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
        private DS2_ProyectoFinalSalonesDBEntities3 db = new DS2_ProyectoFinalSalonesDBEntities3();

        // GET: Salones
        public ActionResult Index()
        {
            
            return View(db.Salones.ToList());
        }

        public ActionResult Lista()
        {
            var viewModel = new ViewModel();
            {
                viewModel.salones = db.Salones.ToList();
                viewModel.transaccion = db.Transacciones.ToList();
            }

            return View(viewModel);
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
        public ActionResult DetallesLista(string id)
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
            return View();
        }

        // POST: Salones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,Superficie,Direccion,Precio,Disponibilidad,Descripcion,Imagen,Propietario_Id")] Salone salone, HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {
                salone.Id = Guid.NewGuid().ToString();
                if (file != null)
                {
                    string imagenUrl = System.IO.Path.GetFileName(file.FileName);
                    string pathUrl = System.IO.Path.Combine(Server.MapPath("/Public/Icons"), imagenUrl);
                    file.SaveAs(pathUrl);

                    salone.Imagen = imagenUrl;
                }
                db.Salones.Add(salone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre", salone.Propietario_Id);
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

            return View(salone);
        }

      


        // POST: Salones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Superficie,Direccion,Precio,Disponibilidad,Descripcion,Imagen,Propietario_Id")] Salone salone, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    Salone salonActual = db.Salones.AsNoTracking().FirstOrDefault(x => x.Id == salone.Id);

                    string imagenFinal = salonActual.Imagen;

                    salone.Imagen = imagenFinal;
                }
                if (file != null)
                {
                    string imagenUrl = System.IO.Path.GetFileName(file.FileName);
                    string pathUrl = System.IO.Path.Combine(Server.MapPath("/Public/Icons"), imagenUrl);
                    file.SaveAs(pathUrl);

                    salone.Imagen = imagenUrl;
                }
                
                db.Entry(salone).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
           
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre", salone.Propietario_Id);
            return View(salone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        
        
       

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
