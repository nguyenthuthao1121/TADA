using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TADA.Dto.Provider;
using TADA.Service;
using TADA.Middleware;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class AddProviderModel : PageModel
{
    private readonly IProviderService providerService;
    [BindProperty]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    public string ProviderName { get; set; }
    [BindProperty]
    [Required(ErrorMessage = "Vui lòng chọn xã/ phường !")]
    public int WardId { get; set; }
    [BindProperty]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    public string Street { get; set; }
    public string Message { get; set; }
    public AddProviderModel(IProviderService providerService)
    {
        this.providerService = providerService;
    }

    public IActionResult OnPost()
    {
        if (ProviderName == null || Street == null || WardId == 0)
        {
            Message = "Bạn cần nhập đầy đủ các thông tin của nhà cung cấp";
            return Page();
        }
        else
        {
            AddProviderDto provider = new AddProviderDto()
            {
                Name = ProviderName,
                Street = Street,
                WardId = Convert.ToInt32(WardId)
            };
            if (providerService.AddProvider(provider))
            {

                return RedirectToPage("/ProviderManagement", new { area = "Provider" });
            }
            else
            {
                Message = "Tên nhà cung cấp đã tồn tại";
                return Page();
            }
        }
    }
}
