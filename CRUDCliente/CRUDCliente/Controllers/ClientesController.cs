using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUDCliente.Models;

namespace CRUDCliente.Controllers
{
    public class ClientesController : Controller
    {
        private AdmonClientesEntities db = new AdmonClientesEntities();
        
        public ActionResult Index1()
        {
            return View(new List<Clientes>());
        }
        [HttpPost]
        public ActionResult Index1(HttpPostedFileBase postedFile)
        {
            List<Clientes> customers = new List<Clientes>();
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        customers.Add(new Clientes
                        {
                            NumeroIdentificacion = Convert.ToInt32(row.Split(',')[0]),
                            Nombres = row.Split(',')[1],
                            IdTipoIdentificacion = Convert.ToInt32(row.Split(',')[2]),
                            Correo = row.Split(',')[3],
                            Celular = row.Split(',')[4],
                            Direccion = row.Split(',')[5],
                            DireccionDeInstalacion = row.Split(',')[6],
                            Activo = Convert.ToBoolean(row.Split(',')[6]),
                        });
                    }
                }
            }
            return View(customers);
        }
        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = db.Clientes.Include(c => c.TipoIdentificacion);
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.IdTipoIdentificacion = new SelectList(db.TipoIdentificacion, "IdTipoIdentificacion", "TipoIdentificacion1");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumeroIdentificacion,Nombres,IdTipoIdentificacion,Correo,Celular,Direccion,DireccionDeInstalacion,Activo")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTipoIdentificacion = new SelectList(db.TipoIdentificacion, "IdTipoIdentificacion", "TipoIdentificacion1", clientes.IdTipoIdentificacion);
            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTipoIdentificacion = new SelectList(db.TipoIdentificacion, "IdTipoIdentificacion", "TipoIdentificacion1", clientes.IdTipoIdentificacion);
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NumeroIdentificacion,Nombres,IdTipoIdentificacion,Correo,Celular,Direccion,DireccionDeInstalacion,Activo")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTipoIdentificacion = new SelectList(db.TipoIdentificacion, "IdTipoIdentificacion", "TipoIdentificacion1", clientes.IdTipoIdentificacion);
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            db.Clientes.Remove(clientes);
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
