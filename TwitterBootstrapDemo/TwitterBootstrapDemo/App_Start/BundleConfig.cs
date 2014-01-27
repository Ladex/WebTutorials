using System.Web;
using System.Web.Optimization;

namespace TwitterBootstrapDemo
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                            "~/Scripts/knockout-2.1.0.js",
                            "~/Scripts/TwitterBoostrapDemo.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-3.0.1/css/bootstrap.css",
                    "~/Content/bootstrap-3.0.1/css/bootstrap-theme.css",
                    "~/Content/site.css"));
        }
    }
}