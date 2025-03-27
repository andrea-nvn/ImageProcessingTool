using ImageProcessingTool;
using ImageProcessingTool.ImageAnalyzer;


try
{
    var visionSystem = new ImageDiskVisionSystem();
    var imageAnalyzer = new ImageAnalyzer();

    imageAnalyzer.AcquireImages(visionSystem);

    Console.WriteLine($"\n{imageAnalyzer.ListLoadedImages()}");
    Console.WriteLine("Seleziona l'immagine da analizzare: ");

    if (!int.TryParse(Console.ReadLine(), out int imageIndex))
    {
        Console.WriteLine("\nIndice non valido!");
        return;
    }

    Console.WriteLine($"\n{imageAnalyzer.CalculateImageBrightness(imageIndex)}");
    FileService.SearchFromCsv(100);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
