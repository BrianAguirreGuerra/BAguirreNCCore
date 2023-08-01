using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result ObtenerProveedores()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
                {
                    var proveedores = context.Proveedors.ToList();
                    var proveedoresObjeto = proveedores.Cast<object>().ToList();

                    result.Correct = true;
                    result.Objects = proveedoresObjeto;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un problema: " + ex.Message;
            }
            return result;
        }
    }
}
