using System.IO;
using System.Web.Optimization;
using dotless.Core;
using dotless.Core.configuration;

namespace MPC.WebBase
{
    /// <summary>
    /// Transforms Less to Css
    /// </summary>
    public class LessTransform : IBundleTransform
    {
        #region private
        private readonly string directoryPath;
        #endregion
        #region Public
        /// <summary>
        /// Constructor
        /// </summary>        
        public LessTransform(string directoryPath)
        {
            this.directoryPath = directoryPath;
        }
        /// <summary>
        /// Transform from less to css
        /// </summary>
        public void Process(BundleContext context, BundleResponse response)
        {
            Directory.SetCurrentDirectory(directoryPath);
// ReSharper disable SuggestUseVarKeywordEvident
            DotlessConfiguration configuration = new DotlessConfiguration { CacheEnabled = true };
// ReSharper restore SuggestUseVarKeywordEvident
            response.Content = Less.Parse(response.Content, configuration);
            response.ContentType = "text/css";
        }

        #endregion

    }
}
