﻿using TADA.Model;

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
}