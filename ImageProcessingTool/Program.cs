using ImageProcessingTool.AnalysisResults;
using ImageProcessingTool.ImageDisk;
using ImageUtilities;
using System.Drawing;
using System.Text;
using System.Text.Json;


try
{
    var imageAnalyzer = CreateImageAnalyzerFromSettings();
    var visionSystem = imageAnalyzer.CreateVisionSystem();

    var images = visionSystem.AcquireImages();
    ShowAcquiredImages(images);

    var imageIndex = GetImageToProcessIndexFromUser(images.Count);

    var analysisResults = AnalyzeSelectedImage(imageAnalyzer, images[imageIndex]);
    Console.WriteLine($"\nLuminosità stimata: {analysisResults.LuminositàStimata}");

    FileService.WriteToCsv(analysisResults);

    SearchAnalisysResultsByBrightnessThreshold();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


static ImageAnalyzer CreateImageAnalyzerFromSettings()
{
    var appsettings = File.ReadAllText("appsettings.json");

    var doc = JsonDocument.Parse(appsettings);
    var root = doc.RootElement;
    var visionSistemType = root.GetProperty("VisionSistemType").GetString();

    return visionSistemType switch
    {
        nameof(ImageDiskImageAnalyzer) => new ImageDiskImageAnalyzer(),
        _ => throw new NotSupportedException("Dispositivo di visione non supportato!")
    };
}

static void ShowAcquiredImages(
    List<FileInfo> images)
{
    var sb = new StringBuilder();

    for (int i = 0; i < images.Count; i++)
    {
        var image = images[i];
        sb.AppendLine($"[{i}] {image.Name} - {image.Length} byte - {image.CreationTimeUtc}");
    }

    Console.WriteLine($"\n{sb}");
}

static int GetImageToProcessIndexFromUser(
    int imagesCount)
{
    Console.WriteLine("Seleziona l'immagine da analizzare: ");

    if (!int.TryParse(Console.ReadLine(), out int imageIndex))
    {
        throw new Exception("Indice non valido!");
    }

    if (imageIndex > imagesCount)
    {
        throw new Exception($"L'indice scelto deve essere minore di {imagesCount}");
    }

    return imageIndex;
}

static ImageAnalysisResults AnalyzeSelectedImage(
    ImageAnalyzer imageAnalyzer,
    FileInfo image)
{
    var bitmap = new Bitmap(image.FullName);
    var extimatedBrightness = imageAnalyzer.CalculateImageBrightness(bitmap);

    return new ImageAnalysisResults(image.Name, image.Length, extimatedBrightness, DateTimeOffset.Now);
}

static void SearchAnalisysResultsByBrightnessThreshold()
{
    Console.WriteLine($"\nInserire valore minimo di luminosità per la ricerca:");

    if (!double.TryParse(Console.ReadLine(), out double threshold))
    {
        throw new Exception("Valore non valido!");
    }

    FileService.SearchFromCsv(threshold);
}