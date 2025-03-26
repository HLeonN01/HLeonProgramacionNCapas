using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetAll" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GetAll.svc or GetAll.svc.cs at the Solution Explorer and start debugging.
    public class GetAll : IGetAll
    {
        public SL.Result UsuarioGetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            return new SL.Result 
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects
            };
        }
    }
}
