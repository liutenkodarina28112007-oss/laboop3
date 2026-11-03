namespace DAL;
public class StudentEntity { public string Surname{get;set;}=""; public string Name{get;set;}=""; public int Course{get;set;} public string StudentCard{get;set;}=""; public string Gender{get;set;}=""; public string Residence{get;set;}=""; public bool LivesInDormitory=>Residence.Contains('.'); public StudentEntity(){} }
public class MusicianEntity { public string Name{get;set;}=""; public string Instrument{get;set;}=""; public bool CanSkate{get;set;} public MusicianEntity(){} }
public class PilotEntity { public string Name{get;set;}=""; public string LicenseNumber{get;set;}=""; public bool CanSkate{get;set;} public PilotEntity(){} }
