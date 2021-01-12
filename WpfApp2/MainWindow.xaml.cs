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
        List<SelsctedList> selsctedLists = new List<SelsctedList>();
        List<FinalCSV> finalCSVs = new List<FinalCSV>();
        int finalpoint = 0, finalsubjectnum = 0;
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
            int fileNumber = -1;
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
                        count += 1;
                        fileNumber += 1;
                        Text = Allfile.TeacherName;
                        treenodes.Number = fileNumber;
                        treenodes.TeacherName = Allfile.TeacherName;
                        treenodes.teacherclassfiles.Add(new Teacherclassfile() {ClassNumber = fileNumber, TeacherName = Allfile.TeacherName, ClassName = Allfile.OpeningClass, TeacherCourseName = Allfile.CourseName, ClassType = Allfile.Type, Point = Allfile.Point});
                        teacherList.Add(treenodes);
                        
                    }
                    else
                    {
                        fileNumber += 1;
                        treenodes.Number = fileNumber;
                        teacherList[count].teacherclassfiles.Add(new Teacherclassfile() {ClassNumber = fileNumber, TeacherName = Allfile.TeacherName, ClassName = Allfile.OpeningClass, TeacherCourseName = Allfile.CourseName, ClassType = Allfile.Type, Point = Allfile.Point});
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
            if(trTeacher.SelectedItem is Teacherclassfile)
            {
                Showclass.Content = trTeacher.SelectedItem;
            }
        }

        private void InToListButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (CBStudent.SelectedItem != null)
            {
                if(trTeacher.SelectedItem != null)
                {
                    trteacherSeleted();                    
                }
                if (AllClassList.SelectedItem != null)
                {
                    ListSeleted();                    
                }

            }
            PointSubject.Content = $"目前學分數:{finalpoint} 目前學科數: {finalsubjectnum}";
        }

        private void ListSeleted()
        {
            Student liststudent = new Student()
            {
                ID = studentlist[CBStudent.SelectedIndex].ID,
                Name = studentlist[CBStudent.SelectedIndex].Name
            };
            Teacherclassfile listteacherclassfile = new Teacherclassfile()
            {
                ClassName = teacherFileslist[AllClassList.SelectedIndex].OpeningClass,
                TeacherCourseName = teacherFileslist[AllClassList.SelectedIndex].CourseName,
                ClassType = teacherFileslist[AllClassList.SelectedIndex].Type,
                Point = teacherFileslist[AllClassList.SelectedIndex].Point,
                TeacherName = teacherFileslist[AllClassList.SelectedIndex].TeacherName,
                ClassNumber = AllClassList.SelectedIndex
            };
            SelsctedList listselsctedList = new SelsctedList()
            {
                SelectStudent = liststudent,
                SelectClass = listteacherclassfile
            };
            if (selsctedLists.Count > 0)
            {
                int s = 0;//紀錄重複項目的數量             
                for (int i = 0; i < selsctedLists.Count; i++)
                {

                    if (selsctedLists[i].SelectClass.ClassNumber == listselsctedList.SelectClass.ClassNumber)
                        s += 1;
                    if (selsctedLists[i].SelectClass.ClassNumber != listselsctedList.SelectClass.ClassNumber && i == selsctedLists.Count - 1 && s == 0)
                    {
                        selsctedLists.Add(listselsctedList);
                        finalpoint += Convert.ToInt32(listselsctedList.SelectClass.Point);
                        finalsubjectnum += 1;
                    }

                }
            }
            else if (selsctedLists.Count == 0)
            {
                selsctedLists.Add(listselsctedList);
                finalpoint += Convert.ToInt32(listselsctedList.SelectClass.Point);
                finalsubjectnum += 1;
            }
            FinalView.ItemsSource = selsctedLists;
            FinalView.Items.Refresh();

        }

        private void trteacherSeleted()
        {
            Student student = new Student()
            {
                ID = studentlist[CBStudent.SelectedIndex].ID,
                Name = studentlist[CBStudent.SelectedIndex].Name
            };
            SelsctedList selsctedList = new SelsctedList()
            {
                SelectStudent = student,
                SelectClass = trTeacher.SelectedItem as Teacherclassfile
            };
            if (selsctedLists.Count > 0)
            {
                int s = 0;//紀錄重複項目的數量             
                for (int i = 0; i < selsctedLists.Count; i++)
                {

                    if (selsctedLists[i].SelectClass.ClassNumber == selsctedList.SelectClass.ClassNumber)
                        s += 1;
                    if (selsctedLists[i].SelectClass.ClassNumber != selsctedList.SelectClass.ClassNumber && i == selsctedLists.Count - 1 && s == 0)
                    {                       
                        selsctedLists.Add(selsctedList);
                        finalpoint += Convert.ToInt32(selsctedList.SelectClass.Point);
                        finalsubjectnum += 1;
                    }

                }
            }
            else if (selsctedLists.Count == 0)
            {
                selsctedLists.Add(selsctedList);
                finalpoint += Convert.ToInt32(selsctedList.SelectClass.Point);
                finalsubjectnum += 1;
            }
            FinalView.ItemsSource = selsctedLists;
            FinalView.Items.Refresh();
        }

        private void OutListButton_Click(object sender, RoutedEventArgs e)
        {
            selsctedLists.RemoveAt(FinalView.SelectedIndex);
            FinalView.Items.Refresh();
            finalsubjectnum -= 1;
            finalpoint -= Convert.ToInt32(teacherFileslist[FinalView.SelectedIndex+1].Point);
            PointSubject.Content = $"目前學分數:{finalpoint} 目前學科數: {finalsubjectnum}";
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            FinalCSV final = new FinalCSV
            {
                學號 = studentlist[CBStudent.SelectedIndex].ID,
                姓名 = studentlist[CBStudent.SelectedIndex].Name,
                學分總數 = finalpoint,
                學科總數 = finalsubjectnum
            };
            
            PointSubject.Content = $"目前學分數:{finalpoint} 目前學科數: {finalsubjectnum}";

            using (var writer = new StreamWriter($"E:\\{CBStudent.SelectedItem}選課清單.csv",false,Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                finalCSVs.Add(final);
                csv.WriteRecords(finalCSVs);
            }
            finalCSVs.Clear();
            
        }
        private void CBStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selsctedLists.Clear();
            finalpoint = 0;
            finalsubjectnum = 0;
        }

        private void AllClassList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                Showclass.Content = AllClassList.SelectedItem;
        }
    }
}
