using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Social_Media_Links.Models;

namespace Social_Media_Links.Controllers;

public class HomeController(IOptions<SocialMediaLinksOptions> socialMediaLinksOptions) : Controller
{
    private readonly SocialMediaLinksOptions _socialMediaLinksOptions = socialMediaLinksOptions.Value;

    [Route("/")]
    public IActionResult Index()
    {
        ViewBag.SocialMediaLinks = _socialMediaLinksOptions;
        return View();
    }
}