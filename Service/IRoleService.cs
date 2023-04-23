using TADA.Dto.Role;

namespace TADA.Service;

public interface IRoleService
{
    string GetRoleNameById(int roleId);
    List<RoleDto> GetAllRoles();
}
