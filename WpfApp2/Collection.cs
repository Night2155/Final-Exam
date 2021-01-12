using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration.Attributes;


namespace WpfApp2
{
    class Student
    {
        [Index(0)]
        public string ID { get; set; }
        [Index(1)]
        public string Name { get; set; }
        public override string ToString()
        {
            return $"{this.ID} {this.Name}";
        }
    }
    class AllFile //選課詳細表
    {
        [Index(0)]
        public string TeacherName { get; set; } //教師名稱
        [Index(1)]
        public string CourseName { get; set; } //課程名稱
        [Index(2)]
        public string Point { get; set; } //學分
        [Index(3)]
        public string Type { get; set; } //必、選修
        [Index(4)]
        public string OpeningClass { get; set; }//開課班級
        [Index(5)]
        public string ClassTime { get; set; } //上課日期
        public override string ToString()
        {
            return $"教師姓名: {this.TeacherName} 課程名稱: {this.CourseName} 學分數: {this.Point} 必選修: {this.Type} 開課班級: {this.OpeningClass} 開課時間: {this.ClassTime}";
        }
    }

    class Teacher 
    {
        public Teacher()
        {
            this.teacherclassfiles = new ObservableCollection<Teacherclassfile>();
        }
        public string TeacherName { get; set; }
        public int Number { get; set; }
        public ObservableCollection<Teacherclassfile> teacherclassfiles { get; set; }

        public override string ToString()
        {
            return $"{"老師姓名: "}{this.TeacherName}";
        }
    }

    class Teacherclassfile
    {
        public string TeacherName { get; set; }//老師名稱
        public string ClassName { get; set; } //開課班級
        public string TeacherCourseName { get; set; }//課程名稱
        public string ClassType { get; set; }//必、選修
        public string Point { get; set; }//學分
        public int ClassNumber { get; set; }//課程編號
        public override string ToString()
        {
            return $" 老師姓名: {this.TeacherName} 開課班級: {this.ClassName} 學分數: {this.Point} 課程名稱: {this.TeacherCourseName} 必、選修: {this.ClassType}";
        }
    }
    class SelsctedList 
    { 
        public Student SelectStudent { get; set; }
        public Teacherclassfile SelectClass { get; set; }
    
    }
    class FinalCSV 
    {
        public string 學號 { get; set; }
        public string 姓名 { get; set; }
        public int 學科總數 { get; set; }
        public int 學分總數 { get; set; }

    }


}
