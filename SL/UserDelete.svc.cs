using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserDelete" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserDelete.svc or UserDelete.svc.cs at the Solution Explorer and start debugging.
    public class UserDelete : IUserDelete
    {
        public SL.Result UsuarioDelete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.UsuarioDireccionDelete(IdUsuario);
            return new SL.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                //Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects
            };
        }
    }
}
