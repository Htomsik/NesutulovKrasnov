using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika.Stores
{
    public static class MessageBus
    {
        public static event Action<object> Bus;

        public static void Send(object data)
            => Bus?.Invoke(data);
    }
}
