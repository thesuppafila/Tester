using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Tester.Model
{
    [Serializable]
    public class Package
    {

        public Package()
        {
            Tests = new ObservableCollection<Test>();
            Groups = new ObservableCollection<Group>();
        }

        private ObservableCollection<Test> tests;
        public ObservableCollection<Test> Tests
        {
            get
            {
                return tests;
            }
            set
            {
                tests = value;
            }
        }


        private ObservableCollection<Model.Group> groups;
        public ObservableCollection<Model.Group> Groups
        {
            get
            {
                return groups;
            }
            set
            {
                groups = value;
            }
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
                    foreach (Model.Group g in package.Groups)
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
