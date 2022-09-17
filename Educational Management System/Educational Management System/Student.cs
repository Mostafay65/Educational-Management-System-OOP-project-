using System.Collections;

namespace Educational_Management_System;

public class Student:Person
{
    public ArrayList Registered_Courses=new ArrayList();
    public Student():base()
    {
    }
    public Student(string name,string username , string passwor,int id):base(name,username,passwor,id)
    {
    }

    public void AddCourse(Course c)
    {
        Registered_Courses.Add(c);
    }

   
    public void viewCourses()
    {
        foreach (Course c in Registered_Courses)
        {
            Console.WriteLine("ID : "+c.id + " - Course " + c.Name + "  With code : " + c.Code);
        }
    }
    public bool viewCourse(int id)
    {
        foreach (Course c in Registered_Courses)
        {
            if (c.id==id)
            {
                Console.Clear();
                Console.WriteLine("Course Name : " + c.Name);
                Console.WriteLine("Course code : " + c.Code);
                Console.WriteLine("Taught by Dr : " + c.Taught_By.Name);
                Console.WriteLine("Registered Student : " + c.Students.Count +"\n");
                Console.WriteLine($"This Course has {c.Assignments.Count} Assignment");
                foreach (Assignment ass in c.Assignments)
                {
                    Console.Write("Assignments "+ass.id+" - ");
                    bool f = false;
                    foreach (Solution Sol in ass.solutions)
                    {
                        if (Sol.student==this)
                        {
                            Console.Write("Submitted - ");
                            if (Sol.grade==0)
                            {
                                Console.Write("NA / 100");
                            }
                            else
                            {
                                Console.Write(Sol.grade+" / 100");
                            }
                            f = true;
                            Console.WriteLine();
                            break;
                        }
                    }

                    if (!f)
                    {
                        Console.WriteLine("NOT submitted - NA / 100");
                    }
                }
                return true;
            }
        }
        return false;
    }

    public void report()
    {
        foreach (Course c in Registered_Courses)
        {
            Console.WriteLine($"Course ( {c.Name} ) With code ( {c.Code} ) hase {c.Assignments.Count} Assignments ");
            int sum = 0,cnt=0;
            foreach (Assignment Ass in c.Assignments)
            {
                Console.Write($"Assignment {Ass.id} ");
                bool f = false;
                foreach (Solution sol in Ass.solutions)
                {
                    if (sol.student==this)
                    {
                        Console.Write("Submitted - ");
                        if (sol.grade==0)
                        {
                            Console.Write("NA / 100");
                        }
                        else
                        {
                            Console.Write(sol.grade+" / 100");
                            sum += sol.grade;
                            cnt++;
                        }
                        f = true;
                        Console.WriteLine();
                        break;
                    }
                }

                if (!f)
                {
                    Console.WriteLine("NOT submitted - NA / 100");
                }
            }

            Console.WriteLine($"Your total grade in this course is {sum} / {cnt*100}\n");
        }
    }
    public Course unUnRegister(int id)
    {
        Course rt=new Course();
        foreach (Course c in Registered_Courses)
        {
            if (c.id==id)
            {
                rt = c;
                Registered_Courses.Remove(c);
                break;
            }
        }
        return rt;
    }

    public bool SBSOL(string sol, int id,int i)
    {
        foreach (Course c in Registered_Courses)
        {
            if (c.id==id)
            {
                foreach (Assignment ass in c.Assignments)
                {
                    if (ass.id==i)
                    {
                        Solution s = new Solution(sol,this);
                        int choice;
                        foreach (Solution SOL in ass.solutions)
                        {
                            if (SOL.student==this)
                            {
                                Console.WriteLine("You actually have submitted a solution to this assignment ");
                                Console.WriteLine("Do you want to replace it ? ");
                                Console.WriteLine("1 - YES");
                                Console.WriteLine("2 - NO");
                                choice = int.Parse(Console.ReadLine());
                                while (choice!=1 && choice!=2)
                                {
                                    Console.Write("Please enter a valid choice : ");
                                    choice = int.Parse(Console.ReadLine());
                                }
                                if (choice==1)
                                {
                                    ass.solutions.Remove(SOL);
                                    break;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                        }
                        ass.solutions.Add(s);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
