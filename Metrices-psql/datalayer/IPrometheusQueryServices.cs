using Metrices_psql.Models;

namespace Metrices_psql.datalayer
{
    public interface IPrometheusQueryServices
    {
        //post data
        void TotalEmployesIncBYDepartment(Employes employee);
        void TotalEmployesDecByDepartment(Employes employee);
        void TotalIndexInc();
        void TotalEmployesInSystemIncandDec(double value);
        void RecordEmployeeFindDuration(TimeSpan duration);




        //get data
        Task<string> CustomQueryPromethues(string customquery);
        Task<string> TotalIndexReachedPromethues();
        Task<string> TotalEmployesOverallSystemPromethues();
        Task<string> TotalEmployesByDepartment();

    }
}
