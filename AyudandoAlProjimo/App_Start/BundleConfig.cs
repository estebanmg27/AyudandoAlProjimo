using System.Web;
using System.Web.Optimization;

namespace AyudandoAlProjimo
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include(
                        "~/Content/vendor/bootstrap/css/bootstrap.min.css"));


            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/Content/js")
                .Include(
                    "~/Content/vendor/jquery/jquery.min.js",
                    "~/Content//bootstrap/js/bootstrap.bundle.min.js"));
        }
    }
}