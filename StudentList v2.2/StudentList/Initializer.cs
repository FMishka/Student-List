using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StudentList
{
    static class Initializer
    {
        const string fileName = @"std.txt";
        public static void Save(string path, Student[] students)
        {
            using (StreamWriter stream = new StreamWriter(path + "\\" + fileName))
            {
                for (int i = 0; i < StudentHandler.Count(students); i++)
                {
                    stream.WriteLine(students[i].id.ToString() + ";" + students[i].LastName + ";" + students[i].FristName + ";" + students[i].comment);
                }
            }
        }
        public static Student[] Load(string path)
        {
            Student[] students = new Student[0];
            try
            {
                using (StreamReader stream = new StreamReader(path +"\\" +fileName))
                {
                    string line = "";
                    string[] loadingStudent = new string[4];
                    while ((line = stream.ReadLine()) != null)
                    {
                        loadingStudent = line.Split(";");
                        Student student = new Student { id = Convert.ToUInt32(loadingStudent[0]), LastName = loadingStudent[1], FristName = loadingStudent[2], comment = loadingStudent[3] };
                        StudentHandler.AddStudent(ref students, student);
                    }
                }
            }
            catch (Exception)
            {
                Directory.CreateDirectory(path);
            }
            return students;
        }
    }
}
