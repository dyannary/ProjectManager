using System.Web.Optimization;

namespace ProjectManager.Presentation
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.5.1.min.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/lib/jquery-datatables").Include(
             "~/lib/jquery-datatables/css/jquery.dataTables.css",
            "~/lib/jquery-contextmenu/dist/jquery.contextMenu.min.css"
            ));
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
            "~/lib/jquery-datatables/js/jquery.dataTables.js",
            "~/lib/jquery-contextmenu/dist/jquery.contextMenu.min.js",
            "~/lib/jquery-contextmenu/dist/jquery.ui.position.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
            "~/Scripts/jquery.signalR-2.4.3.min.js",
            "~/signalr/hubs"
            ));

            bundles.Add(new Bundle("~/bundles/CustomJs").Include(
                      "~/Scripts/user/OpenModal.js",
                      "~/Scripts/Layout/LayoutScripts.js",
                      "~/Scripts/User/UserProjects.js",
                      "~/Scripts/Admin/UserManagement.js",
                      "~/Scripts/ProjectTask/UserProject.js",
                      "~/Scripts/User/Notification.js"));

            bundles.Add(new Bundle("~/bundles/Popper").Include(
                "~/Scripts/umd/popper.min.js"
                ));

            bundles.Add(new Bundle("~/bundles/NotificationLayout").Include(
                    "~/Scripts/Layout/SideLayoutScrips.js"));
        }
    }
}
