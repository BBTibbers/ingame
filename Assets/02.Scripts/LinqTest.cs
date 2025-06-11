using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LinqTest : MonoBehaviour
{
    private void Start()
    {
        List<Student> students = new List<Student>()
        {
            new Student("Alice", 20, "A"),
            new Student("Bob", 22, "B"),
            new Student("Charlie", 21, "C"),
            new Student("David", 23, "A")
        };
    }
}
