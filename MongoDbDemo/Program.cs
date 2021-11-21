// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Text;
using System.Text.Json;
using MongoDB.Data;
using MongoDB.Model;

MongoCrud db = new MongoCrud("AddressBook");
var table = "Users";

Console.WriteLine("Para inserir digite 1");
Console.WriteLine("Para ler todos digite 2");
Console.WriteLine("Para ler somente um pelo Id digite 3");
Console.WriteLine("Para ler somente um pelo Nome digite 4");
Console.WriteLine("Para Atualizar o registro digite 5");
Console.WriteLine("Para Apagar o registro digite 6");
Console.WriteLine("Para Ler somente os Nomes digite 7");
var digitado = Console.ReadKey();
Console.WriteLine($"Foi digitado: {digitado.KeyChar}");
if (digitado.KeyChar == '1')
{
    var address = new Address(){
        StreetAdrress="Rua Andre Rocco",
        City="Mandaguaçu",
        State="PR",
        ZipCode="87160-000"
    };

    var person = new Person(){
        Id = Guid.NewGuid(),
        FirstName = "Cristiane", 
        LastName = "Gonzalez",
        Email = "cristiane.gonzalez@skyneti.com.br",
        PrimaryAdress = address};

    db.InsertRecord(table, person);
}
else if (digitado.KeyChar == '2')
{
    var recs = db.LoadRecords<Person>(table);

    foreach (var item in recs)
    {
        PrintPerson(item);
    }

    Console.WriteLine("Leitura Concluída");

}
else if (digitado.KeyChar == '3')
{
    Console.WriteLine("Digite o Id");
    var id = Console.ReadLine();

    if (id != null)
    {
        var person = db.LoadRecordById<Person>(table, new Guid(id));
        if (person != null)
            PrintPerson(person);
    }

}
else if (digitado.KeyChar == '4')
{
    Console.WriteLine("Digite o Primeiro Nome:");
    var firstName = Console.ReadLine();
    Console.WriteLine("Digite o Ultimo Nome:");
    var lastName = Console.ReadLine();

    if (firstName != null && lastName != null)
    {
        var recs = db.LoadRecordByName<Person>(table, firstName, lastName);
        if (recs != null && recs.Any())
        {
            foreach (var item in recs)
            {
                PrintPerson(item);
            }
        }
        else
            Console.WriteLine("Não foram encontrado registros");
    }

}
else if (digitado.KeyChar == '5')
{
    var person = db.LoadRecordById<Person>(table, new Guid("61e419b0-c842-4bf9-979f-ca21f3e6444f"));
    person.DateOfBirth = DateTime.Now;
    db.UpsertRecord<Person>(table, person.Id, person);
}
else if (digitado.KeyChar == '6')
{
    Console.WriteLine("Digite o Id");
    var id = Console.ReadLine();

    if (id != null)
    {
        db.DeleteRecord<Person>(table, new Guid(id));
    }

}
else if (digitado.KeyChar == '7')
{
    var recs = db.LoadRecords<Name>(table);

    foreach (var item in recs)
    {
        Console.WriteLine($"{item.FirstName} {item.LastName}");
    }

    Console.WriteLine("Leitura Concluída");

}
//61e419b0-c842-4bf9-979f-ca21f3e6444f: Cristiane Gonzalez

void PrintPerson(Person item)
{
    Console.WriteLine($"{item.Id}: {item.FirstName} {item.LastName}");

    if (item.PrimaryAdress != null)
        Console.WriteLine($"--> City: {item.PrimaryAdress.City}");
    Console.WriteLine();
}

//Console.ReadLine();
