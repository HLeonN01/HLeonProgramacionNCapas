using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    public class UserAdd : IUserAdd
    {
        public SL.Result UsuarioAdd(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
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
