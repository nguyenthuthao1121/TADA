using TADA.Dto.Role;
using TADA.Repository;

namespace TADA.Service.Implement;

public class RoleService : IRoleService
{
    private readonly IRoleRepository roleRepository;
    public RoleService(IRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public string GetRoleNameById(int roleId)
    {
        return roleRepository.GetRoleNameById(roleId);    
    }

    public List<RoleDto> GetAllRoles()
    {
        return roleRepository.GetAllRoles();
    }
}
