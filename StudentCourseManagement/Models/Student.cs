namespace StudentCourseManagement.Models
{
    public class Student: Person
    {
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();

        public override string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public override string ShowInformation()
        {
            return $"Student: {GetFullName()}, Enrolled Courses: {EnrolledCourses.Count}";
        }
    }
}
