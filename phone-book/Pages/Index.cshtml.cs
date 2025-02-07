using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using phone_book.models;

namespace phone_book.Pages;

public class IndexModel : PageModel
{
    private const string filePath = "book/phonebook.csv";
    public List<PhoneBookEntry> PhoneBook { get; set; } = new();
    [BindProperty]
    public PhoneBookEntry Entry { get; set; } = new();

    public void OnGet()
    {
        PhoneBook = PhoneBookEntry.LoadFromFile(filePath);
    }

    public IActionResult OnPost() {
        PhoneBook = PhoneBookEntry.LoadFromFile(filePath);
        if(PhoneBook.Any(x => x.PhoneNumber == Entry.PhoneNumber)) {
            ModelState.AddModelError("Entry.PhoneNumber", "Phone number already exists");
            return Page();
        }

        if(!ModelState.IsValid) {
            return Page();
        }

        PhoneBook.Add(Entry);
        PhoneBookEntry.SaveToFile(filePath, PhoneBook);
        return Page();
    }
}
