using System.Web;
using System.Web.Optimization;

namespace SMES.AspNetDTM.UI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css", 
                      "~/Content/font-awesome.css",
                      "~/Content/framework.css", 
                      "~/Content/jquery-confirm.css", 
                      "~/Content/datepicker.css",
                      "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/smesjs").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/framwork.js",
                "~/Scripts/datepicker.js",
                "~/Scripts/date.js",
                "~/Scripts/jquery.slimscroll.js",
                "~/Scripts/jqPaginator.js",
                "~/Scripts/jquery-confirm.js",
                "~/Scripts/bootstrap-datetimepicker.js",
                "~/Scripts/bootstrap-datetimepicker.zh-CN.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include("~/Scripts/highcharts.js",
                "~/Scripts/highcharts-more.js",
                "~/Scripts/highcharts-3d.js",
                "~/Scripts/solid-gauge.js",
                "~/Scripts/radialIndicator.js"));

            bundles.Add(new ScriptBundle("~/bundles/newchart").Include(
                "~/Scripts/JS/jquery-1.8.3.min.js",
                "~/Scripts/JS/highcharts.js",
                "~/Scripts/highcharts-more.js",
                "~/Scripts/highcharts-3d.js",
                "~/Scripts/solid-gauge.js",
                "~/Scripts/radialIndicator.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/tmpl").Include("~/Scripts/jquery.tmpl.js"));
        }
    }
}
