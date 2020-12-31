using System;
using System.Collections.Generic;
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
    }
}
