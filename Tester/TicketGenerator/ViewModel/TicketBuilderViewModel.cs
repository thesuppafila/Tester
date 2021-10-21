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
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using Section = Microsoft.Office.Interop.Word.Section;
using System.Collections.ObjectModel;

namespace TicketGenerator.ViewModel
{
    class TicketBuilderViewModel : NotifyPropertyChanged
    {
        Random random;

        private int _countOfTickets;
        public int CountOfTickets
        {
            get
            {
                return _countOfTickets;
            }
            set
            {
                _countOfTickets = value;
                OnPropertyChanged("CountOfTickets");
            }
        }

        private string _groupNumber;
        public string GroupNumber
        {
            get
            {
                return _groupNumber;
            }
            set
            {
                _groupNumber = value;
                OnPropertyChanged("GroupNumber");
            }
        }

        private ObservableCollection<Expression> _expressions;
        public ObservableCollection<Expression> Expressions
        {
            get => _expressions;
            set
            {
                _expressions = value;
                OnPropertyChanged("Expressions");
            }
        }

        public RelayCommand PrintTicket { get; set; }

        public TicketBuilderViewModel()
        {
            random = new Random();
            Expressions = new ObservableCollection<Expression>();
            CountOfTickets = 30;
            PrintTicket = new RelayCommand(obj =>
            {
                // Определение переменной oWord
                Word._Application oWord = new Word.Application();
                // Считывает шаблон и сохраняет измененный в новом
                _Document mainDoc = oWord.Documents.Add();
                _Document keyDoc = oWord.Documents.Add();

                for (int i = 1; i < CountOfTickets + 1; i++)
                {
                    var expNumber = random.Next(0, Expressions.Count);
                    Microsoft.Office.Interop.Word.Paragraph para1 = mainDoc.Content.Paragraphs.Add();
                    para1.Range.Font.Name = "times new roman";
                    para1.Range.Font.Size = 16;
                    para1.Range.Text = "Вариант " + i.ToString();
                    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    para1.Range.InsertParagraphAfter();

                    AddExample(ref mainDoc);
                    AddTask(ref mainDoc, Expressions[expNumber]);
                    mainDoc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);

                    Microsoft.Office.Interop.Word.Paragraph para2 = keyDoc.Content.Paragraphs.Add();
                    para2.Range.Font.Name = "times new roman";
                    para2.Range.Font.Size = 14;
                    para2.Range.Text = "Вариант " + i.ToString();
                    para2.Range.InsertParagraphAfter();

                    Microsoft.Office.Interop.Word.Paragraph para3 = keyDoc.Content.Paragraphs.Add();
                    para3.Range.Font.Name = "times new roman";
                    para3.Range.Font.Size = 14;
                    para3.Range.Text = "Задание 2: " + Expressions[expNumber].Solve;
                    para3.Range.InsertParagraphAfter();
                }

                keyDoc.SaveAs(FileName: Environment.CurrentDirectory + "\\" + GroupNumber + "_keys.docx");
                keyDoc.Close();
                mainDoc.SaveAs(FileName: Environment.CurrentDirectory + "\\" + GroupNumber + "_tickets.docx");
                mainDoc.Close();
                oWord.Quit();
                MessageBox.Show("Файлы билетов сгенерированы в папке с программой.");
            });
        }

        private void AddExample(ref _Document document)
        {
            Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add();
            para1.Range.Font.Name = "times new roman";
            para1.Range.Font.Size = 16;
            para1.Range.Text = "Задание 1.";
            para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;
            para1.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add();
            para2.Range.Font.Name = "times new roman";
            para2.Range.Font.Size = 16;
            para2.Range.Text = "Вычислить значение t, используя тип операндов byte и операции языка C#. Ответ представить в виде десятичного числа.";
            para2.Range.InsertParagraphAfter();

            Word.Table taskTable = document.Tables.Add(para1.Range, 1, 1);
            taskTable.Borders.Enable = 1;
            taskTable.Range.Font.Name = "Consolas";
            taskTable.Range.Font.Size = 16;
            taskTable.Range.Text = "x = 8\ny = 2\nz = 1\nt = x | y << (z ^ x)";
        }

        private void AddTask(ref _Document document, Expression exp)
        {
            Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add();
            para1.Range.Font.Name = "times new roman";
            para1.Range.Font.Size = 16;
            para1.Range.Text = "\nЗадание 2.";
            para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;
            para1.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add();
            para2.Range.Font.Name = "times new roman";
            para2.Range.Font.Size = 16;
            para2.Range.Text = "Вычислить значение t, используя " + exp.CountOfRanks + "-разрядный тип операндов и операции языка C#. Ответ представить в виде десятичного числа.";
            para2.Range.InsertParagraphAfter();

            Word.Table taskTable = document.Tables.Add(para1.Range, 1, 1);
            taskTable.Borders.Enable = 1;
            taskTable.Range.Font.Name = "Consolas";
            taskTable.Range.Font.Size = 16;
            taskTable.Range.Text = exp.TaskText;
        }
    }
}
