using CRUDCliente.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDCliente.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            return View(new List<Clientes>());
            //return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
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
                            NumeroIdentificacion = Convert.ToInt32(row.Split(';')[0]),
                            Nombres = row.Split(';')[1],
                            IdTipoIdentificacion = Convert.ToInt32(row.Split(';')[2]),
                            Correo = row.Split(';')[3],
                            Celular = row.Split(';')[4],
                            Direccion = row.Split(';')[5],
                            DireccionDeInstalacion = row.Split(';')[6],
                            Activo = Convert.ToBoolean(row.Split(';')[7]),
                        });
                    }
                }
            }
            return View(customers);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Página descriptiva de la aplicación.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Página de Contacto.";

            return View();
        }
    }
}