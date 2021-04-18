using System;
using System.Collections.Generic;
using System.Text;

namespace StudentList
{
    class Student
    {
        private string firstName;
        private string lastName;
        public uint id;
        public string comment;
        public string FristName
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

    }
}
