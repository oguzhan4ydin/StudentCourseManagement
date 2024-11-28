using StudentCourseManagement.Models.Interfaces;

namespace StudentCourseManagement.Models
{
    public abstract class Person : IPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public abstract string GetFullName();
        public virtual string ShowInformation()
        {
            return $"ID: {Id}, Name: {GetFullName()}";
        }
    }
}
