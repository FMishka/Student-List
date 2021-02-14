using System;
using System.IO;

namespace StudentList
{
    class Student
    {
        public uint id;
        private string firstName;
        private string lastName;
        private static uint countUsers;
        public string FirstName
        {
            set
            {
                if (value == null)
                {
                    firstName = "";
                }
                else
                {
                    firstName = value;
                }
            }
            get
            {
                return firstName;
            }
        }
        public string LastName
        {
            set
            {
                if (value == null)
                {
                    lastName = "";
                }
                else
                {
                    lastName = value;
                }
            }
            get
            {
                return lastName;
            }
        }
        public static uint CountUsers
        {
            get
            {
                return countUsers;
            }
        }
        public Student()
        {

        }
        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            countUsers++;
        }
        public Student(string firstName, string lastName, uint id) : this(firstName, lastName)
        {
            this.id = id;
        }
        public static void Sort(ref Student[] students)
        {
            if (countUsers != 0)
            {
                for (int i = 0; i < countUsers - 1; i++)
                {
                    for (int j = i + 1; j < countUsers; j++)
                    {
                        if (string.Compare(students[i].LastName, students[j].LastName) == 1)
                        {
                            ChangeElements(ref students[i].lastName, ref students[j].lastName);
                            ChangeElements(ref students[i].firstName, ref students[j].firstName);
                            ChangeElements(ref students[i].id, ref students[j].id);

                        }
                    }
                }
            }
        }
        public static void RemoveStudent(ref Student[] students, string lastName)
        {
            int nowCountOfStudents = 0;
            Student[] newListStudents = new Student[CountUsers - 1];
            foreach (var item in students)
            {
                if (item.LastName != lastName)
                {
                    newListStudents[nowCountOfStudents] = item;
                    ++nowCountOfStudents;
                }
            }
            --countUsers;
            students = newListStudents;
        }
        public static void Add(ref Student[] students, string lastName, string firstName, uint id)
        {
            Student[] newStudentList = new Student[CountUsers + 1];
            StudentInitializing(ref newStudentList, CountUsers + 1);
            for (int i = 0; i < CountUsers; i++)
            {
                newStudentList[i].LastName = students[i].LastName;
                newStudentList[i].FirstName = students[i].FirstName;
                newStudentList[i].id = students[i].id;
            }
            newStudentList[CountUsers] = new Student(firstName, lastName, id);
            students = newStudentList;
        }
        public static void EditLastName(ref Student[] student, string oldLastName, string newLastName)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                if (student[i].LastName == oldLastName)
                {
                    student[i].LastName = newLastName;
                    break;
                }
            }
        }
        public static void EditFirstName(ref Student[] student, string oldFirstName, string newFirstName)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                if (student[i].FirstName == oldFirstName)
                {
                    student[i].FirstName = newFirstName;
                    break;
                }
            }
        }
        public static void EditId(ref Student[] student, uint oldId, uint newId)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                if (student[i].id == oldId)
                {
                    student[i].id = newId;
                    break;
                }
            }
        }
        private static void ChangeElements(ref string firstArg, ref string secondArg)
        {
            string nowValue = firstArg;
            firstArg = secondArg;
            secondArg = nowValue;
        }
        private static void ChangeElements(ref uint firstArg, ref uint secondArg)
        {
            uint nowValue = firstArg;
            firstArg = secondArg;
            secondArg = nowValue;
        }
        private static void StudentInitializing(ref Student[] students, uint countOfStudents)
        {
            for (int i = 0; i < countOfStudents; i++)
            {
                students[i] = new Student();
            }
        }
    }
    static class Writer
    {
        public static void WriteStudents(string directory ,Student[] students)
        {
            StreamWriter writer = new StreamWriter(directory, false);
            string studentList = "";
            foreach (var student in students)
            {
                studentList += $"{student.LastName} {student.FirstName} {student.id}\n";
            }
            writer.Write(studentList);
            writer.Close();
        }
        public static void InitializeStudents(ref Student[] students, string directory)
        {
            StreamReader reader = new StreamReader(directory);
            Student[] allStudents = new Student[0];
            string nowStudent;
            while ((nowStudent = reader.ReadLine()) != null)
            {
                string[] student = nowStudent.Split(" ");
                Student.Add(ref allStudents, student[0], student[1], Convert.ToUInt32(student[2]));
            }
            reader.Close();
            students = allStudents;
        }
    }
    static class Show
    {
        public static void PrintStudents(Student[] student)
        {
            Console.WriteLine($"{"Фамилия",10} {"Имя",10} {"Id",10}");
            for (int i = 0; i < Student.CountUsers; i++)
            {
                Console.WriteLine($"{student[i].LastName,10} {student[i].FirstName,10} {student[i].id,10}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Student[] student = new Student[0];
            string directory = @"E:\StudentList";
            string subDirectory = @"E:\StudentList\list.txt";
            Console.WriteLine("Существующие команды:");
            if (Directory.Exists(subDirectory) != false)
            {
                Directory.CreateDirectory(directory);
                File.Create(subDirectory).Close();
            }
            Writer.InitializeStudents(ref student, subDirectory);
            while (true)
            {
                Console.WriteLine("add, edit, remove, show");
                Console.WriteLine("Выберите команду:");
                string decision = Console.ReadLine().ToLower();
                //Инициализация студентов
                switch (decision)
                {
                    case "add":
                        Console.WriteLine("Введите имя:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Введите фамилию:");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Введите номер зачётки:");
                        uint id = Convert.ToUInt32(Console.ReadLine());
                        Student.Add(ref student, lastName, firstName, id);
                        Student.Sort(ref student);
                        Writer.WriteStudents(subDirectory, student);
                        Console.Clear();
                        break;

                    case "edit":
                        Show.PrintStudents(student);
                        Console.WriteLine("Что менять?");
                        Console.WriteLine("FirstName, LastName, Id");
                        string choice = Console.ReadLine().ToLower();
                        switch (choice)
                        {
                            case "firstname":
                                Console.WriteLine("Введите имя, которое надо изменить:");
                                string oldFirstName = Console.ReadLine();
                                Console.WriteLine("Введите новое имя:");
                                string newFirstName = Console.ReadLine();
                                Student.EditFirstName(ref student, oldFirstName, newFirstName);
                                Student.Sort(ref student);
                                Writer.WriteStudents(subDirectory, student);
                                Console.Clear();
                                break;

                            case "lastname":
                                Console.WriteLine("Введите фамилию, которую надо изменить:");
                                string oldLastName = Console.ReadLine();
                                Console.WriteLine("Введите новую фамилию:");
                                string newLastName = Console.ReadLine();
                                Student.EditLastName(ref student, oldLastName, newLastName);
                                Student.Sort(ref student);
                                Writer.WriteStudents(subDirectory, student);
                                Console.Clear();
                                break;

                            case "id":
                                Console.WriteLine("Введите идентификатор, который надо изменить:");
                                uint oldId = Convert.ToUInt32(Console.ReadLine());
                                Console.WriteLine("Введите новый идентификатор:");
                                uint newId = Convert.ToUInt32(Console.ReadLine());
                                Student.EditId(ref student, oldId, newId);
                                Student.Sort(ref student);
                                Writer.WriteStudents(subDirectory, student);
                                Console.Clear();
                                break;

                            default:
                                Console.WriteLine("Введена неверная команда!");
                                break;
                        }
                        break;

                    case "remove":
                        Show.PrintStudents(student);
                        Console.WriteLine("Введите фамилию студента:");
                        string lastNameForRemove = Console.ReadLine();
                        Student.RemoveStudent(ref student, lastNameForRemove);
                        Writer.WriteStudents(subDirectory, student);
                        Console.Clear();
                        break;
                    
                    case "show":
                        Console.Clear();
                        Show.PrintStudents(student);
                        break;

                    default:
                        Console.WriteLine("Введена неверная команда!");
                        break;
                }
            }
        }
    }
}
