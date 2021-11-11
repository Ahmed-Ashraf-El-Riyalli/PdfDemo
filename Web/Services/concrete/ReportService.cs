using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using Web.Services.Interfaces;

namespace Web.Services.concrete
{
    public class ReportService : IReportService
    {
        private readonly IConverter _converter;


        public ReportService(IConverter converter)
        {
            _converter = converter;
        }


        public byte[] GeneratePdfReport()
        {
            string html = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
               <title>This is the header of this document.</title>
            </head>
                <body>
                    <h1>This is the heading for demonstration purposes only.</h1>
                    <p>This is a line of text for demonstration purposes only.</p>
                </body>
            </html>
            ";

            #region GlobalSettings
            GlobalSettings globalSettings = new GlobalSettings();

            globalSettings.DocumentTitle = "PDF Title";
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;

            globalSettings.Margins = new MarginSettings() { Top = 25, Bottom = 25 };
            #endregion

            #region WebSettings
            WebSettings webSettings = new WebSettings();

            webSettings.DefaultEncoding = "utf-8";
            webSettings.UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/css/pdfStyles.css");
            #endregion

            #region HeaderSettings
            HeaderSettings headerSettings = new HeaderSettings();

            headerSettings.FontSize = 15;
            headerSettings.FontName = "Ariel";
            headerSettings.Right = "Page [page] of [toPage]";
            headerSettings.Line = true;
            #endregion

            #region FooterSettings
            FooterSettings footerSettings = new FooterSettings();

            footerSettings.FontSize = 12;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "This is for demonstration purposes only.";
            footerSettings.Line = true;
            #endregion

            #region ObjectSettings
            ObjectSettings objectSettings = new ObjectSettings();

            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = html;
            //objectSettings.Page = "https://www.google.com/search?q=RxJS+icons&tbm=isch&ved=2ahUKEwjgs92UppD0AhWOMhQKHdF1CuMQ2-cCegQIABAA&oq=RxJS+icons&gs_lcp=CgNpbWcQAzIHCCMQ7wMQJzoFCAAQgAQ6BggAEAcQHlDWCVi3EWCpFGgAcAB4AIABcIgBmQSSAQMwLjWYAQCgAQGqAQtnd3Mtd2l6LWltZ8ABAQ&sclient=img&ei=1QqNYaD1Oo7lUNHrqZgO&bih=754&biw=1536";
            objectSettings.WebSettings = webSettings;
            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            #endregion

            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return _converter.Convert(htmlToPdfDocument);
        }
    }
}
