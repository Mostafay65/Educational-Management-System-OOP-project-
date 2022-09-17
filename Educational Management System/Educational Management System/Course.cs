using System.Collections;

namespace Educational_Management_System;

public class Course
{
    public string Name;
    public string Code;
    public int id;
    public Doctor Taught_By;
    public ArrayList Students=new ArrayList();
    public ArrayList Assignments=new ArrayList();
    public Course()
    {
        Assignment ass = new Assignment(1);
        Assignments.Add(ass);
        ass = new Assignment(2);
        Assignments.Add(ass);
    }
    public Course(string name, string code,int id)
    {
        this.Name = name;
        this.Code = code;
        this.id = id;
        Assignment ass = new Assignment(1);
        Assignments.Add(ass);
        ass = new Assignment(2);
        Assignments.Add(ass);
    }

    public void AddDoctor(Doctor d)
    {
        this.Taught_By = d;
    }
    public void AddStudent(Student S)
    {
        Students.Add(S);
    }

    public void removestudent(Student s)
    {
        Students.Remove(s);
        foreach (Assignment ass in this.Assignments)
        {
            foreach (Solution sol in ass.solutions)
            {
                if (sol.student==s)
                {
                    ass.solutions.Remove(sol);
                    break;
                }
            }
        }
    }

    public void view()
    {
        Console.WriteLine("ID : "+this.id + " - Course " + this.Name + "  With code : " + this.Code+ " is taught by : "+ this.Taught_By.Name);
    }
}