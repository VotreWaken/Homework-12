﻿using System.Collections;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

[Serializable]
[DataContract]
struct Poetry
{

    public string PoetryName_ { get; set; }
    public string AuthorName_ { get; set; }
    public DateTimeOffset YearOfWriting_ { get; set; }
    public string PoetryText_ { get; set; }
    public string PoetryTopic_ { get; set; }
}
[Serializable]
[DataContract]

class PoetryList : IEnumerable
{
    [DataMember]
    private List<Poetry>? Poetrys_;
    public PoetryList() => Poetrys_ = new List<Poetry>();
    public void AddPoetry()
    {
        Console.WriteLine("\nEnter Poetry Name: ");
        string? title = Console.ReadLine();

        Console.WriteLine("Enter Poetry Author: ");
        string? author = Console.ReadLine();

        Console.Write("Введите год написания стиха (year-month-day): ");
        DateTimeOffset yearWritten;
        while (!DateTimeOffset.
            TryParse(Console.ReadLine(), out yearWritten))
        {
            Console.WriteLine("Error, Try Again In Format: " +
                "'yyyy-MM-dd': ");
        }

        Console.WriteLine("Enter Text: ");
        string? text = Console.ReadLine();

        Console.WriteLine("Enter Poetry Topic: ");
        string? topic = Console.ReadLine();

        Poetry newPoem = new Poetry
        {
            PoetryName_ = title,
            AuthorName_ = author,
            YearOfWriting_ = yearWritten,
            PoetryText_ = text,
            PoetryTopic_ = topic
        };

        Poetrys_.Add(newPoem);
    }
    public void Output()
    {
        if (Poetrys_.Count == 0)
        {
            Console.WriteLine("Empty");
        }
        else
        {
            foreach (var item in Poetrys_)
            {
                Console.WriteLine($"Name: {item.PoetryName_}");
                Console.WriteLine($"Author: {item.AuthorName_}");
                Console.WriteLine($"Year Of Writing:" +
                    $" {item.YearOfWriting_.ToString("yyyy-MM-dd")}");
                Console.WriteLine($"Topic Text: {string.Join("\n", item.PoetryText_)}");
                Console.WriteLine($"Topic: {item.PoetryTopic_}");
                Console.WriteLine();
            }
        }
    }
    public void AddPoetry(Poetry PoetryAdd)
    {
        Poetrys_.Add(PoetryAdd);
    }
    public void RemovePoetry()
    {
        Console.Write("Enter Name of Poetry: ");
        string todelete = Console.ReadLine();

        foreach (var item in Poetrys_)
        {
            if (item.PoetryName_ == todelete)
            {
                Poetrys_.Remove(item);
                return;
            }
        }
    }
    public void EditPoetry()
    {
        Console.Write("Enter Name of Poetry: ");
        string toupdate = Console.ReadLine();

        foreach (var item in Poetrys_)
        {
            if (item.PoetryName_ == toupdate)
            {
                Poetrys_.Remove(item);
                Console.Write("Enter New Data of Poetry: ");
                AddPoetry();
                return;
            }
        }
    }
    public void SearchPoetry()
    {
        Console.WriteLine("1. By Name");
        Console.WriteLine("2. By Author");
        Console.WriteLine("3. By Year Of Writing");
        Console.WriteLine("4. By Topic");
        Console.WriteLine("5. Return ");
        string searchOption = Console.ReadLine();

        switch (searchOption)
        {
            case "1":
                Console.Write("Enter Name of Poetry: ");
                string title = Console.ReadLine();
                foreach (var item in Poetrys_)
                {
                    if (item.PoetryName_ == title)
                    {
                        Console.Write($"Find Poetry Author: {item.AuthorName_} ");
                    }
                }
                break;
            case "2":
                Console.Write("Enter Author of Poetry: ");
                string author = Console.ReadLine();
                foreach (var item in Poetrys_)
                {
                    if (item.AuthorName_ == author)
                    {
                        Console.Write($"Find Poetry Name: {item.PoetryName_} ");
                    }
                }
                break;
            case "3":
                Console.Write("Enter Year Of Writing: ");
                DateTimeOffset yearWritten;
                while (!DateTimeOffset.
                   TryParseExact(Console.ReadLine(),
                   "yyyy-MM-dd", CultureInfo.InvariantCulture,
                   DateTimeStyles.None, out yearWritten))
                {
                    Console.WriteLine("Error, Try Again In Format: " +
                        "'yyyy-MM-dd': ");
                }
                foreach (var item in Poetrys_)
                {
                    if (item.YearOfWriting_ == yearWritten)
                    {
                        Console.Write($"Find Poetry Name: {item.PoetryName_} ");
                    }
                }
                break;
            case "4":
                Console.Write("Enter Topic Name: ");
                string topic = Console.ReadLine();
                foreach (var item in Poetrys_)
                {
                    if (item.PoetryTopic_ == topic)
                    {
                        Console.Write($"Find Poetry Name: {item.PoetryName_} ");
                    }
                }
                break;
            case "5":
                return;
            default:
                Console.Write("Error Choice: ");
                break;
        }
    }
    async public void SaveToFile()
    {
        string fileName = "Poetrys.json";
        using FileStream createStream = File.Create(fileName);
        await JsonSerializer.SerializeAsync(createStream, Poetrys_);
        await createStream.DisposeAsync();
    }
    public async void LoadFromFile()
    {
        string fileName = "Poetrys.json";
        using FileStream openStream = File.OpenRead(fileName);
        string jsonString = File.ReadAllText(fileName);
        List<Poetry>? PoetrysFromFile =
            await JsonSerializer.DeserializeAsync<List<Poetry>>(openStream);
        Poetrys_ = PoetrysFromFile;
    }
    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < Poetrys_.Count; i++)
            yield return Poetrys_[i];
    }
}
class GenerateReports
{
    static public void ReportByAuthorName(PoetryList Poetrys)
    {
        Console.WriteLine("Enter Author Name of Poetry to get report:");
        string author = Console.ReadLine();

        PoetryList ContainsPoetry = new PoetryList();
        foreach (Poetry item in Poetrys)
        {
            if (item.AuthorName_ == author)
            {
                ContainsPoetry.AddPoetry(item);
            }
        }
        ContainsPoetry.Output();
    }

