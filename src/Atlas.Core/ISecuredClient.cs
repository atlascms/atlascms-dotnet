using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public interface ISecuredClient<T> where T : ClientBase
    {
        T SetMyToken(string token);
    }
}
