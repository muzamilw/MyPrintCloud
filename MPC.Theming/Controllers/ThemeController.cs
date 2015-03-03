using System.Collections.Generic;
using System.Web.Http;

namespace MPC.Theming.Controllers
{
    /// <summary>
    /// Theme Controller Api
    /// </summary>
    public class ThemeController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new [] { "value1", "value2" };
        }

    }
}