using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Category;
using TADA.Dto.Provider;
using TADA.Middleware;
using TADA.Service;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên kinh doanh")]
public class ProviderManagementModel : PageModel
{
    private readonly IProviderService providerService;
    public List<ProviderManagementDto> Providers { get; set; }
    public ProviderManagementModel(IProviderService providerService)
    {
        this.providerService = providerService;
    }

    public void OnGet()
    {
        Providers = providerService.GetAllProviders();
    }
}
