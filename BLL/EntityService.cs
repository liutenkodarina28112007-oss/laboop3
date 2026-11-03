using DAL;
namespace BLL;
public class EntityService
{
 private readonly EntityContext _context;public EntityService(EntityContext? context=null){_context=context??new EntityContext();}
 public List<Student> GetStudents(string file,string format)=>_context.Read<DAL.StudentEntity>(file,format).Select(ToModel).ToList();
 public void SaveStudents(string file,string format,List<Student> items){ValidateStudents(items);_context.Write(file,format,items.Select(ToEntity).ToList());}
 public void AddStudent(string file,string format,Student item){var all=GetStudents(file,format);if(all.Any(x=>x.StudentCard==item.StudentCard))throw new StudentDataException("Студент із таким квитком уже існує.");all.Add(item);SaveStudents(file,format,all);}
 public void DeleteStudent(string file,string format,string card){var all=GetStudents(file,format);if(all.RemoveAll(x=>x.StudentCard==card)==0)throw new StudentDataException("Студента не знайдено.");SaveStudents(file,format,all);}
 public int CountFirstYearFemaleDormStudents(string file,string format)=>GetStudents(file,format).Count(s=>s.Course==1&&s.Gender.Equals("ж",StringComparison.OrdinalIgnoreCase)&&s.LivesInDormitory);
 public List<Musician> GetMusicians(string file,string format)=>_context.Read<MusicianEntity>(file,format).Select(x=>new Musician{Name=x.Name,Instrument=x.Instrument,CanSkate=x.CanSkate}).ToList();
 public void SaveMusicians(string file,string format,List<Musician> x)=>_context.Write(file,format,x.Select(a=>new MusicianEntity{Name=a.Name,Instrument=a.Instrument,CanSkate=a.CanSkate}).ToList());
 public List<Pilot> GetPilots(string file,string format)=>_context.Read<PilotEntity>(file,format).Select(x=>new Pilot{Name=x.Name,LicenseNumber=x.LicenseNumber,CanSkate=x.CanSkate}).ToList();
 public void SavePilots(string file,string format,List<Pilot> x)=>_context.Write(file,format,x.Select(a=>new PilotEntity{Name=a.Name,LicenseNumber=a.LicenseNumber,CanSkate=a.CanSkate}).ToList());
 private static Student ToModel(StudentEntity x)=>new(){Surname=x.Surname,Name=x.Name,Course=x.Course,StudentCard=x.StudentCard,Gender=x.Gender,Residence=x.Residence};
 private static StudentEntity ToEntity(Student x)=>new(){Surname=x.Surname,Name=x.Name,Course=x.Course,StudentCard=x.StudentCard,Gender=x.Gender,Residence=x.Residence};
 private static void ValidateStudents(List<Student> a){if(a.Any(x=>string.IsNullOrWhiteSpace(x.Surname)||string.IsNullOrWhiteSpace(x.Name)||x.Course<1||x.Course>6||string.IsNullOrWhiteSpace(x.StudentCard)||!(x.Gender=="ж"||x.Gender=="ч")))throw new StudentDataException("Перевірте дані: курс 1–6, стать ж/ч, обов'язкові поля заповнені.");}
}
