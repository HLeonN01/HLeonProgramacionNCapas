using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserUpdate" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserUpdate.svc or UserUpdate.svc.cs at the Solution Explorer and start debugging.
    public class UserUpdate : IUserUpdate
    {
        public SL.Result UsuarioUpdate(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UsuarioDireccionUpdate(usuario);
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
