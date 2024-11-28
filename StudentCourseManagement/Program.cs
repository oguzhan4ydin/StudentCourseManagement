using System;
using System.Collections.Generic;
using StudentCourseManagement.Models;

namespace StudentCourseManagement
{
    class Program
    {
        // dinamik listeler datayi tutabilmek icin
        static List<Student> students = new List<Student>();
        static List<Lecturer> lecturers = new List<Lecturer>();
        static List<Course> courses = new List<Course>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- Student and Course Management System ---");
                Console.WriteLine("1. Define Student");
                Console.WriteLine("2. Define Lecturer");
                Console.WriteLine("3. Define Course");
                Console.WriteLine("4. List Course Details");
                Console.WriteLine("5. Enroll Student in a Course");
                Console.WriteLine("6. Exit");
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DefineStudent();
                        break;
                    case "2":
                        DefineLecturer();
                        break;
                    case "3":
                        DefineCourse();
                        break;
                    case "4":
                        ListCourseDetails();
                        break;
                    case "5":
                        EnrollStudentInCourse();
                        break;
                    case "6":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

            }
        }
        // ogrenci tanimlama
        static void DefineStudent()
        {
            Console.WriteLine("\n--- Define Student ---");
            Console.Write("Enter Student First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Enter Student Last Name: ");
            var lastName = Console.ReadLine();

            var student = new Student
            {
                Id = students.Count + 1,
                FirstName = firstName,
                LastName = lastName
            };

            students.Add(student);
            Console.WriteLine($"Student {student.GetFullName()} has been successfully added.");
        }

        // ogretim gorevlisi tanimlama
        static void DefineLecturer()
        {
            Console.WriteLine("\n--- Define Lecturer ---");
            Console.Write("Enter Lecturer First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Enter Lecturer Last Name: ");
            var lastName = Console.ReadLine();

            var lecturer = new Lecturer
            {
                Id = lecturers.Count + 1,
                FirstName = firstName,
                LastName = lastName
            };

            lecturers.Add(lecturer);
            Console.WriteLine($"Lecturer {lecturer.GetFullName()} has been successfully added.");
        }

        // kurs tanimlama
        static void DefineCourse()
        {
            Console.WriteLine("\n--- Define Course ---");
            Console.Write("Enter Course Name: ");
            var courseName = Console.ReadLine();
            Console.Write("Enter Course Credit Hours: ");
            if (!int.TryParse(Console.ReadLine(), out var creditHours))
            {
                Console.WriteLine("Invalid credit hours. Operation canceled.");
                return;
            }

            if (lecturers.Count == 0)
            {
                Console.WriteLine("No lecturers available. Please define a lecturer first.");
                return;
            }

            Console.WriteLine("\nAvailable Lecturers:");
            for (int i = 0; i < lecturers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {lecturers[i].GetFullName()}");
            }

            Console.Write("Select the lecturer for the course: ");
            if (!int.TryParse(Console.ReadLine(), out var lecturerIndex) || lecturerIndex < 1 || lecturerIndex > lecturers.Count)
            {
                Console.WriteLine("Invalid selection. Operation canceled.");
                return;
            }

            var selectedLecturer = lecturers[lecturerIndex - 1];
            var course = new Course
            {
                CourseName = courseName,
                Credits = creditHours,
                _Lecturer = selectedLecturer
            };

            courses.Add(course);
            selectedLecturer.AssignedCourses.Add(course);
            Console.WriteLine($"Course '{course.CourseName}' has been successfully added and assigned to {selectedLecturer.GetFullName()}.");
        }

        // kurs listeleme
        static void ListCourseDetails()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("\nNo courses have been defined yet.");
                return;
            }

            Console.WriteLine("\n--- Defined Courses ---");
            for (int i = 0; i < courses.Count; i++)
            {
                var course = courses[i];
                Console.WriteLine($"{i + 1}. {course.GetCourseInfo()}");
            }

            Console.WriteLine("\nEnter a course number for more details, or press Enter to return to the main menu:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var courseIndex) && courseIndex >= 1 && courseIndex <= courses.Count)
            {
                var selectedCourse = courses[courseIndex - 1];
                Console.WriteLine($"\n--- Course Details ---");
                Console.WriteLine(selectedCourse.GetCourseInfo());
            }
            else if (!string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input. Returning to the main menu...");
            }
        }

        // ogrencileri kurslara atama (ogretim gorevlileri kurs tanimlarken ataniyor)
        static void EnrollStudentInCourse()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("\nNo students available. Please define a student first.");
                return;
            }

            if (courses.Count == 0)
            {
                Console.WriteLine("\nNo courses available. Please define a course first.");
                return;
            }

            Console.WriteLine("\n--- Enroll Student in a Course ---");

            // ogrenci secimi
            Console.WriteLine("\nAvailable Students:");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i].GetFullName()}");
            }

            Console.Write("Select a student by number: ");
            if (!int.TryParse(Console.ReadLine(), out var studentIndex) || studentIndex < 1 || studentIndex > students.Count)
            {
                Console.WriteLine("Invalid student selection. Operation canceled.");
                return;
            }
            var selectedStudent = students[studentIndex - 1];

            // ders secimi
            Console.WriteLine("\nAvailable Courses:");
            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {courses[i].CourseName}");
            }

            Console.Write("Select a course by number: ");
            if (!int.TryParse(Console.ReadLine(), out var courseIndex) || courseIndex < 1 || courseIndex > courses.Count)
            {
                Console.WriteLine("Invalid course selection. Operation canceled.");
                return;
            }
            var selectedCourse = courses[courseIndex - 1];

            // ogrenci kaydetme
            selectedCourse.AddStudent(selectedStudent);
            Console.WriteLine($"Student {selectedStudent.GetFullName()} has been successfully enrolled in course '{selectedCourse.CourseName}'.");
        }

    }
}
