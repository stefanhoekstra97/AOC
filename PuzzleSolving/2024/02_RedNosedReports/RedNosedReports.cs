using Infrastructure.Entities;

namespace PuzzleSolving._2024._02_RedNosedReports;

public class RedNosedReports : IPuzzleSolver
{
    public Task<string> SolveSilver(PuzzleInput input)
    {
        return Task.FromResult(input.Lines.Count(l => ReportSafetyInspector(l.Split(" ").Select(int.Parse).ToList())).ToString());
    }
    
    private static bool ReportSafetyInspector(List<int> report)
    {
        // int reportRectifier = report[0] - report[1] > 0 ? 1 : report[0] - report[1] < 0 ? -1 : throw new ReportSafetyException("Report was very very unsafe. Neither increaasing nor decreasing levels!");
        int? reportRectifier = report[0] - report[1] > 0 ? 1 : report[0] - report[1] < 0 ? -1 : null ;
        if (reportRectifier == null) return false;
        
        for (int i = 1; i < report.Count; i++)
        {
            if ((report[i - 1] - report[i]) * reportRectifier > 3 || (report[i - 1] - report[i]) * reportRectifier < 1)
            {
                return false;
            }
        }

        return true;
    }

    public Task<string> SolveGold(PuzzleInput input)
    {
        var safeReports = 0;
        foreach (var line in input.Lines)
        {
            List<int> report = line.Split(" ").Select(int.Parse).ToList();
            if (ReportSafetyInspector(report))
            {
                safeReports++;
            } else {
                if (ReportSafetyInspectorWithDampening(report))
                {
                    safeReports++;
                }
            }
        }
        return Task.FromResult(safeReports.ToString());
    }
    private static bool ReportSafetyInspectorWithDampening(List<int> report)
    {
        for (int i = 0; i < report.Count; i++)
        {
            var forcedReport = new List<int>(report.Count - 1);
            forcedReport.AddRange(report.GetRange(0, i));
            forcedReport.AddRange(report.GetRange(i + 1, report.Count - i - 1));
            if (ReportSafetyInspector(forcedReport))
            {
                return true;
            }
        }

        return false;
    }
}

internal class ReportSafetyException(string safetyReason) : Exception(safetyReason)
{
}