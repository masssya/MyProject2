using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection(); // создаем коллекцию сервисов
services.AddTransient < IPurchaseReport, PurchaseReport > (); // добавляем сервисы в эту колеекцию
services.AddTransient<Reporter>();

using var servisProvider  = services.BuildServiceProvider (); // билдим полученную коллекцию
IPurchaseReport? rep = servisProvider.GetRequiredService<IPurchaseReport> (); // первый способ получения сервиса

Reporter? reporter = servisProvider.GetService<Reporter> (); 

rep?.Report("Молоко", 73); // использование певрого варианта

reporter?.ShowInformation("Ананасик", 300); // использование второго варианта
interface IPurchaseReport
{
    void Report (string name, double price);
}

class PurchaseReport : IPurchaseReport
{
    public void Report(string name, double price) => Console.WriteLine($"{name} \n{price} рубля \n{DateTime.Now}\n");
}

class Reporter
{
    IPurchaseReport? reportSer;
    public Reporter (IPurchaseReport? reportSer) => this.reportSer = reportSer;
    public void ShowInformation (string name, double price) => reportSer?.Report(name, price);
}