using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: NeutralResourcesLanguage("en-US")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat_Medium")] //+ 500
[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat_Regular")] //+ 400
[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat_SemiBold")] //+ 600
[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "Montserrat_Bold")]//+ 700