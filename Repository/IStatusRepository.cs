using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Dto.Status;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IStatusRepository
{
    int IdForUserConfirm();
    List<StatusDto> GetStatuses();
    string GetStatusById(int statusId);
}
