using System.Web;
using System.Web.Optimization;

namespace RandImportGenerator.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/modernizr-*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/vue.js",
                        "~/Scripts/blockUI.js",
                        "~/Scripts/lodash.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/app/global.js",
                        "~/Scripts/app/models.js",
                        "~/Scripts/app/utility.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/home-index").Include(
                        "~/Scripts/app/apps/home/column-list.js",
                        "~/Scripts/app/apps/home/columns/auto-incremented-column.js",
                        "~/Scripts/app/apps/home/columns/static-column.js",
                        "~/Scripts/app/apps/home/columns/randomized-column.js",
                        "~/Scripts/app/apps/home/columns/dependent-column.js",
                        "~/Scripts/app/apps/home/index.js"));

        }
    }
}
