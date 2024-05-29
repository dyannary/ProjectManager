using ProjectManager.Infrastructure.Persistance;
using System.Web;
using System.Web.Optimization;

namespace ProjectManager.Presentation
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/lib/jquery-datatables").Include(
             "~/lib/jquery-datatables/css/jquery.dataTables.css"
            //"~/lib/jquery-contextmenu/dist/jquery.contextMenu.min.css"
            ));
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
            "~/lib/jquery-datatables/js/jquery.dataTables.js"
            //"~/lib/jquery-contextmenu/dist/jquery.contextMenu.min.js",
            //"~/lib/jquery-contextmenu/dist/jquery.ui.position.min.js"
            ));

            //bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
            //"~/lib/fontawesome-iconpicker/js/fontawesome-iconpicker.js"
            ////"~/lib/jquery-contextmenu/dist/jquery.contextMenu.min.js",
            ////"~/lib/jquery-contextmenu/dist/jquery.ui.position.min.js"
            //));
            //bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
            //"~/lib/fontawesome-iconpicker/css/fontawesome-iconpicker.css"
            ////"~/lib/jquery-contextmenu/dist/jquery.contextMenu.min.js",
            ////"~/lib/jquery-contextmenu/dist/jquery.ui.position.min.js"
            //));

            bundles.Add(new Bundle("~/bundles/OpenModal").Include(
                      "~/Scripts/user/OpenModal.js"));

            bundles.Add(new Bundle("~/bundles/LayoutScripts").Include(
                    "~/Scripts/Layout/LayoutScripts.js"));

            bundles.Add(new Bundle("~/bundles/UserProjects").Include(
                    "~/Scripts/User/UserProjects.js"));

            bundles.Add(new Bundle("~/bundles/Admin").Include(
                    "~/Scripts/Admin/UserManagement.js"));


            //bundles.Add(new ScriptBundle("~/bundles/toastr").Include("~/Scripts/toastr.js"));
            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/toastr.css"));


        }
    }
}
