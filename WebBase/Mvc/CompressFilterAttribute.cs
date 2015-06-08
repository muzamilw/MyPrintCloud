using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;
namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Filter that does compression
    /// </summary>
    public class CompressFilterAttribute : ActionFilterAttribute
    {
        #region Public

        /// <summary>
        /// Implement the filter
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            HttpRequestMessage request = filterContext.Request;


            if (request.Headers.AcceptEncoding == null ||
                    request.Headers.AcceptEncoding.Count == 0)
            {
                base.OnActionExecuted(filterContext);
                return;
            }

            // Get Encoding
            string encodingType = request.Headers.AcceptEncoding.First().Value;
            encodingType = encodingType.ToUpperInvariant();

            // If Compression is not supported
            if (encodingType != "GZIP" && encodingType != "DEFLATE" || filterContext.Response == null)
            {
                base.OnActionExecuted(filterContext);
                return;
            }

            HttpContent response = filterContext.Response.Content;
            var bytes = response == null ? null : response.ReadAsByteArrayAsync().Result;

            if (encodingType.Contains("GZIP"))
            {
                var gzippedContent = bytes == null ? new byte[0] : CompressionHelper.GZipByte(bytes);
                filterContext.Response.Content = new ByteArrayContent(gzippedContent);
                filterContext.Response.Content.Headers.Add("Content-encoding", "gzip");
            }
            else if (encodingType.Contains("DEFLATE"))
            {
                var deflatedContent = bytes == null ? new byte[0] : CompressionHelper.DeflateByte(bytes);
                filterContext.Response.Content = new ByteArrayContent(deflatedContent);
                filterContext.Response.Content.Headers.Add("Content-encoding", "deflate");
            }

            base.OnActionExecuted(filterContext);
        }

        #endregion
    }
}
