using System.Text;
using ESC_POS_USB_NET.Enums;
using ESC_POS_USB_NET.Printer;

namespace NewsPrint {
    public class Program {
        public static void Main(string[] args){
            System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);
            Printer printer = new Printer("EPSON TM-T88III Receipt", "ISO-8859-1");

            NewsArticle na = new NewsArticle("https://www.tagesschau.de/xml/rss2");
            na.getArticles();

            printer.AlignCenter();
            printer.ExpandedMode("NACHRICHTEN");
            printer.Append(DateTime.Now.ToString("G"));
            printer.AlignLeft();

            for(int i = 0; i < 5; i++){
                printer.NewLine();
                printer.BoldMode(na.Articles[i].Title);
                printer.Append(na.Articles[i].Description);
                printer.NewLine();
                printer.AlignRight();
                printer.Font(na.Articles[i].PublishDate.ToString("G"), Fonts.FontB);
                printer.AlignLeft();
                printer.Font("########################################################", Fonts.FontB);
            }

            printer.FullPaperCut();
            printer.PrintDocument();
        }
    }
}