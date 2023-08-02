using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static ML.Result ObtenerAreas()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.BaguirreNcapasNetcoreContext context = new DL.BaguirreNcapasNetcoreContext())
                {
                    var areas = context.Areas.ToList();
                    var areasObjeto = areas.Cast<object>().ToList();

                    result.Correct = true;
                    result.Objects = areasObjeto;
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
