using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Configuration;
using System.IO;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XDocument ToDoList;
        public MainWindow()
        {
            InitializeComponent();
            AppSettingsReader asr = new AppSettingsReader();
            string listFileName = (string)asr.GetValue("listFileName", typeof(string));
            if (File.Exists(listFileName))
            {
                ToDoList = XDocument.Load(listFileName);
                LoadItems();
            }
            else
            {
                ToDoList = new XDocument();
                ToDoList.Add(new XElement("todolist"));
            }
        }

        private void LoadItems()
        {
            lvToDo.Items.Clear();
            foreach (XElement item in ToDoList.Root.Descendants("todo").Where(x => x.Descendants("donedatetime").Count() == 0))
            {
                lvToDo.Items.Add(item.Element("description").Value);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!txtItemDesc.Text.Trim().Equals(string.Empty))
            {
                XElement item = new XElement("todo");
                XElement createAt = new XElement("createdatetime");
                createAt.Value = DateTime.Now.ToString("yyyy-dd-MMThh:mm:ss.ms");
                item.Add(createAt);
                XElement desc = new XElement("description");
                desc.Value = txtItemDesc.Text.Trim();
                item.Add(desc);
                ToDoList.Root.Add(item);
                lvToDo.Items.Add(item.Element("description").Value);
                txtItemDesc.Text = "";
            }
        }

        private bool notdone(XElement x)
        {
            return (x.Descendants("donedatetime") == null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AppSettingsReader asr = new AppSettingsReader();
            string listFileName = (string)asr.GetValue("listFileName", typeof(string));
            ToDoList.Save(listFileName);
        }

        private void lvToDo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult done = MessageBox.Show("Mark this as done?", "Done?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (done == MessageBoxResult.Yes)
            {
                XElement item = ToDoList.Root.Descendants("todo").Where(x => x.Descendants("donedatetime").Count() == 0).ElementAt(lvToDo.SelectedIndex);
                XElement doneAt = new XElement("donedatetime", DateTime.Now.ToString("yyyy-dd-MMThh:mm:ss.ms"));
                item.Add(doneAt);
                LoadItems();
            }
        }
    }
}
