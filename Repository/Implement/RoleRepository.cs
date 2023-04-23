using TADA.Dto.Role;
using TADA.Model;

namespace TADA.Repository.Implement;

public class RoleRepository : IRoleRepository
{
    private readonly TadaContext context;
    public RoleRepository(TadaContext context)
    {
        this.context = context;
    }

    public List<RoleDto> GetAllRoles()
    {
        return context.Roles.Select(role => new RoleDto
        {
            RoleId = role.Id,
            RoleName = role.Name,
        }).ToList();
    }

    public string GetRoleNameById(int roleId)
    {
        return context.Roles.Where(role => role.Id == roleId).Select(role => role.Name).FirstOrDefault();
    }
}
