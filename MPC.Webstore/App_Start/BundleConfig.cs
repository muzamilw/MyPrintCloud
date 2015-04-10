using System.Web;
using System.Web.Optimization;

namespace MPC.Webstore
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.js",
                        "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Slider").Include(
                       "~/Scripts/js-image-slider.js",
                       "~/Scripts/SmartFormJCarousel.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/SliderDetail").Include(
                     "~/Scripts/js-image-slider-detail.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/input.watermark.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                     "~/Scripts/toastr.js",
                     "~/Scripts/toastr.min.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/PopUps").Include(
                   "~/Scripts/PopUp.js"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/jquery.rating").Include(
               "~/Scripts/jquery.rating.js"
               ));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datepicker.js", "~/LightBox/Js/jquery-1.10.1.min.js","~/LightBox/Js/jquery.fancybox.pack.js","~/LightBox/Js/jquery.fancybox.js"));

            bundles.Add(new ScriptBundle("~/bundles/modalEffects").Include(
                     "~/Scripts/modalEffects.js"));


            bundles.Add(new StyleBundle("~/Content/CSS").Include(
                      "~/Content/bootstrap.min.css"

                      ));

            bundles.Add(new StyleBundle("~/Content/jquery.rating").Include(
                     "~/Content/jquery.rating.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/js-image-slider-detail").Include(
                    "~/Content/js-image-slider-detail.css"
                    ));
            bundles.Add(new StyleBundle("~/Content/DesignerCss").Include(
                      "~/Content/Designer/DSv2.css",
                        "~/Content/Designer/jquery-sunny/jquery-ui-1.10.4.custom.min.css",
                        "~/Content/Designer/a66.css",
                        "~/Content/Designer/p103.css",
                         "~/Content/Designer/jquery.cropbox.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/designerPreloadScripts").Include(
                  "~/Scripts/Designer/p55.js",
                  "~/Scripts/Designer/p55-ease.js",
                  "~/Scripts/Designer/p40.js",
                  "~/Scripts/Designer/pvcc1.js",
                  "~/Scripts/Designer/pcf01-v2.js",
                  "~/Scripts/Designer/p64test.js",
                  "~/Scripts/Designer/p21-v2.js",
                  "~/Scripts/Designer/p15-v2.js"
                  ));
              bundles.Add(new ScriptBundle("~/bundles/designerScripts").Include(
                  "~/Scripts/Designer/p71.js",
                  "~/Scripts/Designer/p19.js",
                  "~/Scripts/Designer/XMLWriter.js",
                  "~/Scripts/Designer/pc1.js",
                  "~/Scripts/Designer/pc2.js",
                  "~/Scripts/Designer/pc3.js",
                  "~/Scripts/Designer/aj9.js",
                  "~/Scripts/Designer/aj21-v2.js",
                  "~/Scripts/Designer/aj12.js",
                  "~/Scripts/Designer/aj1.js",
                  "~/Scripts/Designer/p101.js",
                  "~/Scripts/Designer/p1.js",
                  "~/Scripts/Designer/p10-v2.js",
                  "~/Scripts/Designer/p35-v2.js",
                  "~/Scripts/Designer/pcf01-35.js"
                  ));
            
            bundles.Add(new StyleBundle("~/Content/LightBoxcss").Include(
                    "~/LightBox/Css/jquery.fancybox.css"
                    ));
              //bundles.Add(new ScriptBundle("~/bundles/LightBox").Include("~/LightBox1/Js/jquery.js", "~/LightBox1/Js/jquery.lightbox-0.5.js", "~/LightBox1/Js/jquery.lightbox-0.5.min.js", "~/LightBox1/Js/jquery.lightbox-0.5.pack.js", "~/LightBox1/Js/viewport.js"));

            //bundles.Add(new ScriptBundle("~/bundles/LightBox").Include("~/LightBox/Js/jquery-1.10.1.min.js", "~/LightBox/Js/jquery.fancybox.js", "~/LightBox/Js/jquery.fancybox-thumbs.js", "~/LightBox1/Js/jquery.fancybox-media.js", "~/LightBox1/Js/jquery.fancybox-buttons.js"));
            // Set EnableOptimizations to false for debugging. For more information,

            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
