namespace BLL;
public class Student {public string Surname{get;set;}="";public string Name{get;set;}="";public int Course{get;set;}public string StudentCard{get;set;}="";public string Gender{get;set;}="";public string Residence{get;set;}="";public bool LivesInDormitory=>Residence.Contains('.');}
public class Musician {public string Name{get;set;}="";public string Instrument{get;set;}="";public bool CanSkate{get;set;}}
public class Pilot {public string Name{get;set;}="";public string LicenseNumber{get;set;}="";public bool CanSkate{get;set;}}
public class StudentDataException:Exception{public StudentDataException(string message):base(message){}}
