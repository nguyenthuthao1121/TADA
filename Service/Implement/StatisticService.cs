namespace TADA.Service.Implement;

public class StatisticService : IStatisticService
{
    private readonly IOrderService orderService;
    private readonly ICustomerService customerService;
    private readonly IStaffService staffService;
    public StatisticService(IOrderService orderService, ICustomerService customerService, IStaffService staffService)
    {
        this.orderService = orderService;
        this.customerService = customerService;
        this.staffService = staffService;
    }

    public List<int> GetReportByYear(int year)
    {
        try
        {
            List<int> report = new List<int>();
            int revueneInCurrentYear = 0;
            int revueneInPreviousYear = 0;
            for (int month = 1; month <= 12; month++)
            {
                var revuene = orderService.RevueneOfMonth(month, year);
                report.Add(revuene);
                revueneInCurrentYear += revuene;
                revuene = orderService.RevueneOfMonth(month, year - 1);
                revueneInPreviousYear += revuene;
            }
            report.Add(revueneInCurrentYear);
            report.Add(revueneInPreviousYear);
            report.Add(orderService.GetNumOfOrdersByYear(year));
            report.Add(customerService.GetNumOfCustomersByYear(year));
            report.Add(staffService.GetNumOfStaffsByYear(year));
            return report;
        }
        catch(Exception)
        {
            return new List<int>();
        }
    }
}
