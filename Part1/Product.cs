using System.Xml.Serialization;
namespace Part1;
[Serializable]
public class Product
{
    public int Code { get; set; }
    public string Name { get; set; } = "";
    public string Manufacturer { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Product() { }
    public Product(int code, string name, string manufacturer, decimal price, int quantity)
    { Code=code; Name=name; Manufacturer=manufacturer; Price=price; Quantity=quantity; }
    public decimal BatchTotal() => Price * Quantity;
    public void IncreasePrice(decimal percent)
    { if (percent < 0) throw new ArgumentOutOfRangeException(nameof(percent)); Price *= 1 + percent / 100m; }
    public override string ToString() => $"Код: {Code}; Назва: {Name}; Виробник: {Manufacturer}; Ціна: {Price:F2}; Кількість: {Quantity}; Вартість партії: {BatchTotal():F2}";
}
