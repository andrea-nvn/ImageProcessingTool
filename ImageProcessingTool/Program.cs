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
        throw new Exception($"L'indice scelto deve essere minore di {imageAnalyzer.FileList.Count}");
    }

    var selectedImage = imageAnalyzer.FileList[imageIndex];
    var image = new Bitmap(selectedImage.FullName);

    Console.WriteLine($"\n{imageAnalyzer.CalculateImageBrightness(image)}");
    FileService.SearchFromCsv(100);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
