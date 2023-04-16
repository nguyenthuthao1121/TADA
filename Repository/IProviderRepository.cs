using TADA.Dto.Provider;

namespace TADA.Repository;

public interface IProviderRepository
{
    string GetProviderNameById(int id);
    List<ProviderDto> GetAllProviders();
}
