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
        List<Teacher> teacherList = new List<Teacher>();
        public MainWindow()
        {
            InitializeComponent();
            OpenFileInListStudent();//讀入學生檔案
            OpenFileInListAllTeacher();//讀入課程資料
            
        }
        private void OpenFileInListAllTeacher()
        {
            string Text = " ";
            int count = -1;
            using (var reader = new StreamReader("D:\\course.csv", Encoding.Default))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                var records = csv.GetRecords<AllFile>();
                foreach (var Allfile in records)
                {
                    Teacher treenodes = new Teacher();
                    teacherFileslist.Add(Allfile);
                    if(Text != Allfile.TeacherName)
                    {
                        Text = Allfile.TeacherName;
                        treenodes.TeacherName = Allfile.TeacherName;
                        treenodes.teacherclassfiles.Add(new Teacherclassfile() {TeacherName = Allfile.TeacherName, ClassName = Allfile.OpeningClass, TeacherCourseName = Allfile.CourseName, ClassType = Allfile.Type, Point = Allfile.Point });
                        teacherList.Add(treenodes);
                        count += 1;
                    }
                    else
                    {
                        teacherList[count].teacherclassfiles.Add(new Teacherclassfile() { TeacherName = Allfile.TeacherName, ClassName = Allfile.OpeningClass, TeacherCourseName = Allfile.CourseName, ClassType = Allfile.Type, Point = Allfile.Point });
                    }
                }
            }
            AllClassList.ItemsSource = teacherFileslist;
            trTeacher.ItemsSource = teacherList;
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

        private void trTeacher_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Showclass.Content = trTeacher.SelectedItem;
        }
    }
}
