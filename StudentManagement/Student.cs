using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    internal class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Student(string studentID, string name, int age, string gender, string phoneNumber, string email)
        {
            Id = studentID;
            Name = name;
            Age = age;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
