using TADA.Model;

namespace TADA.Repository.Implement;

public class AdminRepository : IAdminRepository
{
    private readonly TadaContext context;
    public AdminRepository(TadaContext context)
    {
        this.context = context;
    }
    public string GetNameByAccountId(int id)
    {
        return context.Admins.Where(admin => admin.AccountId == id).Select(admin => admin.Name).FirstOrDefault();
    }
}
