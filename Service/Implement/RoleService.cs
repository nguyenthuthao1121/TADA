using TADA.Dto.Role;
using TADA.Model.Entity;
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
        try
        {
            return roleRepository.GetRoleNameById(roleId);
        }    
        catch (Exception)
        {
            return null;
        }
    }

    public List<RoleDto> GetAllRoles()
    {
        try
        {
            return roleRepository.GetAllRoles();
        }
        catch (Exception)
        {
            return new List<RoleDto>();
        }
        
    }
}
