using ImageProcessingTool;
using System.Drawing;


try
{
    var imageAnalyzer = new ImageDiskImageAnalyzer();
    var visionSystem = imageAnalyzer.CreateVisionSystem();

    imageAnalyzer.AcquireImages(visionSystem);

    Console.WriteLine($"\n{imageAnalyzer.ListLoadedImages()}");
    Console.WriteLine("Seleziona l'immagine da analizzare: ");

    if (!int.TryParse(Console.ReadLine(), out int imageIndex))
    {
        Console.WriteLine("\nIndice non valido!");
        return;
    }

    if (imageIndex > imageAnalyzer.FileList.Count)
    {
        Console.WriteLine($"L'indice scelto deve essere minore di {imageAnalyzer.FileList.Count}");
        return;
    }

    var selectedImage = imageAnalyzer.FileList[imageIndex];
    var image = new Bitmap(selectedImage.FullName);
    var extimatedBrightness = imageAnalyzer.CalculateImageBrightness(image);

    Console.WriteLine($"\n{extimatedBrightness}");

    var processedImage = new ProcessedImage(selectedImage.Name, selectedImage.Length, extimatedBrightness, DateTimeOffset.Now);
    FileService.WriteToCsv(processedImage);

    FileService.SearchFromCsv(100);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
