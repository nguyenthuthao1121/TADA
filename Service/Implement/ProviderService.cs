using TADA.Dto.Provider;
using TADA.Model.Entity;
using TADA.Repository;
using TADA.Repository.Implement;
using TADA.Utilities;

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
        try
        {
            var list = new List<ProviderManagementDto>();
            var providers = providerRepository.GetAllProviders();
            foreach (var provider in providers)
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
        catch (Exception)
        {
            return new List<ProviderManagementDto>();
        }
    }
    public List<ProviderManagementDto> GetProviders(string search)
    {
        try
        {
            var list = new List<ProviderManagementDto>();
            var providers = providerRepository.GetProviders();
            if (string.IsNullOrWhiteSpace(search))
            {
                foreach (var provider in providers)
                {
                    list.Add(new ProviderManagementDto
                    {
                        Id = provider.Id,
                        Name = provider.Name,
                        Address = addressRepository.GetAddressById(provider.AddressId)
                    });
                }
            }
            else
            {
                foreach (var provider in providers)
                {
                    if ((UIHelper.RemoveUnicodeSymbol(provider.Name)).Contains(UIHelper.RemoveUnicodeSymbol(search)))
                    {
                        list.Add(new ProviderManagementDto
                        {
                            Id = provider.Id,
                            Name = provider.Name,
                            Address = addressRepository.GetAddressById(provider.AddressId)
                        });
                    }
                }
            }
            return list;
        }
        catch (Exception)
        {
            return new List<ProviderManagementDto>();
        }
        
    }
    public bool AddProvider(AddProviderDto provider)
    {
        try
        {
            if (providerRepository.GetProviderByName(provider.Name) == null)
            {
                addressRepository.AddAddress(provider.Street, provider.WardId);
                providerRepository.AddProvider(new ProviderDto
                {
                    Name = provider.Name,
                    AddressId = addressRepository.GetLastId()
                });
                return true;
            }
            else
            {
                return false;
            }
        }
        catch(Exception)
        {
            return false;
        }
    }
}
