using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace TicketGenerator.Model
{
    [Serializable]
    public class Package
    {
        public ObservableCollection<Expression> Expressions { get; set; }

        public Package()
        {
            Expressions = new ObservableCollection<Expression>();
        }

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = new FileStream("config.dat", FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, this);
            }
        }

        public void Load()
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = new FileStream("config.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    Package package = (Package)bf.Deserialize(fs);
                    foreach (Expression t in package.Expressions)
                        Expressions.Add(t);
                }
                catch
                {
                    MessageBox.Show("Не найден конфигурационный файл.");
                }
            }
        }
    }
}
