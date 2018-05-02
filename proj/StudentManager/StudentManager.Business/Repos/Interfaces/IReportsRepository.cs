using Restaurant.Business.Models;

namespace Restaurant.Business.Repos.Repositories
{
    public interface IReportsRepository
    {
        ReportModel GetReportForUser(int userId);
        void SaveReportForStudent(int userId);
        ReportModel GetUserLastReport(int userId);
    }
}