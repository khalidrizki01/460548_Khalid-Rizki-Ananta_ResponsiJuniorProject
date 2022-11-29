using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHR
{
    internal class Employee
    {
        private int id;
        private string name;
        private string dept;

        public Employee (int id, string name, string dept)
        {
            this.id = id;
            this.name = name;
            this.dept = dept;
        }  
        
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Dept
        {
            get { return this.dept; }   
            set { this.dept = value; }
        }
    }
}
