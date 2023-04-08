namespace TADA.Service;

public interface IAddressService
{
    string GetAddressByIdAndType(int id, bool type);
    string GetAddressById(int id);
}
