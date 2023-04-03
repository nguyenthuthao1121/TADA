using TADA.Repository;

namespace TADA.Service.Implement;
public class AdminService : IAdminService
{
    private readonly IAdminRepository adminRepository;
    public AdminService(IAdminRepository adminRepository)
    {
        this.adminRepository = adminRepository;
    }
    public string GetNameByAccountId(int id)
    {
        return adminRepository.GetNameByAccountId(id);
    }
}
