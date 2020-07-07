using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Models;

namespace OdeToFood.Pages {
  public class ContactPageModel : PageModel {

    [BindProperty]
    public Contact Contact { get; set; } = new Contact ();
    public void OnGet () {

    }

    public void OnPost () {
      Console.WriteLine (Contact);
      Console.WriteLine (ModelState);

    }
  }
}