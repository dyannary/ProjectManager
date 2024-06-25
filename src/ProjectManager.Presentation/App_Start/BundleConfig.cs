using System.Web.Optimization;

namespace ProjectManager.Presentation
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

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

            bundles.Add(new Bundle("~/bundles/OpenModal").Include(
                      "~/Scripts/user/OpenModal.js"));

            bundles.Add(new Bundle("~/bundles/LayoutScripts").Include(
                    "~/Scripts/Layout/LayoutScripts.js"));

            bundles.Add(new Bundle("~/bundles/UserProjects").Include(
                    "~/Scripts/User/UserProjects.js"));
            
            bundles.Add(new Bundle("~/bundles/Admin").Include(
                    "~/Scripts/Admin/UserManagement.js"));

            bundles.Add(new Bundle("~/bundles/ProjectTask").Include(
                    "~/Scripts/ProjectTask/UserProject.js"));

            bundles.Add(new Bundle("~/bundles/Notification").Include(
                    "~/Scripts/User/Notification.js"));

            bundles.Add(new Bundle("~/bundles/NotificationLayout").Include(
                    "~/Scripts/Layout/SideLayoutScrips.js"));
        }
    }
}
