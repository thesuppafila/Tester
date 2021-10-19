using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Tester.Model;
using Tester.ViewModel;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.IO;

namespace TicketGenerator.ViewModel
{
    class TicketBuilderViewModel : NotifyPropertyChanged
    {
        private XpsDocument _mainDocument;
        public XpsDocument MainDocument
        {
            get => _mainDocument;
            set
            {
                _mainDocument = value;
                OnPropertyChanged("MainDocument");
            }
        }

        private DocumentViewer _documentViewer1;
        public DocumentViewer DocumentViewer1
        {
            get
            {
                return _documentViewer1; }
            set
            {
                _documentViewer1 = value;
                OnPropertyChanged("DocumentViewer1");
            }
        }

     

        RelayCommand PrintTicket;

        public TicketBuilderViewModel()
        {
            /*_mainDocument = new XpsDocument("ticket.xps", FileAccess.ReadWrite);
            XpsDocumentWriter xpsdw = XpsDocument.CreateXpsDocumentWriter(_mainDocument);
            var page = new FixedPage();
            page.Children.Add(new TextBlock() { Text = "Задание 1." });
            xpsdw.Write(page);
            _documentViewer1 = new DocumentViewer();
            _documentViewer1.Document = MainDocument.GetFixedDocumentSequence();

            PrintTicket = new RelayCommand(obj => {

                /*var paragraphs = new List<Paragraph>();
                //Add Sample

                paragraphs.Add(new Paragraph(new Run(string.Format("Вычислить значение t, используя тип операндов byte и операции языка C#. Ответ представить в двоичном виде со всеми ведущими нулями."))));
                for (int i = 0; i < Count; i++)
                {
                    try
                    {
                        var exp = new Expression(Pattern, random.Next(RankMin, RankMax + 1));
                        Expressions.Add(exp);
                        paragraphs.Add(new Paragraph(new Run(string.Format("Решить: {0}, количество разрядов {1}", exp.ToString(), exp.CountOfRanks))));
                    }
                    catch
                    {

                    }
                }

                Section mySection = new Section();
                mySection.Blocks.AddRange(paragraphs);
                var _myFlowDocument = new FlowDocument();
                _myFlowDocument.Blocks.Add(mySection);

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    FlowDocument doc = _myFlowDocument as FlowDocument;

                    doc.PagePadding = new Thickness(96 * 0.25, 96 * 0.75, 96 * 0.25, 96 * 0.25);

                    HeaderedFlowDocumentPaginator paginator =
                        new HeaderedFlowDocumentPaginator(doc);

                    printDialog.PrintDocument(paginator, "Headered Flow Document");
                }*/
            //});
        }
    }
}
