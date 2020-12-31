using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Collections.ObjectModel;

namespace WpfApp2
{ 
    public partial class MainWindow : Window
    {
        List<Student> studentlist = new List<Student>();
        ObservableCollection<string> studentNames = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            using (var reader = new StreamReader("D:\\2B.csv",Encoding.Default))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                var records = csv.GetRecords<Student>();
                foreach(var studentfile in records)
                {
                    studentlist.Add(studentfile);
                    studentNames.Add(studentfile.ID+studentfile.Name);
                    
                }
            }
            CBStudent.ItemsSource = studentNames;
            studentNames.Clear();
        }


    }
}
