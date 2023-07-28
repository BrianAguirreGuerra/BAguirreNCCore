using System;
using System.IO;
using System.Net;
using BL;

namespace ML
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Registrar un nuevo producto");
                Console.WriteLine("2. Buscar un producto por ID");
                Console.WriteLine("3. Actualizar un producto por ID");
                Console.WriteLine("4. Eliminar un producto por ID");
                Console.WriteLine("5. Obtener todos los productos");
                Console.WriteLine("0. Salir");

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarProducto();
                        break;
                    case 2:
                        BuscarProducto();
                        break;
                    case 3:
                        ActualizarProducto();
                        break;
                    case 4:
                        EliminarProducto();
                        break;
                    case 5:
                        ObtenerProductos();
                        break;
                    case 0:
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void RegistrarProducto()
        {
            Console.Write("Ingrese el nombre del producto: ");
            string nombreProducto = Console.ReadLine();

            Console.Write("Ingrese el precio unitario: ");
            decimal precioUnitario = decimal.Parse(Console.ReadLine());

            Console.Write("Ingrese el stock: ");
            int stock = int.Parse(Console.ReadLine());

            Console.Write("Ingrese el ID del proveedor: ");
            int idProveedor = int.Parse(Console.ReadLine());

            Console.Write("Ingrese el ID del departamento: ");
            int idDepartamento = int.Parse(Console.ReadLine());

            Console.Write("Ingrese la descripción del producto: ");
            string descripcion = Console.ReadLine();

            var nuevoProducto = new DL.Producto
            {
                Nombre = nombreProducto,
                PrecioUnitario = precioUnitario,
                Stock = stock,
                IdProveedor = idProveedor,
                IdDepartamento = idDepartamento,
                Descripcion = descripcion,
            };

            ML.Result resultado = Producto.Add(nuevoProducto);

            if (resultado.Correct)
            {
                Console.WriteLine("El producto se ha insertado correctamente.");
            }
            else
            {
                Console.WriteLine("Error al insertar el producto: " + resultado.ErrorMessage);
            }
        }

        static void BuscarProducto()
        {
            Console.Write("Ingrese el Id del producto a buscar: ");
            int idProducto = int.Parse(Console.ReadLine());

            ML.Result resultado = Producto.ObtenerProductoPorId(idProducto);

            if (resultado.Correct)
            {
                DL.Producto producto = (DL.Producto)resultado.Object;
                if (producto != null)
                {
                    Console.WriteLine("Producto encontrado:");
                    Console.WriteLine($"Id: {producto.IdProducto}");
                    Console.WriteLine($"Nombre: {producto.Nombre}");
                    Console.WriteLine($"Precio Unitario: {producto.PrecioUnitario}");
                    Console.WriteLine($"Stock: {producto.Stock}");
                    Console.WriteLine($"IdProveedor: {producto.IdProveedor}");
                    Console.WriteLine($"IdDepartamento: {producto.IdDepartamento}");
                    Console.WriteLine($"Descripción: {producto.Descripcion}");
                }
                else
                {
                    Console.WriteLine("El objeto almacenado en el resultado no es del tipo Producto.");
                }
            }
            else
            {
                Console.WriteLine("Error al buscar el producto: " + resultado.ErrorMessage);
            }
        }

        static void ActualizarProducto()
        {
            Console.Write("Ingrese el ID del producto a actualizar: ");
            int idProducto = int.Parse(Console.ReadLine());

            ML.Result resultadoBusqueda = Producto.ObtenerProductoPorId(idProducto);

            if (resultadoBusqueda.Correct)
            {
                DL.Producto producto = (DL.Producto)resultadoBusqueda.Object;
                if (producto != null)
                {
                    Console.WriteLine("Información actual del producto:");
                    MostrarInformacionProducto(producto);

                    Console.WriteLine();
                    Console.WriteLine("Ingrese los nuevos datos para el producto:");

                    Console.Write("Nombre: ");
                    producto.Nombre = Console.ReadLine();

                    Console.Write("Precio Unitario: ");
                    producto.PrecioUnitario = decimal.Parse(Console.ReadLine());

                    Console.Write("Stock: ");
                    producto.Stock = int.Parse(Console.ReadLine());

                    Console.Write("ID del Proveedor: ");
                    producto.IdProveedor = int.Parse(Console.ReadLine());

                    Console.Write("ID del Departamento: ");
                    producto.IdDepartamento = int.Parse(Console.ReadLine());

                    Console.Write("Descripción: ");
                    producto.Descripcion = Console.ReadLine();

                    ML.Result resultadoActualizacion = Producto.ActualizarProducto(producto);

                    if (resultadoActualizacion.Correct)
                    {
                        Console.WriteLine("El producto se ha actualizado correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("Error al actualizar el producto: " + resultadoActualizacion.ErrorMessage);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró el producto con el ID especificado.");
                }
            }
            else
            {
                Console.WriteLine("Error al buscar el producto: " + resultadoBusqueda.ErrorMessage);
            }
        }

        static void EliminarProducto()
        {
            Console.Write("Ingrese el ID del producto a eliminar: ");
            int idProducto = int.Parse(Console.ReadLine());

            ML.Result resultadoEliminacion = Producto.EliminarProducto(idProducto);

            if (resultadoEliminacion.Correct)
            {
                Console.WriteLine("El producto se ha eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Error al eliminar el producto: " + resultadoEliminacion.ErrorMessage);
            }
        }

        static void ObtenerProductos()
        {
            ML.Result resultado = Producto.ObtenerProductos();

            if (resultado.Correct)
            {
                List<DL.Producto> productos = new List<DL.Producto>();
                foreach (var producto in resultado.Objects)
                {
                    if (producto is DL.Producto prod)
                    {
                        productos.Add(prod);
                    }
                }

                if (productos.Count > 0)
                {
                    Console.WriteLine("Lista de productos:");

                    foreach (var producto in productos)
                    {
                        MostrarInformacionProducto(producto);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron productos.");
                }
            }
            else
            {
                Console.WriteLine("Error al obtener los productos: " + resultado.ErrorMessage);
            }
        }

        static void MostrarInformacionProducto(DL.Producto producto)
        {
            Console.WriteLine($"ID: {producto.IdProducto}");
            Console.WriteLine($"Nombre: {producto.Nombre}");
            Console.WriteLine($"Precio Unitario: {producto.PrecioUnitario}");
            Console.WriteLine($"Stock: {producto.Stock}");
            Console.WriteLine($"ID Proveedor: {producto.IdProveedor}");
            Console.WriteLine($"ID Departamento: {producto.IdDepartamento}");
            Console.WriteLine($"Descripción: {producto.Descripcion}");
            Console.WriteLine();
        }


    }
}
