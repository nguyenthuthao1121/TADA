using TADA.Dto.Role;

namespace TADA.Repository;

public interface IRoleRepository
{
    string GetRoleNameById(int roleId);
    List<RoleDto> GetAllRoles();
}
