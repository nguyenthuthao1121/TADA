using TADA.Dto.Provider;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IProviderRepository
{
    string GetProviderNameById(int id);
    List<ProviderDto> GetAllProviders();
    Provider GetProviderByName(string providerName);
    void AddProvider(ProviderDto provider);
}
