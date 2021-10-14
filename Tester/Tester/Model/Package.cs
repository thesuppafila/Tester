using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace Tester.Model
{
    [Serializable]
    public class Package
    {
        public ObservableCollection<Test> Tests { get; set; }

        public ObservableCollection<Group> Groups { get; set; }

        public Package()
        {
            Tests = new ObservableCollection<Test>();
            Groups = new ObservableCollection<Group>();
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
                    foreach (Test t in package.Tests)
                        Tests.Add(t);
                    foreach (Group g in package.Groups)
                        Groups.Add(g);
                }
                catch
                {
                    MessageBox.Show("Не найден конфигурационный файл.");
                }
            }
        }
    }
}
