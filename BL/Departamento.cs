using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result ObtenerDepartamentos()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
                {
                    var departamentos = context.Departamentos.ToList();
                    var departamentosObjeto = departamentos.Cast<object>().ToList();

                    result.Correct = true;
                    result.Objects = departamentosObjeto;
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
