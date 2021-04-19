using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentList
{
    public partial class MainWindow : Window
    {
        Student[] students = new Student[0];
        int selectedStudentNumber;
        string path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\StudentList";

        private static void FillingStudentList(ListView list, Student[] students)
        {
            int count = 0;
            list.Items.Clear();
            foreach (var item in students)
            {
                count++;
                string[] str = { count.ToString(), item.id.ToString(), item.LastName, item.FristName, item.comment };
                list.Items.Add(str);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            students = Initializer.Load(path);
            FillingStudentList(StudentList, students);
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Initializer.Save(path, students);
            this.Close();
        }

        private void ButtonTurn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Drag(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Student student = new Student { LastName = lastNameTextBox.Text, FristName = firstNameTextBox.Text, id = Convert.ToUInt32(idTextBox.Text), comment = commentTextBox.Text };
                StudentHandler.AddStudent(ref students, student);
                StudentHandler.Sort(ref students);

                FillingStudentList(StudentList, students);

                idTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                idTextBox.Clear();
                firstNameTextBox.Clear();
                lastNameTextBox.Clear();
                commentTextBox.Clear();

                Initializer.Save(path, students);
            }
            catch (Exception)
            {
                idTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            selectedStudentNumber = StudentList.SelectedIndex;
            if (selectedStudentNumber != -1)
            {
                StudentList.Items.Clear();
                StudentHandler.RemoveStudent(ref students, selectedStudentNumber);

                FillingStudentList(StudentList, students);

                Initializer.Save(path, students);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            selectedStudentNumber = StudentList.SelectedIndex;
            if (selectedStudentNumber != -1)
            {
                ConfirmButton.Visibility = Visibility.Visible;
                idTextBox.Text = students[selectedStudentNumber].id.ToString();
                lastNameTextBox.Text = students[selectedStudentNumber].LastName.ToString();
                firstNameTextBox.Text = students[selectedStudentNumber].FristName.ToString();
                commentTextBox.Text = students[selectedStudentNumber].comment.ToString();

                EditButton.Foreground = new SolidColorBrush(Colors.Red);
                AddButton.Foreground = new SolidColorBrush(Colors.Red);
                RemoveButton.Foreground = new SolidColorBrush(Colors.Red);

                EditButton.IsEnabled = false;
                AddButton.IsEnabled = false;
                RemoveButton.IsEnabled = false;

                StudentHandler.RemoveStudent(ref students, selectedStudentNumber);
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmButton.Visibility = Visibility.Hidden;
            Student editedStudent = new Student { id = Convert.ToUInt32(idTextBox.Text), LastName = lastNameTextBox.Text, FristName = firstNameTextBox.Text, comment = commentTextBox.Text };
            StudentHandler.AddStudent(ref students, editedStudent);
            StudentHandler.Sort(ref students);

            FillingStudentList(StudentList, students);

            EditButton.Foreground = new SolidColorBrush(Colors.White);
            AddButton.Foreground = new SolidColorBrush(Colors.White);
            RemoveButton.Foreground = new SolidColorBrush(Colors.White);

            EditButton.IsEnabled = true;
            AddButton.IsEnabled = true;
            RemoveButton.IsEnabled = true;

            idTextBox.Clear();
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            commentTextBox.Clear();

            Initializer.Save(path, students);
        }

        private void AnyKeyPressed(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (ConfirmButton.Visibility == Visibility.Hidden)
                {
                    AddButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else
                {
                    ConfirmButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            }
            else if(e.Key == Key.Delete)
            {
                RemoveButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}