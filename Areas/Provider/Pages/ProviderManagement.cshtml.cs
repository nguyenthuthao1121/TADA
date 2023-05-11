using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Category;
using TADA.Dto.Provider;
using TADA.Middleware;
using TADA.Service;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class ProviderManagementModel : PageModel
{
    private readonly IProviderService providerService;
    public List<ProviderManagementDto> Providers { get; set; }
    public const int ITEMS_PER_PAGE = 5;
    public int countPages { get; set; }
    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int currentPage { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SearchQuery { get; set; }

    public ProviderManagementModel(IProviderService providerService)
    {
        this.providerService = providerService;
    }

    public void OnGet()
    {
        var providers = providerService.GetProviders(SearchQuery);
        int total = providers.Count();
        countPages = (int)Math.Ceiling((double)total / ITEMS_PER_PAGE);
        if (currentPage < 1)
        {
            currentPage = 1;
        }
        if (currentPage > countPages)
        {
            currentPage = countPages;
        }
        Providers = providers.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
    }
}
