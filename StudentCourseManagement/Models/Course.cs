using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourseManagement.Models
{
    public class Course
    {     
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public Lecturer _Lecturer { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();

        public void AddStudent(Student student)
        {
            EnrolledStudents.Add(student);
            student.EnrolledCourses.Add(this);
        }

        public string GetCourseInfo()
        {
            var studentNames = EnrolledStudents.Count > 0
                ? string.Join(", ", EnrolledStudents.ConvertAll(s => s.GetFullName()))
                : "No students enrolled";

            return $"Course: {CourseName}, Credits: {Credits}, Lecturer: {_Lecturer.GetFullName()}, Registered Students: {studentNames}";
        }
    }
}
