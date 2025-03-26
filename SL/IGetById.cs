using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGetById" in both code and config file together.
    [ServiceContract]
    public interface IGetById
    {
        [OperationContract]
        [ServiceKnownType(typeof(ML.Usuario))]
        SL.Result UsuarioGetById(int IdUsuario);
    }
}
