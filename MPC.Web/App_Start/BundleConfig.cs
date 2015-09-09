using MPC.WebBase;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace MPC.MIS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            /* ============================================================== */
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                         "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            /* ============================================================== */

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            /* ============================================================== */
            bundles.Add(new ScriptBundle("~/Bundle/CentaurusLibs")
                .Include("~/Content/themes/Centaurus/js/demo-skin-changer.js")
                .Include("~/Content/themes/Centaurus/js/bootstrap.js")
                .Include("~/Content/themes/Centaurus/js/jquery.nanoscroller.min.js")
                .Include("~/Content/themes/Centaurus/js/demo.js")
                .Include("~/Content/themes/Centaurus/js/jquery.slimscroll.min.js")
                .Include("~/Content/themes/Centaurus/js/raphael-min.js")
                .Include("~/Content/themes/Centaurus/js/morris.min.js")
                .Include("~/Content/themes/Centaurus/js/daterangepicker.js")
                .Include("~/Content/themes/Centaurus/js/jquery-jvectormap-1.2.2.min.js")
                .Include("~/Content/themes/Centaurus/js/jquery-jvectormap-world-merc-en.js")
                .Include("~/Content/themes/Centaurus/js/gdp-data.js")
                .Include("~/Content/themes/Centaurus/js/flot/jquery.flot.js")
                .Include("~/Content/themes/Centaurus/js/flot/jquery.flot.min.js")
                .Include("~/Content/themes/Centaurus/js/flot/jquery.flot.pie.min.js")
                .Include("~/Content/themes/Centaurus/js/flot/jquery.flot.stack.min.js")
                .Include("~/Content/themes/Centaurus/js/flot/jquery.flot.resize.min.js")
                .Include("~/Content/themes/Centaurus/js/flot/jquery.flot.time.min")
                .Include("~/Content/themes/Centaurus/js/flot/jquery.flot.threshold.js")
                .Include("~/Content/themes/Centaurus/js/scripts.js")
                .Include("~/Content/themes/Centaurus/js/jquery.nestable.js")
                .Include("~/Content/themes/Centaurus/js/jquery.nouislider.js")
                .Include("~/Content/themes/Centaurus/js/jquerySlider.js")
                .Include("~/Content/themes/Centaurus/js/select2.min.js"));

            /* ============================================================== */

            bundles.Add(new ScriptBundle("~/Bundle/BaseLibs")
                .Include("~/Scripts/jquery-ui-1.10.4.js")
                .Include("~/Scripts/jquery-ui-timepicker-addon.js")
                .Include("~/Scripts/jquery.blockUI.js")
                .Include("~/Scripts/spectrum.js")
                .Include("~/Scripts/Ace/colorpicker.js")
                .Include("~/Scripts/json2.js")
                .Include("~/Scripts/knockout-3.1.0.js")
                .Include("~/Scripts/knockout.mapping-latest.js")
                .Include("~/Scripts/knockout.validation.js")
                .Include("~/Scripts/underscore.js")
                .Include("~/Scripts/underscore-ko-1.6.0.js")
                .Include("~/Scripts/moment.js")
                .Include("~/Scripts/toastr.js")
                .Include("~/Scripts/amplify.js")
                .Include("~/Scripts/knockout-sortable.js")
                .Include("~/Scripts/knockout-morris.js")
                .Include("~/RichTextEditor/ckeditor.js")
                .Include("~/Scripts/require.js")
                .Include("~/Scripts/respond.js")
                .Include("~/Scripts/App/architecture.js")
                .Include("~/Scripts/App/requireConfig.js")
                .Include("~/Scripts/Ace/lib/ace/ace.js")
                .Include("~/Scripts/autoNumeric/autoNumeric-1.9.25.min.js")
                .Include("~/Scripts/numeral/numeral.min.js")
                .Include("~/Scripts/knockout-ace.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                 "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Bundles/BaseCss")
                .Include("~/Content/CSS/toastr.css")
                .Include("~/Content/colorpicker.css")
                .Include("~/Content/CSS/spectrum.css")
                .Include("~/RichTextEditor/skins/moono/editor.css")
                .Include("~/Content/CSS/custom.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                   "~/Scripts/dropzone/dropzone.min.js"));

            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                     "~/Scripts/dropzone/css/basic.css",
                     "~/Scripts/dropzone/css/dropzone.css"));

            bundles.Add(new StyleBundle("~/Content/themes/Centaurus/css/bundleCentaurus/css")
                .Include("~/Content/themes/Centaurus/css/bootstrap/bootstrap.min.css")
                .Include("~/Content/themes/Centaurus/css/libs/font-awesome.css")
                .Include("~/Content/themes/Centaurus/css/libs/nanoscroller.css")
                .Include("~/Content/themes/Centaurus/css/libs/datepicker.css")
                .Include("~/Content/themes/Centaurus/css/compiled/layout.css")
                .Include("~/Content/themes/Centaurus/css/compiled/elements.css")
                .Include("~/Content/themes/Centaurus/css/libs/dropzone.css")
                .Include("~/Content/themes/Centaurus/css/libs/magnific-popup.css")
                .Include("~/Content/themes/Centaurus/css/compiled/calendar.css")
                .Include("~/Content/themes/Centaurus/css/libs/morris.css")
                .Include("~/Content/themes/Centaurus/css/libs/daterangepicker.css")
                .Include("~/Content/themes/Centaurus/css/libs/jquery-jvectormap-1.2.2.css")
                .Include("~/Content/themes/Centaurus/css/compiled/wizard.css")
                .Include("~/Content/themes/Centaurus/css/libs/bootstrap-wizard.css")
                .Include("~/Content/themes/Centaurus/css/libs/select2.css")
                .Include("~/Content/themes/Centaurus/css/libs/jquery.nouislider.css")
                .Include("~/Content/CSS/jquery-Slider.css"));

            Bundle lessBundle = new Bundle("~/Bundle/AppLess").Include("~/Content/less/MainSite.less");

            lessBundle.Transforms.Add(new LessTransform(Path.Combine(HttpRuntime.AppDomainAppPath, "Content\\less")));
            lessBundle.Transforms.Add(new CssMinify());

            bundles.Add(lessBundle);
        }
    }
}
