using TADA.Dto.Role;
using TADA.Dto.Status;

namespace TADA.Service;

public interface IStatusService
{
    int IdForUserConfirm();
    List<StatusDto> GetStatuses();
}
