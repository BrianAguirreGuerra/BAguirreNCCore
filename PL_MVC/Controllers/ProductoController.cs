using Microsoft.AspNetCore.Mvc;
using BL; 
using ML;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult GetAll()
        {
            Result result = Producto.ObtenerProductos();

            if (result.Correct)
            {
                ViewBag.Productos = result.Objects; 
            }
            else
            {            
                ViewBag.ErrorMessage = result.ErrorMessage;
            }

            return View();
        }

        public IActionResult Eliminar(int IdProducto)
        {
            ML.Result result = BL.Producto.EliminarProducto(IdProducto);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido eliminar. Error: " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }

        public IActionResult Form(int? IdProducto)
        {
            DL.Producto producto;

            if (IdProducto == null)
            {
                producto = new DL.Producto();
            }
            else
            {
                ML.Result result = BL.Producto.ObtenerProductoPorId(IdProducto.Value);
                if (result.Correct)
                {
                    producto = (DL.Producto)result.Object;
                }
                else
                {
                    ViewBag.ErrorMessage = "No se encontró el producto con el ID especificado.";
                    producto = new DL.Producto();
                }
            }
            ML.Result proveedoresResult = BL.Proveedor.ObtenerProveedores();
            if (proveedoresResult.Correct)
            {
                List<DL.Proveedor> proveedores = ((List<object>)proveedoresResult.Objects).Cast<DL.Proveedor>().ToList();
                ViewBag.Proveedores = proveedores;
            }

            ML.Result departamentosResult = BL.Departamento.ObtenerDepartamentos();
            if (departamentosResult.Correct)
            {
                List<DL.Departamento> departamentos = ((List<object>)departamentosResult.Objects).Cast<DL.Departamento>().ToList();
                ViewBag.Departamentos = departamentos;
            }

            return View(producto);
        }



        [HttpPost]
        public IActionResult Form(DL.Producto model)
        {
                if (model.IdProducto == 0)
                {
                    ML.Result result = BL.Producto.Add(model);
                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Se ha agregado un nuevo producto correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha podido agregar el producto. Error: " + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
                else 
                {
                    ML.Result result = BL.Producto.ActualizarProducto(model);
                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Se ha actualizado el producto correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha podido actualizar el producto. Error: " + result.ErrorMessage;
                    return PartialView("Modal");
                    }
                } 
        }
    }
}
