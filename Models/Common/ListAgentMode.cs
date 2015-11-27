using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.Common
{
    public class ListAgentMode
    {
        public List<AgentsModel> objList { get; set; }
    }
    public class AgentsModel
    {
        public string agentName { get; set; }
        public string agentTel { get; set; }
        public string agentMobile { get; set; }
        public string agentEmail { get; set; }
    }
    
}