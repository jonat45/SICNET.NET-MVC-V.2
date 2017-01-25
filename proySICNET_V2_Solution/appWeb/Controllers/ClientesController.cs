﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using entityModels;
using bussinesModel;


namespace appWeb.Controllers
{
    public class ClientesController : Controller
    {

        #region "DECLARACIONES"

        clienteBussines obj_cliente      = new clienteBussines();
        utilitariosBussines obj_utilitarios  = new utilitariosBussines();

        #endregion


        #region "PROCEDIMIENTOS"


        //Lista de departamentos
        private void getDepartamento()
        {
            var departamentos = new SelectList(obj_utilitarios.FPub_ListaDepartamentos(), "Id_departamento", "Departamento_descripcion");
            ViewBag.departamentos = departamentos;
        }


        //Lista de Provincias (Json)
        public JsonResult getProvincia(int id)
        {
            var provincias = obj_utilitarios.FPub_ListaProvincia(id);
            return this.Json(provincias, JsonRequestBehavior.AllowGet);
        }


        //Lista de Distritos (Json)
        public JsonResult getDistrito(int id)
        {
            var distritos = obj_utilitarios.FPub_ListaDistritos(id);
            return this.Json(distritos, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //Pagina principal Clientes
        public ActionResult Clientes()
        {
            var res = from c in obj_cliente.FPub_Listar_Clientes(string.Empty,string.Empty) select c;

            return View(res.ToList());
        }

        [HttpPost]
        public ActionResult Clientes(string v_CODIGO,string v_CLIENTE)
        {

            var res = from c in obj_cliente.FPub_Listar_Clientes(v_CODIGO, v_CLIENTE) select c;

            return View(res.ToList());
        }

        [ChildActionOnly]
        public ActionResult callPartialView(int v_Modulo,string v_Id)
        {
            getDepartamento();

            

            if (v_Modulo == 1)
            {
                return PartialView("_AgregarCliente", new Cliente());

            }
            /*
            else if(v_Modulo == 2 && v_Id != null)
            {
                var clienteDetalle = obj_cliente.FPub_DetalleCliente(v_Id);
                return PartialView("_DetalleCliente", clienteDetalle);
            }
            */

            return PartialView();
        }


        public ActionResult _AgregarCliente()
        {
            getDepartamento();
            return PartialView(new Cliente());
        }

        public void validacion()
        {
            return;
        }

        [HttpPost]
        public ActionResult _AgregarCliente(Cliente cliente)
        {

            if (!ModelState.IsValid)
            {
                getDepartamento();
                return View(cliente);
            }
           

            try
            {
                int res = obj_cliente.FPub_MantenimientoCliente("1", cliente);
                return RedirectToAction("Clientes");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("error",ex);
            }

            return View();
        }


        public ActionResult _DetalleCliente(string id)
        {
            getDepartamento();
            var cliente = obj_cliente.FPub_DetalleCliente(id);

            return PartialView(cliente);
        }


        public ActionResult _EditarCliente(string id)
        {
            getDepartamento();
            var cliente = obj_cliente.FPub_DetalleCliente(id);

            return PartialView(cliente);
        }

        public ActionResult _EliminarCliente(string id)
        {
            getDepartamento();
            var cliente = obj_cliente.FPub_DetalleCliente(id);

            return PartialView(cliente);
        }
















        public ActionResult _partial()
        {
            getDepartamento();
            return View();
        }
        
        public ActionResult _Cliente_detalle()
        {
            getDepartamento();

            Cliente cliente = new Cliente();

            return PartialView("_Cliente_agregar");
        }

        // GET: Clientes/Create
        public ActionResult Cliente_agregar(int? codigo)
        {
            getDepartamento();

           

            if (codigo != null)
            {
                var cliente = obj_cliente.FPub_DetalleCliente(codigo.ToString());

                return PartialView(cliente);
            }                      
            
          

            return PartialView();
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