    static public void ReportByTitle(PoetryList Poetrys)
    {
        Console.WriteLine("Enter Title of Poetry to get report:");
        string title = Console.ReadLine();

        PoetryList ContainsPoetry = new PoetryList();
        foreach (Poetry item in Poetrys)
        {
            if (item.PoetryName_ == title)
            {
                ContainsPoetry.AddPoetry(item);
            }
        }
        ContainsPoetry.Output();
    }

    static public void ReportByTopic(PoetryList Poetrys)
    {
        Console.WriteLine("Enter Topic of Poetry to get report:");
        string topic = Console.ReadLine();

        PoetryList ContainsPoetry = new PoetryList();
        foreach (Poetry item in Poetrys)
        {
            if (item.PoetryTopic_ == topic)
            {
                ContainsPoetry.AddPoetry(item);
            }
        }
        ContainsPoetry.Output();
    }

    static public void ReportByWord(PoetryList Poetrys)
    {
        Console.WriteLine("Enter Word of Poetry to get report:");
        string word = Console.ReadLine();

        PoetryList ContainsPoetry = new PoetryList();
        foreach (Poetry item in Poetrys)
        {
            if (item.PoetryText_.Contains(word))
            {
                ContainsPoetry.AddPoetry(item);
            }
        }
        ContainsPoetry.Output();
    }

    static public void ReportByYear(PoetryList Poetrys)
    {
        Console.WriteLine("Enter Poetry Year Of Writing to get report:");
        int title;
        if (int.TryParse(Console.ReadLine(), out title))
        {
            PoetryList ContainsPoetry = new PoetryList();

            foreach (Poetry item in Poetrys)
            {
                if (item.YearOfWriting_.Year == title)
                {
                    ContainsPoetry.AddPoetry(item);
                }
            }
            ContainsPoetry.Output();
        }
    }

    static public void ReportByTextSize(PoetryList Poetrys)
    {
        Console.WriteLine("Enter Symbol Length of Poetry to get report:");
        int title;
        if (int.TryParse(Console.ReadLine(), out title))
        {
            PoetryList ContainsPoetry = new PoetryList();

            foreach (Poetry item in Poetrys)
            {
                if (item.PoetryText_.Length == title)
                {
                    ContainsPoetry.AddPoetry(item);
                }
            }
            ContainsPoetry.Output();
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        PoetryList Poetry = new PoetryList();

        while (true)
        {
            Console.WriteLine("\n1. Add Poetry");
            Console.WriteLine("2. Remove Poetry");
            Console.WriteLine("3. Edit Poetry");
            Console.WriteLine("4. Search Poetry");
            Console.WriteLine("5. Show Poetrys");
            Console.WriteLine("6. Save Poetry");
            Console.WriteLine("7. Load Poetry");
            Console.WriteLine("8. Report By Poetry Name");
            Console.WriteLine("9. Report By Poetry Author");
            Console.WriteLine("10. Report By Poetry Topic");
            Console.WriteLine("11. Report By Word In Poetry");
            Console.WriteLine("12. Report By Poetry Writing Year");
            Console.WriteLine("13. Report By Poetry Symbol Length");
            Console.WriteLine("14. Exit");
            string UserChoice = Console.ReadLine();
            switch (UserChoice)
            {
                case "1":
                    Poetry.AddPoetry();
                    break;
                case "2":
                    Poetry.RemovePoetry();
                    break;
                case "3":
                    Poetry.EditPoetry();
                    break;
                case "4":
                    Poetry.SearchPoetry();
                    break;
                case "5":
                    Poetry.Output();
                    break;
                case "6":
                    Poetry.SaveToFile();
                    break;
                case "7":
                    Poetry.LoadFromFile();
                    break;
                case "8":
                    GenerateReports.ReportByTitle(Poetry);
                    break;
                case "9":
                    GenerateReports.ReportByAuthorName(Poetry);
                    break;
                case "10":
                    GenerateReports.ReportByTopic(Poetry);
                    break;
                case "11":
                    GenerateReports.ReportByWord(Poetry);
                    break;
                case "12":
                    GenerateReports.ReportByYear(Poetry);
                    break;
                case "13":
                    GenerateReports.ReportByTextSize(Poetry);
                    break;
                case "14":
                    return;
                default: Console.WriteLine("Error Input"); break;
            }
        }
    }

}