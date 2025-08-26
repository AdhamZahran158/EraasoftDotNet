namespace Task4_Pt._2
{
    class Instructor
    {
        public Instructor(string name, string specialization)
        {
            Name = name;
            Specialization = specialization;

        }

        public string InstructorId = Guid.NewGuid().ToString().Substring(24,6);
        public string Name { get; private set; }
        public string Specialization { get; private set; }

        public string PrintDetails() => $"Name: {Name} \nID: {InstructorId} \nSpecialization: {Specialization} ";
    }
    class Student
    {
        public string id = Guid.NewGuid().ToString().Substring(24,6);
        public string Name { get; private set; }
        public int Age { get; private set; }

        public  List<Course> studentCourses = new List<Course>();

        public Student(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public bool Enroll(Course course)
        {
            if(studentCourses.Contains(course))
                return false;
            if (course == null) 
                return false;
            studentCourses.Add(course);
            return true;
        }

        public string PrintDetails()
        {
            return $"ID: {this.id} \nName: {Name} \nAge: {Age}";
        }
    }
     
    class Course
     {
            public string CourseId = Guid.NewGuid().ToString().Substring(24, 6);
            public string Title { get; private set; }
            public Instructor instructor;

        public Course(string title, Instructor instructor)
        {
            Title = title;
            this.instructor = instructor;
        }

        public string PrintDetails() => $"Course Name: {Title} \nCourse ID: {CourseId} \nInstructor: {instructor.Name}";
     }

    class School
    {
        public List<Student> students = new List<Student>();
        public List<Instructor> instructors = new List<Instructor>();
        public List<Course> courses = new List<Course>();

        public bool AddStudent(Student student)
        {
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].id == student.id)
                    return false;
            }
            students.Add(student);
            return true;
        }
        public bool AddCourse(Course course)
        {
            for (int i = 0; i < courses.Count; i++)
            {
                if (courses[i].CourseId == course.CourseId)
                    return false;
            }
            if (course == null || course.instructor == null)
                return false;
            courses.Add(course);
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            for (int i = 0; i < instructors.Count; i++)
            {
                if (instructors[i].InstructorId == instructor.InstructorId)
                    return false;
            }
            instructors.Add(instructor);
            return true;
        }

        public Student FindStudent(string id)
        {
            for (int i = 0; i < students.Count; i++)
            {
                if(students[i].id == id) return students[i];
            }
            return null;
        }

        public Course FindCourse(string id)
        {
            for (int i = 0; i < courses.Count; i++)
            {
                if (courses[i].CourseId == id) return courses[i];
            }
            return null;
        }

        public Instructor FindInstructor(string id)
        {
            for (int i = 0; i < instructors.Count; i++)
            {
                if (instructors[i].InstructorId == id) return instructors[i];
            }
            return null;
        }

        public bool EnrollStudentInCourse(string studentId, string courseId)
        {
            for(int i = 0;i < students.Count; i++)
            {
                if (students[i].id == studentId)
                {
                    Course foundCourse = FindCourse(courseId);
                    if(foundCourse == null)
                        return false;
                    students[i].Enroll(foundCourse);
                    return true;
                }
            }
            return false;
        }

        //                          {  BONUS  }                       \\
        public bool CheckEnrolled(string studentId, string courseId)
        {
            for (int i =0; i < students.Count;i++)
            {
                if (students[i].id == studentId)
                {
                    if (students[i].studentCourses.Contains(FindCourse(courseId)) && FindCourse(courseId) != null)
                        return true;
                }
            }
            return false ;
        }

        public string InstructorTeachesThisCourse(string courseName)
        {
            for(int i=0; i < courses.Count;i++)
            {
                if (courses[i].Title == courseName)
                {
                    return $"Instructor teaches this course is: {courses[i].instructor.Name}";
                }
            }
            return "Incorrect Information Please Provide a valid course";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            School school =new School();
            while (true)
            {
                Console.WriteLine("1. Add Student (hint: start with empty list of courses)\r\n2. Add Instructor\r\n3. Add Course (hint: NEED the instructor ID ! ! !)\r\n4. Enroll Student in Course\r\n5. Show All Students\r\n6. Show All Courses\r\n7. Show All Instructors\r\n8. Find the student by id or name\r\n9. Find the course by ID or name\r\n10. Check if the student enrolled the course\r\n11. Get the instructor name of the course by course name\r\n12. Exit");
                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter Student Name And Age Respectively");
                            Console.Write("Name --> ");
                            string studentName = Console.ReadLine();
                            Console.Write("Age --> ");
                            int studentAge = Convert.ToInt32(Console.ReadLine());
                            bool done = school.AddStudent(new Student(studentName,studentAge));
                            if (done)
                                Console.WriteLine("Added Student Successfully");
                            else
                                Console.WriteLine("Failed to add student");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter Instructor Name And Specialization Respectively");
                            Console.Write("Name --> ");
                            string insName = Console.ReadLine();
                            Console.Write("Specializaton --> ");
                            string insSpec = Console.ReadLine();
                            bool done = school.AddInstructor(new Instructor(insName, insSpec));
                            if (done)
                                Console.WriteLine("Added Instructor Successfully");
                            else
                                Console.WriteLine("Failed to add instructor");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter Course Title and Instructor ID Respectively");
                            Console.Write("Course Title --> ");
                            string courseTitle = Console.ReadLine();
                            Console.Write("Instructor ID --> ");
                            string insId = Console.ReadLine();
                            bool done = school.AddCourse(new Course(courseTitle,school.FindInstructor(insId)));
                            if (done)
                                Console.WriteLine("Added Course Successfully");
                            else
                                Console.WriteLine("Failed to add course");
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter Student ID And Course ID Respectively");
                            string[] inpParams = Console.ReadLine().Split(" ");
                            bool stats = school.EnrollStudentInCourse(inpParams[0], inpParams[1]);
                            if(stats)
                                Console.WriteLine("Success");
                            else
                                Console.WriteLine("Failed");
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Showing Students...\n");
                            
                            for(int i = 0; i < school.students.Count; i++)
                            {
                                Console.WriteLine(school.students[i].PrintDetails());
                            }
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Showing Courses...\n");
 
                            for (int i = 0; i < school.courses.Count; i++)
                            {
                                Console.WriteLine(school.courses[i].PrintDetails());
                            }
                            break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Showing Instructors...\n");
                           
                                for (int i = 0; i < school.instructors.Count; i++)
                                {
                                    Console.WriteLine(school.instructors[i].PrintDetails());
                                }
                           
                            break;
                        }
                    case 8:
                        {
                            Console.WriteLine("Enter Student ID or Name");
                            string studentKey = Console.ReadLine();
                            bool done = false;
                            for (int i = 0; i < school.students.Count; i++)
                            {
                                if (school.students[i].Name == studentKey || school.students[i].id == studentKey)
                                {
                                    Console.WriteLine($"Found Student\n" + school.students[i].PrintDetails());
                                    done = true;
                                    break;
                                }
                            }
                            if (!done)
                                Console.WriteLine("Did not find a student with this information");
                            break;
                        }
                    case 9:
                        {
                            Console.WriteLine("Enter Course ID or Name");
                            string courseKey = Console.ReadLine();
                            bool done =false;
                            for (int i = 0; i < school.courses.Count; i++)
                            {
                                if (school.courses[i].Title == courseKey || school.courses[i].CourseId == courseKey)
                                {
                                    Console.WriteLine($"Found Course\n" + school.courses[i].PrintDetails());
                                    done = true;
                                    break;
                                }
                            }
                            if(!done)
                                Console.WriteLine("Did not find a course with this information");
                            break;
                        }
                    case 10:
                        {
                            Console.WriteLine("Enter Student ID and Course ID");
                            Console.Write("Student ID --> ");
                            string studentId = Console.ReadLine();
                            Console.Write("Course ID --> ");
                            string courseId = Console.ReadLine();
                            if (school.CheckEnrolled(studentId, courseId))
                                Console.WriteLine("YES, The given student enrolled the given course");
                            else
                                Console.WriteLine("NO, the given student did not enroll the given course");
                            break;
                        }
                    case 11:
                        {
                            Console.WriteLine("Enter Course Name to find its instructor");
                            string courseName = Console.ReadLine();
                            Console.WriteLine(school.InstructorTeachesThisCourse(courseName));
                            break;
                        }
                    case 12:
                        {
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Choice");
                            break;
                        }
                }
                if (input == 12)
                    break;
            }
        }
    }
}
