using TADA.Dto.Provider;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IProviderRepository
{
    string GetProviderNameById(int id);
    List<ProviderDto> GetAllProviders();
    List<ProviderDto> GetProviders(string search);

    Provider GetProviderByName(string providerName);
    void AddProvider(ProviderDto provider);
}
