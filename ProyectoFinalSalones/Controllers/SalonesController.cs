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
        private DS2_ProyectoFinalSalonesDBEntities2 db = new DS2_ProyectoFinalSalonesDBEntities2();

        // GET: Salones
        public ActionResult Index()
        {
            var salones = db.Salones.Include(s => s.Cliente).Include(s => s.Propietario);
            return View(salones.ToList());
        }

        public ActionResult Lista()
        {
            var salones = db.Salones.Include(s => s.Cliente).Include(s => s.Propietario);
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
        public ActionResult DetallesRentar(string id)
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
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre");
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre");
            return View();
        }

        // POST: Salones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,Superficie,Direccion,Precio,Disponibilidad,Descripcion,Imagen,Propietario_Id,InicioAlquilerActual,FinAlquilerActual,Cliente_Id")] Salone salone, HttpPostedFileBase file)
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

            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", salone.Cliente_Id);
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
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", salone.Cliente_Id);
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre", salone.Propietario_Id);

            return View(salone);
        }

        public ActionResult Rentar(string id)
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
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", salone.Cliente_Id);
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre", salone.Propietario_Id);
            return View(salone);
        }


        // POST: Salones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Superficie,Direccion,Precio,Disponibilidad,Descripcion,Imagen,Propietario_Id,InicioAlquilerActual,FinAlquilerActual,Cliente_Id")] Salone salone, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string imagenUrl = System.IO.Path.GetFileName(file.FileName);
                    string pathUrl = System.IO.Path.Combine(Server.MapPath("/Public/Icons"), imagenUrl);
                    file.SaveAs(pathUrl);

                    salone.Imagen = imagenUrl;
                }
                
                db.Entry(salone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", salone.Cliente_Id);
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre", salone.Propietario_Id);
            return View(salone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Rentar([Bind(Include = "Id,Nombre,Superficie,Direccion,Precio,Disponibilidad,Descripcion,Imagen,Propietario_Id,InicioAlquilerActual,FinAlquilerActual,Cliente_Id")] Salone salone, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                
                {
                    if (file != null)
                    {
                        string imagenUrl = System.IO.Path.GetFileName(file.FileName);
                        string pathUrl = System.IO.Path.Combine(Server.MapPath("/Public/Icons"), imagenUrl);
                        file.SaveAs(pathUrl);

                        salone.Imagen = imagenUrl;
                    }

                    db.Entry(salone).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Lista");
                }
               
                    return View(salone);
                
               
            }

            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre", salone.Cliente_Id);
            ViewBag.Propietario_Id = new SelectList(db.Propietarios, "Id", "Nombre", salone.Propietario_Id);
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
