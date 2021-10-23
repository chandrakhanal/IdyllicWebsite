using System.Web;
using System.Web.Optimization;

namespace IdyllicWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862

        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new Bundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js", "~/Scripts/js/popper.min.js"));

            bundles.Add(new Bundle("~/bundles/sitescript").Include(
                     "~/Scripts/js/vendor.js",
                     "~/Scripts/js/app.js"));

            bundles.Add(new StyleBundle("~/Content/bootcss").Include(
                      "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/maincss").Include(
                     "~/Content/css/vendor.css",
                     "~/Content/css/style.css"));

            bundles.Add(new StyleBundle("~/bundles/jquistyle").Include("~/Content/js/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Content/js/jquery-ui/jquery-ui.js"));

            ///////////////////------Admin Panel---------------///////////
            ///
            bundles.Add(new StyleBundle("~/Content/cmssec").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/Component/cmsup").Include("~/Component/cmsupify/uploadify.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/cmsup").Include("~/Component/cmsupify/jquery.uploadify.min.js"));


            bundles.Add(new StyleBundle("~/Admin/css").Include(
                  "~/Areas/Admin/Component/css/main.css"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                     "~/Areas/Admin/Component/Scripts/main.js"));


            //----Styles----//
            bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
                      "~/Content/DataTable/jQueryUI-1.12.1/themes/base/jquery-ui.min.css",
                      "~/Content/DataTable/datatables.min.css"));

            bundles.Add(new StyleBundle("~/bundles/datatables/ButtonCss").Include(
                      "~/Content/DataTable2/Buttons-1.6.1/css/buttons.dataTables.min.css",
                      "~/Content/DataTable2/Buttons-1.6.1/css/buttons.jqueryui.min.css"));

            bundles.Add(new StyleBundle("~/bundles/css/jstreecss").Include(
                      "~/Content/treeview/css/jstree/themes/default/style.css"));

            //----Scripts----//
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                     "~/Content/DataTable/datatables.min.js",
                     "~/Content/DataTable/jQueryUI-1.12.1/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables/ButtonJs").Include(
                    "~/Content/DataTable2/Buttons-1.6.1/js/dataTables.buttons.min.js",
                     "~/Content/DataTable2/JSZip-2.5.0/jszip.min.js",
                     "~/Content/DataTable2/pdfmake-0.1.36/pdfmake.min.js",
                     "~/Content/DataTable2/pdfmake-0.1.36/vfs_fonts.js",
                     "~/Content/DataTable2/Buttons-1.6.1/js/buttons.colVis.min.js",
                     "~/Content/DataTable2/Buttons-1.6.1/js/buttons.html5.min.js",
                     "~/Content/DataTable2/Buttons-1.6.1/js/buttons.print.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jstree").Include(

                     "~/Content/treeview/js/jstree.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                     "~/Content/Custom/js/customScript.js",
                     "~/Content/custom/js/intlTelInput.js"));

            bundles.Add(new ScriptBundle("~/bundles/0").Include(
                     "~/Content/Custom/js/courseMemberScript.js"));

            #region Notification
            bundles.Add(new StyleBundle("~/bundles/toastr/css").Include(
                      "~/Content/Notification/toastr/toastr2.1.3.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr/js").Include(
                     "~/Content/Notification/toastr/toastr2.1.3.min.js",
                     "~/Content/Notification/toastr/toastrCustom.js"));
            #endregion

            bundles.Add(new ScriptBundle("~/bundles/daterange").Include(
                     "~/Content/plugins/pickers/daterange/moment.min.js",
                     "~/Content/plugins/pickers/daterange/daterangepicker.js"));
        }
    }
}
