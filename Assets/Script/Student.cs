using UnityEngine;

public class Student 
{
    public string Name;
    public int Age;
    public string Grade;
    public Student(string name, int age, string grade)
    {
        Name = name;
        Age = age;
        Grade = grade;
    }
    public void DisplayInfo()
    {
        Debug.Log($"Name: {Name}, Age: {Age}, Grade: {Grade}");
    }

}
