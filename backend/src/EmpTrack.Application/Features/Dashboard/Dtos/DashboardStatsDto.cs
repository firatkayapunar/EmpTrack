namespace EmpTrack.Application.Features.Dashboard.Dtos
{
    public record DashboardStatsDto(int TotalEmployees, int TotalDepartments, int TotalTitles, int ActiveEmployees, int PassiveEmployees);
}