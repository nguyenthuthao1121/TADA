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
        try
        {
            return statusRepository.IdForUserConfirm();
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public List<StatusDto> GetStatuses()
    {
        try
        {
            return statusRepository.GetStatuses();
        }
        catch (Exception)
        {
            return new List<StatusDto>();
        }
        
    }
}
