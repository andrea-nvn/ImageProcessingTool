using ImageUtilities;
using ImageUtilities.Interfaces;

namespace ImageProcessingTool.AnalysisResults;

public sealed class AnalysisCsvFileHandler : IAnalysisFileHandler
{
    private const string Path = "./results";
    private const string FileName = "AnalysisResults.csv";
    private const string FilePath = $"{Path}/{FileName}";


    public void SaveAnalysis(
        ImageAnalysisResults processedImage)
    {
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }

        using var writer = new StreamWriter(FilePath, append: true);
        if (new FileInfo(FilePath).Length == 0)
        {
            writer.WriteLine("NomeFile,Dimensione,LuminositàStimata,DataAnalisi");
        }

        writer.WriteLine($"{processedImage.NomeFile},{processedImage.Dimensione},{processedImage.LuminositàStimata},{processedImage.DataAnalisi}");
    }

    public void SearchByBrightnessThreshold(
        double threshold)
    {
        if (!File.Exists(FilePath))
        {
            throw new Exception("Non è stata trovata nessuna analisi precedente.");
        }

        using var sr = new StreamReader(FilePath);
        bool found = false;

        sr.ReadLine();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();

            var fields = line?.Split(',') ?? [];

            if (double.TryParse(fields[2], out double value))
            {
                if (value > threshold)
                {
                    Console.WriteLine($"{line}");
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine($"Non è stato trovato nessun valore superiore a {threshold}.");
        }
    }
}
