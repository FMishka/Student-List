using System;
using System.Collections.Generic;
using System.Text;

namespace StudentList
{
    static class StudentHandler
    {
        static public void AddStudent(ref Student[] students, Student student)
        {
            int count = Count(students);
            Student[] newStudents = new Student[count + 1];
            for (int i = 0; i < count; i++)
            {
                newStudents[i] = students[i];
            }
            newStudents[count] = new Student();
            newStudents[count].LastName = student.LastName;
            newStudents[count].FristName = student.FristName;
            newStudents[count].id = student.id;
            newStudents[count].comment = student.comment;
            students = newStudents;
        }
        static public void RemoveStudent(ref Student[] students, int number)
        {
            Student[] newStudents = new Student[Count(students) - 1];
            int nowCountOfstudents = 0;
            int countOfIterations = 0;
            foreach (var item in students)
            {
                if (countOfIterations != number)
                {
                    newStudents[nowCountOfstudents] = item;
                    ++nowCountOfstudents;
                }
                ++countOfIterations;
            }
            students = newStudents;
        }
        static public int Count(Student[] students)
        {
            int count = 0;
            if (students == null)
            {
                return 0;
            }
            else
            {
                foreach (var item in students)
                {
                    count++;
                }
                return count;
            }
        }
        static public void Sort(ref Student[] students)
        {
            int countStudents = Count(students);
            for (int i = 0; i < countStudents-1; i++)
            {
                //Не будет второго цикла, потому что мы знаем, что функция "AddStudent" добавляет в конец списка,
                //следовательно нам надо понять только куда
                if(string.Compare(students[i].LastName, students[countStudents - 1].LastName) > 0)
                {
                    Student temp = students[i];
                    students[i] = students[countStudents - 1];
                    students[countStudents - 1] = temp;
                }
            }
        }
    }
}