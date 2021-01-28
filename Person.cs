using System;

namespace PSQLConection.Models
{
    public class Person
    {
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Double Income { get; set; }
        public int CPF { get; set; }
    }
}