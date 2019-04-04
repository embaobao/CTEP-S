using System.Web;
using System.Web.Optimization;

namespace CTEP
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/user/css").Include(
                     "~/Content/css/bootstrap.min.css",
                     "~/Content/css/ef.css",
                     "~/Content/css/jquery-ui.min.css",
                     "~/Content/css/normalize.css",
                     "~/Content/css/sidebar.css"));

            bundles.Add(new ScriptBundle("~/bundles/user/bootstrap").Include(
                     "~/Scripts/js/jquery.min.js",
                     "~/Scripts/js/snap.svg-min.js",
                     "~/Scripts/js/popper.min.js",
                     "~/Scripts/js/bootstrap.min.js"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/user/base").Include(
                 "~/Scripts/js/base64.js",
                  "~/Scripts/js/layer.js",
                    "~/Scripts/js/cookie.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/user/Pos").Include(
                "~/Scripts/js/pos.js",
                   "~/Scripts/js/main.js"
                   ));
            bundles.Add(new ScriptBundle("~/bundles/user/dater").Include(
                "~/Scripts/js/laydate.js"
                   ));
           
        }
    }
}
