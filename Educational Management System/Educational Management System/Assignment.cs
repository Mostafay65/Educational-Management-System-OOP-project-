using System.Collections;

namespace Educational_Management_System;

public class Assignment
{
    public int id;
    public ArrayList solutions=new ArrayList();

    public Assignment(int id)
    {
        this.id = id;
    }
}