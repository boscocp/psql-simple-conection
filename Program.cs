using System;

namespace PSQLConection
{
    class Program
    {
        static void Main(string[] args)
        {
            Models.Person person = new Models.Person();
            person.Name = "Amanda";
            person.BirthDate = DateTime.Today;
            person.CPF = 1234555;
            person.Income = 2000f;
            PSQLConection.DataAccess.IDataAccessProvider dbPerson = new PSQLConection.DataAccess.DataAccessProvider();
            Models.Person person2 = dbPerson.GetPersonSingleRecord(4);
            //dbPerson.GetPersonRecords();
            //dbPerson.AddPersonRecord(person);
            //dbPerson.DeletePersonRecord(person2);
            //dbPerson.UpdatePersonRecord(person2, person);
            Console.WriteLine(person2.Name);
            //AddPersonDB(person);
        }
    }
}
