using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
namespace Part1;
public interface IDataSerializer<T> { void Save(T data, string path); T Load(string path); }
public sealed class JsonProductSerializer : IDataSerializer<List<Product>>
{
 public void Save(List<Product> data,string path)=>File.WriteAllText(path,JsonSerializer.Serialize(data,new JsonSerializerOptions{WriteIndented=true}));
 public List<Product> Load(string path)=>JsonSerializer.Deserialize<List<Product>>(File.ReadAllText(path)) ?? new();
}
public sealed class XmlProductSerializer : IDataSerializer<List<Product>>
{
 public void Save(List<Product> data,string path){using var fs=File.Create(path);new XmlSerializer(typeof(List<Product>)).Serialize(fs,data);}
 public List<Product> Load(string path){using var fs=File.OpenRead(path);return (List<Product>)(new XmlSerializer(typeof(List<Product>)).Deserialize(fs) ?? new List<Product>());}
}
// Binary format: explicit primitive fields written with BinaryWriter (portable alternative to obsolete BinaryFormatter).
public sealed class BinaryProductSerializer : IDataSerializer<List<Product>>
{
 public void Save(List<Product> data,string path){using var w=new BinaryWriter(File.Create(path),Encoding.UTF8);w.Write(data.Count);foreach(var p in data){w.Write(p.Code);w.Write(p.Name);w.Write(p.Manufacturer);w.Write(p.Price);w.Write(p.Quantity);}}
 public List<Product> Load(string path){using var r=new BinaryReader(File.OpenRead(path),Encoding.UTF8);var n=r.ReadInt32();if(n<0||n>100000)throw new InvalidDataException("Некоректна кількість записів.");var a=new List<Product>();for(int i=0;i<n;i++)a.Add(new Product(r.ReadInt32(),r.ReadString(),r.ReadString(),r.ReadDecimal(),r.ReadInt32()));return a;}
}
// User-defined CSV-like serialization.
public sealed class CustomProductSerializer : IDataSerializer<List<Product>>
{
 public void Save(List<Product> data,string path){using var w=new StreamWriter(path,false,Encoding.UTF8);foreach(var p in data)w.WriteLine($"{p.Code}|{p.Name.Replace("|","/")}|{p.Manufacturer.Replace("|","/")}|{p.Price.ToString(System.Globalization.CultureInfo.InvariantCulture)}|{p.Quantity}");}
 public List<Product> Load(string path){var a=new List<Product>();foreach(var line in File.ReadLines(path)){var x=line.Split('|');if(x.Length!=5)throw new InvalidDataException("Помилка формату товару.");a.Add(new Product(int.Parse(x[0]),x[1],x[2],decimal.Parse(x[3],System.Globalization.CultureInfo.InvariantCulture),int.Parse(x[4])));}return a;}
}
