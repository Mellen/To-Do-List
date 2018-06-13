using System;
using System.Windows;
using ToDoListApp.Properties;
using Microsoft.Win32;
using System.IO;

namespace ToDoListApp
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            // Load the value into the text box.
            FilePath_Text.Text = Settings.Default.listFileName;
        }

        private void Confirm_Click(object sender, EventArgs e)
        {

            // Update the value.
            Settings.Default.listFileName = FilePath_Text.Text;
            // Save the config file.
            Settings.Default.Save();
            if(Settings.Default.listFileName.ToString() == FilePath_Text.Text)
            {
                MessageBox.Show("Setting Saved", "Done");
            }
            else
            {
                MessageBox.Show("Please try again", "Something Wrong");
            }
            this.Close();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog path = new SaveFileDialog();

            // SaveFileDialog Setting
            path.Title = "選擇目標路徑";
            path.Filter = "Todo List File(*.todo)|*.todo";
            path.FileName = "List";
            path.ShowDialog();
            // Check whether the file name is empty
            FilePath_Text.Text = path.FileName.ToString();
        }
    }
}
