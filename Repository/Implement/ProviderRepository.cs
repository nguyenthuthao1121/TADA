using TADA.Dto.Provider;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement;

public class ProviderRepository : IProviderRepository
{
    private readonly TadaContext context;
    public ProviderRepository(TadaContext context)
    {
        this.context = context;
    }
    public string GetProviderNameById(int id)
    {
        return context.Providers.Where(provider => provider.Id == id).Select(provider => provider.Name).FirstOrDefault();
    }
    public List<ProviderDto> GetAllProviders()
    {
        return context.Providers.Select(provider => new ProviderDto
        {
            Id = provider.Id,
            Name = provider.Name,
            AddressId = provider.AddressId
        }).ToList();
    }
    public Provider GetProviderByName(string providerName)
    {
        return context.Providers.Where(provider => provider.Name == providerName).FirstOrDefault();
    }

    public void AddProvider(ProviderDto provider)
    {
        context.Providers.Add(new Provider
        {
            Name = provider.Name,
            AddressId = provider.AddressId
        });
        context.SaveChanges();
    }
}
