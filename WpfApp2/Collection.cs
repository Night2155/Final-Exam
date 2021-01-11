﻿using System;
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
            return $"{this.TeacherName} {this.CourseName} {this.Point} {this.Type} {this.OpeningClass} {this.ClassTime}";
        }
    }

    class Teacher 
    {
        public Teacher()
        {
            this.teacherclassfiles = new ObservableCollection<Teacherclassfile>();
        }
        public string TeacherName { get; set; }
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

        public override string ToString()
        {
            return $"{"老師姓名: "}{this.TeacherName} {this.ClassName} {" 學分數: "}{this.Point} {" 課程名稱: "}  {this.TeacherCourseName} {this.ClassType}";
        }
    }


}
