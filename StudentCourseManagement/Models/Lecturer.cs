namespace StudentCourseManagement.Models
{
    public class Lecturer : Person
    {
        public List<Course> AssignedCourses { get; set; } = new List<Course>();

        public override string GetFullName()
        {
            return $"Dr. {FirstName} {LastName}";
        }

        public override string ShowInformation()
        {
            return $"Lecturer: {GetFullName()}, Assigned Courses: {AssignedCourses.Count}";
        }
    }
}

