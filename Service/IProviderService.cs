using TADA.Dto.Provider;

namespace TADA.Service;

public interface IProviderService
{
    List<ProviderManagementDto> GetAllProviders();

    List<ProviderManagementDto> GetProviders(string search);
    bool AddProvider(AddProviderDto provider);
}
