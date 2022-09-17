using System.Collections;

namespace Educational_Management_System;

public class Doctor:Person
{
    public ArrayList courses=new ArrayList();
    public Doctor():base()
    {
    }
    public Doctor(string name,string username , string passwor,int id):base(name,username,passwor,id)
    {
    }

    public void AddCousrse(Course course)
    {
        courses.Add(course);
    }

    public bool viewcourses()
    {
        if (this.courses.Count==0)
        {
            return false;
        }
        foreach (Course c in courses)
        {
            Console.WriteLine($"ID - {c.id} Course {c.Name} ");
        }
        return true;
    }
    public bool viewcourse(int id)
    {
        foreach (Course c in courses)
        {
            if (c.id==id)
            {
                Console.Clear();
                Console.WriteLine($"Course Name : {c.Name}\nCourse code : {c.Code}\nRegistered students {c.Students.Count} \nTotal Assignments {c.Assignments.Count}");
                Console.Write("\nTo view Assignment solution and mark them enter 1 : ");
                int choice = int.Parse(Console.ReadLine());
                if (choice==1)
                {
                    foreach (Assignment ass in c.Assignments)
                    {
                        Console.Write($"Assignment {ass.id} - has {ass.solutions.Count} Solutions\n");
                        if (ass.solutions.Count!=0)
                        {
                            foreach (Solution solution in ass.solutions)
                            {
                                Console.WriteLine($"A solution ( {solution.sol} ) is submited by Student {solution.student.Name}");
                                Console.Write("Grade ? ");
                                solution.grade = int.Parse(Console.ReadLine());
                            }
                        }
                    }
                }
                return true;
            }
        }
        return false;
    }
    
}