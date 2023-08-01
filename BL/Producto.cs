using Microsoft.EntityFrameworkCore;
using ML;
using DL;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(DL.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"InsertarProducto '{producto.Nombre}' , {producto.PrecioUnitario} , {producto.Stock} , '{producto.IdProveedor}', '{producto.IdDepartamento}', '{producto.Descripcion}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Se genero un error al insertar el producto";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; 
                result.ErrorMessage = "Ocurrio un problema" + ex.Message;
            }
            return result;
        }

        public static ML.Result ObtenerProductoPorId(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
                {
                    var idProductoParam = new SqlParameter("@IdProducto", idProducto);

                    var producto = context.Productos.FromSqlRaw("EXEC ObtenerProductoPorId @IdProducto", idProductoParam).AsEnumerable().FirstOrDefault();

                    if (producto != null)
                    {
                        result.Correct = true;
                        result.Object = producto;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró ningún producto con el ID especificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un problema: " + ex.Message;
            }

            return result;
        }


        //public static ML.Result ObtenerProductoPorId(int idProducto)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
        //        {
        //            var producto = context.Productos.Find(idProducto);
        //            if (producto != null)
        //            {
        //                result.Correct = true;
        //                result.Object = producto;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se encontró ningún producto con el ID especificado.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = "Ocurrió un problema: " + ex.Message;
        //    }
        //    return result;
        //}


        public static ML.Result ActualizarProducto(DL.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
                {
                    var idParam = new SqlParameter("@IdProducto", producto.IdProducto);
                    var nombreParam = new SqlParameter("@Nombre", producto.Nombre);
                    var precioParam = new SqlParameter("@PrecioUnitario", producto.PrecioUnitario);
                    var stockParam = new SqlParameter("@Stock", producto.Stock);
                    var proveedorParam = new SqlParameter("@IdProveedor", producto.IdProveedor);
                    var departamentoParam = new SqlParameter("@IdDepartamento", producto.IdDepartamento);
                    var descripcionParam = new SqlParameter("@Descripcion", producto.Descripcion);

                    context.Database.ExecuteSqlRaw("ActualizarProducto @IdProducto, @Nombre, @PrecioUnitario, @Stock, @IdProveedor, @IdDepartamento, @Descripcion",
                        idParam, nombreParam, precioParam, stockParam, proveedorParam, departamentoParam, descripcionParam);

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un problema: " + ex.Message;
            }
            return result;
        }

        //public static ML.Result ActualizarProducto(DL.Producto producto)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
        //        {
        //            var productoExistente = context.Productos.Find(producto.IdProducto);

        //            if (productoExistente != null)
        //            {
        //                productoExistente.Nombre = producto.Nombre;
        //                productoExistente.PrecioUnitario = producto.PrecioUnitario;
        //                productoExistente.Stock = producto.Stock;
        //                productoExistente.IdProveedor = producto.IdProveedor;
        //                productoExistente.IdDepartamento = producto.IdDepartamento;
        //                productoExistente.Descripcion = producto.Descripcion;

        //                context.SaveChanges();

        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se encontró el producto a actualizar.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = "Ocurrió un problema: " + ex.Message;
        //    }
        //    return result;
        //}

        public static ML.Result EliminarProducto(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
                {
                    var idParam = new SqlParameter("@IdProducto", idProducto);

                    int rowsAffected = context.Database.ExecuteSqlRaw("EliminarProducto @IdProducto", idParam);

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el producto con el ID especificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un problema: " + ex.Message;
            }
            return result;
        }


        //public static ML.Result EliminarProducto(int idProducto)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
        //        {
        //            var producto = context.Productos.Find(idProducto);

        //            if (producto != null)
        //            {
        //                context.Productos.Remove(producto);
        //                context.SaveChanges();

        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se encontró el producto a eliminar.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = "Ocurrió un problema: " + ex.Message;
        //    }
        //    return result;
        //}

        public static ML.Result ObtenerProductos()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
                {
                    var productos = context.Productos.FromSqlRaw("EXEC ObtenerProducto").ToList();

                    if (productos.Count > 0)
                    {
                        result.Correct = true;
                        result.Objects = productos.Cast<object>().ToList();
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron productos.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un problema: " + ex.Message;
            }
            return result;
        }

        //public static ML.Result ObtenerProductos()
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
        //        {
        //            var productos = context.Productos.ToList();
        //            var productosObjeto = productos.Cast<object>().ToList();

        //            result.Correct = true;
        //            result.Objects = productosObjeto;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = "Ocurrió un problema: " + ex.Message;
        //    }
        //    return result;
        //}
    }
}