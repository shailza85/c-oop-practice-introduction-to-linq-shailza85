using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ_Practice_Intro
{
    class Person
    {
        public enum GenderValue
        {
            Male,
            Female,
            NotStated
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderValue Gender { get; set; } 

    }
}
