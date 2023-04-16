using TADA.Dto.Provider;
using TADA.Repository;

namespace TADA.Service.Implement;

public class ProviderService : IProviderService
{
    private readonly IProviderRepository providerRepository;
    private readonly IAddressRepository addressRepository;
    public ProviderService(IProviderRepository providerRepository, IAddressRepository addressRepository)
    {
        this.providerRepository = providerRepository;
        this.addressRepository = addressRepository;
    }
    public List<ProviderManagementDto> GetAllProviders()
    {
        var list = new List<ProviderManagementDto>();
        var providers = providerRepository.GetAllProviders();
        foreach ( var provider in providers )
        {
            list.Add(new ProviderManagementDto
            {
                Id = provider.Id,
                Name = provider.Name,
                Address = addressRepository.GetAddressById(provider.AddressId)
            });
        }
        return list;
    }
}
