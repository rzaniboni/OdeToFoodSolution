using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Utils;

namespace OdeToFood.Pages.ConfigPage {
  public class ListModel : PageModel {
    private readonly IConfigurationRoot ConfigRoot;
    private readonly IConfiguration Configuration;

    public ListModel (IConfiguration configRoot) {
      this.ConfigRoot = (IConfigurationRoot) configRoot;
      Configuration = configRoot;
    }
    public ContentResult OnGet (int? id) {

      var idLocal = 0;
      if (id.HasValue) {
        idLocal = id.Value;
      }
      string str = $"id: {idLocal}\n\n";
      foreach (var provider in ConfigRoot.Providers.ToList ()) {
        str += provider.ToString () + "\n";
      }
      var title = Configuration["Position:Title"];
      str += $"\nPosition: Title: {title}";

      var positionOptions = new PositionOptions ();
      Configuration.GetSection (PositionOptions.Position).Bind (positionOptions);

      str +=
        $"\nTitle: {positionOptions.Title} \n" +
        $"Name: {positionOptions.Name}";

      return Content (str);
    }
  }
}