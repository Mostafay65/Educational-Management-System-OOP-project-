using System;
using System.Collections;
using System.IO;
namespace Educational_Management_System
{
    class Program
    {
        static void BuildCourses(ref ArrayList AvailableCourses)
        {
            string File_name = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Courses data/Course data.txt";
            using (StreamReader reader=File.OpenText(File_name))
            {
                string line = reader.ReadLine();
                int cnt = 1;
                while (line!=null)
                {
                    string[] data = line.Split(' ');
                    Course c = new Course(data[0]+' '+data[1],data[2],cnt);
                    AvailableCourses.Add(c);
                    line = reader.ReadLine();
                    cnt++;
                }
                
            }
        }
        static void BuildDoctors(ref ArrayList TotalDoctors,ref ArrayList AvailableCourses)
        {
            // name username pass id
            string File_name = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Doctors data/Names.txt";
            using (StreamReader reader=File.OpenText(File_name))
            {
                string line = reader.ReadLine();
                int cnt = 0;
                while (line!=null)
                {
                    string []data = line.Split(" ");
                    Doctor D = new Doctor(data[0],data[0],data[1],cnt);
                    TotalDoctors.Add(D);
                    line = reader.ReadLine();
                    cnt++;
                }
            }
            
            // Course
            File_name = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Doctors data/Courses.txt";
            using (StreamReader reader = File.OpenText(File_name))
            {
                string line = reader.ReadLine();
                int cnt = 0;
                while (line != null)
                {
                    if (line == "0")
                    {
                        line = reader.ReadLine();
                        cnt++;
                        continue;
                    }

                    int cnt1 = 0;
                    foreach (Doctor curDoctor in TotalDoctors)
                    {
                        if (cnt1 == cnt)
                        {
                            string[] Course_code = line.Split(" ");
                            for (int i = 0; i < Course_code.Length; i++)
                            {
                                foreach (Course curCourse in AvailableCourses)
                                {
                                    if (curCourse.Code == Course_code[i])
                                    {
                                        curDoctor.AddCousrse(curCourse);
                                        curCourse.AddDoctor(curDoctor);
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                        cnt1++;
                    }
                    line = reader.ReadLine();
                    cnt++;
                }
            }
        }
        static void BuildStudents(ref ArrayList TotalStudens, ref ArrayList AvailableCourses)
        {
            string File_Name = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Students data/Names.txt";
            using (StreamReader reader=File.OpenText(File_Name))
            {
                string line = reader.ReadLine();
                int cnt = 0;
                while (line!=null)
                {
                    string []data = line.Split(" ");
                    Student S = new Student(data[0] + ' ' + data[1], data[0], data[2], cnt);
                    TotalStudens.Add(S);
                    line = reader.ReadLine();
                    cnt++;
                }
            }
            
            //Registered Courses
            File_Name = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Students data/Courses.txt";
            using (StreamReader reader=File.OpenText(File_Name))
            {
                string line = reader.ReadLine();
                int cnt = 0;
                while (line!=null)
                {
                    int cnt1 = 0;
                    string[] data = line.Split(" ");
                    foreach (Student CurStudent in TotalStudens)
                    {
                        if (cnt == cnt1)
                        {
                            for (int i = 0; i < data.Length; i++)
                            {
                                foreach (Course CurCourse in AvailableCourses)
                                {
                                    if (data[i]==CurCourse.Code)
                                    {
                                        CurStudent.AddCourse(CurCourse);
                                        CurCourse.AddStudent(CurStudent);
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                        cnt1++;
                    }
                    line = reader.ReadLine();
                    cnt++;
                }
            }

            BuildAssignment(ref TotalStudens,ref AvailableCourses);
        }
        static void BuildAssignment(ref ArrayList TotalStudens, ref ArrayList AvailableCourses)
        {
            string filename = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Courses data/Assignment.txt";
            using (StreamReader reader=File.OpenText(filename))
            {
                string line = reader.ReadLine();
                while (line!=null)
                {
                    string[] data = line.Split(" ");
                    foreach (Course c in AvailableCourses)
                    {
                        if (c.Code==data[0])
                        {
                            foreach (Assignment ass in c.Assignments)
                            {
                                if (ass.id==int.Parse(data[1]))
                                {
                                    for (int i = 2; i <data.Length; i+=3)
                                    {
                                        foreach (Student s in TotalStudens)
                                        {
                                            if (s.id==int.Parse(data[i]))
                                            {
                                                Solution solution = new Solution(data[i+1],s);
                                                solution.grade = int.Parse(data[i + 2]);
                                                ass.solutions.Add(solution);
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    line = reader.ReadLine();
                }
            }
        }
        static void Storecourses(ref ArrayList AvailableCourses)
        {
            string filename = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Courses data/Course data.txt";
            StreamWriter writer = new StreamWriter(filename);
            foreach (Course c in AvailableCourses)
            {
                writer.WriteLine(c.Name+" "+c.Code);
            }
            writer.Close();
            filename = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Courses data/Assignment.txt";
            writer = new StreamWriter(filename);
            foreach (Course c in AvailableCourses)
            {
                foreach (Assignment ass in c.Assignments)
                {
                    writer.Write($"{c.Code} {ass.id}");
                    foreach (Solution soll in ass.solutions)
                    {
                        writer.Write($" {soll.student.id} {soll.sol} {soll.grade}");
                    }
                    writer.Write("\n");
                }
            }
            writer.Close();
        }
        static void StoreStudents(ref ArrayList TotalStudens)
        {
            string filename = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Students data/Names.txt";
            StreamWriter writer = new StreamWriter(filename);
            foreach (Student s in TotalStudens)
            {
                writer.WriteLine($"{s.Name} {s.password}");
            }
            writer.Close();
            
            filename = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Students data/Courses.txt";
            writer = new StreamWriter(filename);
            foreach (Student s in TotalStudens)
            {
                foreach (Course c in s.Registered_Courses)
                {
                    writer.Write($"{c.Code} ");
                }
                writer.Write("\n");
            }
            writer.Close();
            
        }
        static void StoreDoctors(ref ArrayList TotalDoctors)
        {
            string filename = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Doctors data/Names.txt";
            StreamWriter writer = new StreamWriter(filename);
            foreach (Doctor d in TotalDoctors)
            {
                writer.WriteLine($"{d.Name} {d.password}");
            }
            writer.Close();
            
            filename = "/Users/mostafayoussef/RiderProjects/Educational Management System/Dummy data/Doctors data/Courses.txt";
            writer = new StreamWriter(filename);
            foreach (Doctor d in TotalDoctors)
            {
                if (d.courses.Count==0)
                {
                    writer.Write("0");
                }
                foreach (Course c in d.courses)
                {
                    writer.Write($"{c.Code} ");
                }
                writer.Write("\n");
            }
            writer.Close();
            
        }
        
        static void Main(string[] args)
        {
            ArrayList AvailableCourses = new ArrayList();
            ArrayList TotalDoctors = new ArrayList();
            ArrayList TotalStudens = new ArrayList();
            BuildCourses(ref AvailableCourses);
            BuildDoctors(ref TotalDoctors,ref AvailableCourses);
            BuildStudents(ref TotalStudens,ref AvailableCourses);
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("Please make a choice : ");
                Console.WriteLine(" \t 1 - Login \n \t 2 - Sign Up \n \t 3 - Shutdown system \n ");
                int choice = int.Parse(Console.ReadLine());
                if (choice==1)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter user name and password : ");
                    bool stu = false,doc=false;
                    Student currentStudent=new Student();
                    Doctor currentDoctor=new Doctor();
                    while (!stu && !doc)
                    {
                        Console.Write(" Username: ");
                        string username = Console.ReadLine();
                        Console.Write(" Password: ");
                        string password = Console.ReadLine();

                        foreach (Student curstudent in TotalStudens)
                        {
                            if (curstudent.CheckPass(username,password))
                            {
                                stu = true;
                                currentStudent = curstudent;
                                break;
                            }
                        }

                        foreach (Doctor curdoctor in TotalDoctors)
                        {
                            if (curdoctor.CheckPass(username,password))
                            {
                                doc = true;
                                currentDoctor = curdoctor;
                                break;
                            }
                        }

                        if (!stu && !doc )
                        {
                            Console.WriteLine("\nUsername and Password are incorrect \n \t Please enter a valid data :)\n");
                        }
                    }

                    if (stu)
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome "+ currentStudent.Name + ". Your are logged in :)");
                        while (true)
                        {
                            Console.WriteLine("Please make a choice :");
                            Console.WriteLine("\t 1 - Register in a Course");
                            Console.WriteLine("\t 2 - List my courses");
                            Console.WriteLine("\t 3 - View Course");
                            Console.WriteLine("\t 4 - Grades report");
                            Console.WriteLine("\t 5 - Log out");
                            choice = int.Parse(Console.ReadLine());
                            while (choice>5 || choice<1)
                            {
                                Console.Write("please enter a valid choice : ");
                                choice = int.Parse(Console.ReadLine());
                            }
                            if (choice==1)
                            {
                                Console.Clear();
                                ArrayList ids = new ArrayList(); 
                                foreach (Course course in AvailableCourses)
                                {
                                    if (course.Students.Contains(currentStudent))
                                    {
                                        continue;
                                    }
                                    course.view();
                                    ids.Add(course.id);
                                }

                                if (ids.Count==0)
                                {
                                    Console.WriteLine("You are registered in all our available courses :)");
                                    Console.WriteLine("\n\tpress any key to go back");
                                    Console.ReadLine();
                                    Console.Clear();
                                    continue;
                                }
                                Console.WriteLine("Which course Id to register in : ");
                                int id = int.Parse(Console.ReadLine());
                                while (!ids.Contains(id))
                                {
                                    Console.WriteLine("Please enter a valid ID");
                                    id = int.Parse(Console.ReadLine());
                                }
                                foreach (Course course in AvailableCourses)
                                {
                                    if (course.id==id)
                                    {
                                        course.Students.Add(currentStudent);
                                        currentStudent.Registered_Courses.Add(course);
                                        Console.WriteLine("You are registered in successfully\n");
                                        Console.WriteLine("\n \t  To go back enter any key");
                                        string s = Console.ReadLine();
                                        break;
                                    }
                                }
                                
                            }
                            else if (choice==2)
                            {
                                Console.Clear();
                                currentStudent.viewCourses();
                                Console.WriteLine("\n \t  To go back enter any key");
                                string s = Console.ReadLine();
                            }
                            else if (choice==3)
                            {
                                Console.Clear();
                                currentStudent.viewCourses();
                                Console.Write($"\nWhich ID course to view ? : ");
                                int id = int.Parse(Console.ReadLine());
                                while (!currentStudent.viewCourse(id))
                                {
                                    Console.Write("Please enter a valid ID : ");
                                    id = int.Parse(Console.ReadLine());
                                }
                                Console.WriteLine("\n \n Please make a  choice ");
                                Console.WriteLine("\t 1 - UnRegister from a Course");
                                Console.WriteLine("\t 2 - Submit solution");
                                Console.WriteLine("\t 3 - Back");
                                choice = int.Parse(Console.ReadLine());
                                while (choice<1 || choice >3)
                                {
                                    Console.Write("Please enter a valid code ");
                                    choice = int.Parse(Console.ReadLine());
                                }

                                if (choice==1)
                                {
                                    Course curCourse = currentStudent.unUnRegister(id);
                                    curCourse.removestudent(currentStudent);
                                    Console.Clear();
                                    continue;
                                }
                                else if(choice==2)
                                {
                                    Console.WriteLine("Which Assignment to submit?");
                                    int i = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Please Enter Your Solution (no spaces)");
                                    string solution = Console.ReadLine();
                                    while (!currentStudent.SBSOL(solution, id, i))
                                    {
                                        Console.Write("Please enter a valid Assignment id :");
                                        i = int.Parse(Console.ReadLine());
                                    }
                                }
                                else if(choice==3)
                                {
                                    Console.Clear();
                                    continue;
                                }
                            }
                            else if (choice==4)
                            {
                                Console.Clear();
                                currentStudent.report();
                                Console.WriteLine("\n\n\t Press any key to go back");
                                Console.ReadLine();
                            }
                            else if (choice==5)
                            {
                                Console.Write("Logging out");
                                for (int i = 0; i < 7; i++)
                                {
                                    Console.Write(".");
                                    Thread.Sleep(500);   
                                }
                                break;
                            }
                            
                            Console.Clear();
                        }
                    }
                    else if (doc)
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome Dr: "+ currentDoctor.Name + ". Your are logged in :)");
                        while (true)
                        {
                            Console.WriteLine("Please make a choice :");
                            Console.WriteLine("\t 1 - List courses");
                            Console.WriteLine("\t 2 - Create course");
                            Console.WriteLine("\t 3 - View Course");
                            Console.WriteLine("\t 4 - Log out");
                            choice = int.Parse(Console.ReadLine());
                            while (choice>4 || choice<1)
                            {
                                Console.Write("please enter a valid choice : ");
                                choice = int.Parse(Console.ReadLine());
                            }

                            if (choice==1)
                            {
                                Console.Clear();
                                Console.WriteLine("All availablr courses are : \n");
                                foreach (Course c in AvailableCourses)
                                {
                                    Console.WriteLine($"Course {c.Name}  with code {c.Code} is taught by Dr: {c.Taught_By.Name} and has {c.Students.Count} registered students");
                                }

                                Console.WriteLine("\n\n\t Press any key to go back");
                                Console.ReadLine();
                            }
                            else if (choice==2)
                            {
                                Course NEWCOURSE = new Course();
                                Console.Write("Course name ? (Two words only) ");
                                NEWCOURSE.Name = Console.ReadLine();
                                Console.Write("Course code ? ");
                                NEWCOURSE.Code = Console.ReadLine();
                                NEWCOURSE.id = AvailableCourses.Count + 1;
                                NEWCOURSE.Taught_By = currentDoctor;
                                currentDoctor.AddCousrse(NEWCOURSE);
                                AvailableCourses.Add(NEWCOURSE);
                                Console.WriteLine("The course is created successfully ");
                                Console.WriteLine("\n\t Press any key to go back");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else if (choice==3)
                            {
                                Console.Clear();
                                if (!currentDoctor.viewcourses())
                                {
                                    Console.WriteLine("You are not teaching any courses :( ");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("which ID Course to view");
                                    int id = int.Parse(Console.ReadLine());
                                    while (!currentDoctor.viewcourse(id))
                                    {
                                        Console.Write("Please enter a valid id : ");
                                        id = int.Parse(Console.ReadLine());
                                    }
                                    Console.Write("Press any key to go back : ");
                                    Console.ReadLine();
                                }
                            }
                            else if (choice==4)
                            {
                                Console.Write("Logging out");
                                for (int i = 0; i < 7; i++)
                                {
                                    Console.Write(".");
                                    Thread.Sleep(500);   
                                }
                                break;
                            }
                            Console.Clear();
                        }
                        
                    }
                }
                else if (choice==2)
                {
                    Console.Clear();
                    Console.WriteLine("Please make a choice : You are ");
                    Console.WriteLine(" \t 1 - Doctor \n \t 2 - Student \n ");
                    choice = int.Parse(Console.ReadLine());
                    while (choice!=1 && choice!=2)
                    {
                        Console.Write("Please make a valid choice : ");
                        choice = int.Parse(Console.ReadLine());
                    }

                    if (choice==1)
                    {
                        //Doctor
                        Doctor NEWDOC = new Doctor();
                        Console.Write("please enter your first name (username): ");
                        NEWDOC.Name = Console.ReadLine();
                        NEWDOC.username = NEWDOC.Name;
                        Console.Write("please enter your Password: ");
                        NEWDOC.password = Console.ReadLine();
                        NEWDOC.id = TotalDoctors.Count + 1;
                        TotalDoctors.Add(NEWDOC);
                        Console.WriteLine("Your account is created successfully please try to login");
                        Console.ReadLine();
                    }
                    else
                    {
                        //Student
                        Student NEWSTU = new Student();
                        Console.Write("please enter your first name (username): ");
                        NEWSTU.username = Console.ReadLine();
                        Console.Write("please enter your second name : ");
                        NEWSTU.Name = NEWSTU.username+' '+Console.ReadLine();
                        Console.Write("please enter your Password: ");
                        NEWSTU.password = Console.ReadLine();
                        NEWSTU.id = TotalStudens.Count + 1;
                        TotalStudens.Add(NEWSTU);
                        Console.WriteLine("Your account is created successfully please try to login");
                        Console.ReadLine();
                    }
                }
                else if (choice==3)
                {
                    Console.Clear();
                    Console.WriteLine("Your Application is shut down");
                    break;
                }
                else
                {
                    Console.WriteLine("   Please enter a valid choie : ");
                    Thread.Sleep(3000);
                    Console.Clear();
                }
            }

            Storecourses(ref AvailableCourses);
            StoreStudents(ref TotalStudens);
            StoreDoctors(ref TotalDoctors);

        }
    }
}