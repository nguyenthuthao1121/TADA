using TADA.Dto.Role;
using TADA.Dto.Status;
using TADA.Repository;

namespace TADA.Service.Implement;

public class StatusService : IStatusService
{
    private readonly IStatusRepository statusRepository;
    public StatusService(IStatusRepository statusRepository)
    {
        this.statusRepository = statusRepository;
    }

    public int IdForUserConfirm()
    {
        return statusRepository.IdForUserConfirm();
    }
    public List<StatusDto> GetStatuses()
    {
        return statusRepository.GetStatuses();
    }
}
