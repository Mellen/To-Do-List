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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ToDoListApp
{
    /// <summary>
    /// Interaction logic for EditPopup.xaml
    /// </summary>
    public partial class EditPopup : Window
    {
        public EditPopup(ToDoItem item)
        {
            InitializeComponent();
            DataContext = item;
        }
    }
}
