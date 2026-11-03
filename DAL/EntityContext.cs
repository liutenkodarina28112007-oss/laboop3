namespace DAL;
public class EntityContext
{
 private readonly string _directory;
 public EntityContext(string directory="data"){_directory=Path.GetFullPath(directory,AppContext.BaseDirectory);Directory.CreateDirectory(_directory);}
 private IDataProvider<T> Provider<T>(string type)=>type.ToLower() switch{"json"=>new JsonProvider<T>(),"xml"=>new XmlProvider<T>(),"binary"=>new BinaryProvider<T>(),"custom"=>new CustomProvider<T>(),_=>throw new ArgumentException("Невідомий тип серіалізації.")};
 private string PathFor<T>(string file,string type)=>Path.Combine(_directory,$"{file}.{type.ToLower()}");
 public List<T> Read<T>(string file,string type){var p=PathFor<T>(file,type);return File.Exists(p)?Provider<T>(type).Load(p):new List<T>();}
 public void Write<T>(string file,string type,List<T> data){var p=PathFor<T>(file,type);var dir=Path.GetDirectoryName(p);if(!string.IsNullOrEmpty(dir))Directory.CreateDirectory(dir);Provider<T>(type).Save(data,p);}
 public void Delete<T>(string file,string type){var p=PathFor<T>(file,type);if(File.Exists(p))File.Delete(p);}
}
