using System.Web;
using System.Web.Optimization;

namespace AyudandoAlProjimo
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/bootstrap.css"                    
                        ));

            bundles.Add(new StyleBundle("~/Estilos/css")
                .Include(
                        "~/Estilos/formularios.css"
                        ));


            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/Content/js")
                .Include(
                    "~/Scripts/jquery-3.3.1.min.js",
                    "~/Scripts/bootstrap.bundle.min.js"));
        }
    }
}