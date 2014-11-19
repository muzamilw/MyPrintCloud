using System.Collections.Generic;
using System.Diagnostics;

namespace MPC.Interfaces.Logger
{
    /// <summary>
    /// Mpc Logger
    /// </summary>
    public interface IMPCLogger
    {
        void Write(object message, string category, int priority, int eventId, TraceEventType severity);

        void Write(object message, string category, int priority, int eventId, TraceEventType severity, string title);

        void Write(object message, string category, int priority, int eventId, TraceEventType severity, string title, IDictionary<string, object> properties);
    }
}
