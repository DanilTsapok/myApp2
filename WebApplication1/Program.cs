var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Configuration
    .AddXmlFile("config-Microsoft.xml")
    .AddJsonFile("config-Apple.json")
    .AddIniFile("config-Google.ini")
    .AddJsonFile("about-me.json")
    .AddInMemoryCollection(new Dictionary<string, string>
    {
        {"",""}
    });
;

app.MapGet("/companies", (IConfiguration appConfig) =>
{
    var nameCompany = "";
    var numberWorkers = 0;
    IConfiguration  companyConfig = appConfig.GetSection("Company");
        foreach (var company in companyConfig.GetChildren())
        {
        var name = company.Key;
        var workers = Int32.Parse(company.GetSection("workers").Value);
        if (workers > numberWorkers)
        {
            nameCompany = name;
            numberWorkers=workers;
        }
        }
      return $"{nameCompany} - {numberWorkers}";
});
app.MapGet("/about-me", (IConfiguration appConfig ) =>
{
    IConfiguration person = appConfig.GetSection("Person");
    var name = person.GetSection("name").Value;
    var surname = person.GetSection("surname").Value;
    var age = person.GetSection("age").Value;

    return $" My name is {name} \n My surname is {surname}. \n I am {age} years old";
    
});
app.Run();
