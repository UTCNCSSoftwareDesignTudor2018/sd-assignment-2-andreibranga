using Restaurant.Business.Models;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IReportsRepository
    {
        ReportModel GetReportForUser(int userId);
        void SaveReportForStudent(int userId);
        ReportModel GetUserLastReport(int userId);
    }
}