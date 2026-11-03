using BLL;
namespace Menu;
public class Menu
{
 private readonly EntityService _service=new();private string _format="json",_file="students";
 public void MainMenu(){while(true){Console.WriteLine("\n=== ВАРІАНТ 3: ОБЛІК СТУДЕНТІВ ===\n1. Додати студента\n2. Показати студентів\n3. Видалити студента\n4. Порахувати студенток 1 курсу в гуртожитку\n5. Змінити формат/ім'я файлу\n6. Робота з Musician\n7. Робота з Pilot\n0. Вихід");Console.Write("Ваш вибір: ");var c=Console.ReadLine();try{switch(c){case "1":AddStudent();break;case "2":ShowStudents();break;case "3":DeleteStudent();break;case "4":Console.WriteLine($"Кількість: {_service.CountFirstYearFemaleDormStudents(_file,_format)}");break;case "5":SetStorage();break;case "6":MusicianMenu();break;case "7":PilotMenu();break;case "0":return;default:Console.WriteLine("Невірний пункт.");break;}}catch(Exception e){Console.WriteLine("Помилка: "+e.Message);}}}
 private void AddStudent(){var x=new Student{Surname=Ask("Прізвище"),Name=Ask("Ім'я"),Course=int.Parse(Ask("Курс")),StudentCard=Ask("Студентський квиток"),Gender=Ask("Стать (ж/ч)"),Residence=Ask("Місце проживання (гуртожиток: номер.кімната; інакше адреса)")};_service.AddStudent(_file,_format,x);Console.WriteLine("Додано.");}
 private void ShowStudents(){var a=_service.GetStudents(_file,_format);if(a.Count==0)Console.WriteLine("Записів немає.");foreach(var x in a)Console.WriteLine($"{x.Surname} {x.Name}, курс {x.Course}, квиток {x.StudentCard}, стать {x.Gender}, проживання {x.Residence}");}
 private void DeleteStudent(){_service.DeleteStudent(_file,_format,Ask("Номер студентського квитка"));Console.WriteLine("Видалено.");}
 private void SetStorage(){_file=Ask("Шлях та ім'я файлу без розширення (наприклад data/students)");var f=Ask("Формат (binary/xml/json/custom)").ToLower();if(!new[]{"binary","xml","json","custom"}.Contains(f))throw new ArgumentException("Непідтримуваний формат.");_format=f;Console.WriteLine($"Сховище: {_file}.{_format}");}
 private void MusicianMenu(){var a=_service.GetMusicians(_file+"_musicians",_format);a.Add(new Musician{Name=Ask("Ім'я музиканта"),Instrument=Ask("Інструмент"),CanSkate=Ask("Катається на ковзанах? (так/ні)").ToLower()=="так"});_service.SaveMusicians(_file+"_musicians",_format,a);Console.WriteLine("Музиканта збережено.");}
 private void PilotMenu(){var a=_service.GetPilots(_file+"_pilots",_format);a.Add(new Pilot{Name=Ask("Ім'я пілота"),LicenseNumber=Ask("Номер ліцензії"),CanSkate=Ask("Катається на ковзанах? (так/ні)").ToLower()=="так"});_service.SavePilots(_file+"_pilots",_format,a);Console.WriteLine("Пілота збережено.");}
 private static string Ask(string label){Console.Write(label+": ");return Console.ReadLine()??"";}
}
