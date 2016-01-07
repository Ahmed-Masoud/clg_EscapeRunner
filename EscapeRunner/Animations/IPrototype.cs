using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRunner.Animations
{
    /// <summary>
    /// Represents a generic prototype pattern interface
    /// </summary>
    interface IPrototype<T>
    {
        T Clone();
    }
}
