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
        List<AllFile> teacherFileslist = new List<AllFile>();
        public MainWindow()
        {
            InitializeComponent();
            OpenFileInListStudent();//讀入學生檔案
            OpenFileInListAllTeacher();//讀入課程資料
        }

        private void OpenFileInListAllTeacher()
        {
            using (var reader = new StreamReader("D:\\course.csv", Encoding.Default))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                var records = csv.GetRecords<AllFile>();
                foreach (var Allfile in records)
                {
                    teacherFileslist.Add(Allfile);
                }
            }
            AllClassList.ItemsSource = teacherFileslist;
        }

        private void OpenFileInListStudent()
        {
            using (var reader = new StreamReader("D:\\2B.csv", Encoding.Default))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                var records = csv.GetRecords<Student>();
                foreach (var studentfile in records)
                {
                    studentlist.Add(studentfile);
                }
            }
            CBStudent.ItemsSource = studentlist;
        }
    }
}
