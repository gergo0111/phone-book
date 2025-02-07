using System.ComponentModel.DataAnnotations;

namespace phone_book.models;

public class PhoneBookEntry(string? phoneNumber = null, string? name = null)
{
    [Key, Phone, Required]
    public string PhoneNumber { get; set; } = phoneNumber ?? "";
    [Required, StringLength(30, MinimumLength = 3)]
    public string Name { get; set; } = name ?? "";

    public PhoneBookEntry() : this(null, null) { }

    public override string ToString()
    {
        return $"{Name};{PhoneNumber}";
    }

    public static PhoneBookEntry FromString(string str)
    {
        var parts = str.Split(";");
        return new PhoneBookEntry
        {
            Name = parts[0],
            PhoneNumber = parts[1]
        };
    }

    public static List<PhoneBookEntry> LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path).Close();
            return new List<PhoneBookEntry>();
        }
        return File.ReadAllLines(path).Select(FromString).ToList();
    }

    public static void SaveToFile(string path, List<PhoneBookEntry> data)
    {
        File.WriteAllLines(path, data.Select(x => x.ToString()));
    }
}