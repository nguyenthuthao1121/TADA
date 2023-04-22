using TADA.Dto.Provider;

namespace TADA.Service;

public interface IProviderService
{
    List<ProviderManagementDto> GetAllProviders();
    bool AddProvider(AddProviderDto provider);
}
