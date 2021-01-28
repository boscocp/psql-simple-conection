using PSQLConection.Models;
using System;
using System.Collections.Generic;  
  
namespace PSQLConection.DataAccess  
{  
    public interface IDataAccessProvider  
    {  
        void AddPersonRecord(Person person);  
        void UpdatePersonRecord(Person person, Person newPerson);  
        void DeletePersonRecord(Person person);  
        Person GetPersonSingleRecord(Int64 id);  
        List<Person> GetPersonRecords();  
    }  
}  