using ImageProcessingTool;
using System.Drawing;
using System.Text;


try
{
    var imageAnalyzer = new ImageDiskImageAnalyzer();
    var visionSystem = imageAnalyzer.CreateVisionSystem();

    var images = visionSystem.AcquireImages();

    var sb = new StringBuilder();

    for (int i = 0; i < images.Count; i++)
    {
        var image = images[i];
        sb.AppendLine($"[{i}] {image.Name} - {image.Length} byte - {image.CreationTimeUtc}");
    }

    Console.WriteLine($"\n{sb}");
    Console.WriteLine("Seleziona l'immagine da analizzare: ");

    if (!int.TryParse(Console.ReadLine(), out int imageIndex))
    {
        Console.WriteLine("\nIndice non valido!");
        return;
    }

    if (imageIndex > images.Count)
    {
        Console.WriteLine($"L'indice scelto deve essere minore di {images.Count}");
        return;
    }

    var selectedImage = images[imageIndex];
    var bitmap = new Bitmap(selectedImage.FullName);
    var extimatedBrightness = imageAnalyzer.CalculateImageBrightness(bitmap);

    Console.WriteLine($"\nLuminosità stimata: {extimatedBrightness}");

    var processedImage = new ProcessedImage(selectedImage.Name, selectedImage.Length, extimatedBrightness, DateTimeOffset.Now);
    FileService.WriteToCsv(processedImage);

    Console.WriteLine($"\nInserire valore minimo di luminosità per la ricerca:");
    if (!double.TryParse(Console.ReadLine(), out double threshold))
    {
        Console.WriteLine("\nValore non valido!");
        return;
    }

    FileService.SearchFromCsv(threshold);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
