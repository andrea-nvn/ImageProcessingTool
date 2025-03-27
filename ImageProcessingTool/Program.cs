using ImageProcessingTool;

do
{
    try
    {
        Console.WriteLine("Inserisci il percorso dei file da analizzare (0 per terminare): ");
        var path = Console.ReadLine();

        if (path == "0")
        {
            break;
        }

        var imageAnalyzer = new ImageAnalyzer();
        imageAnalyzer.LoadImages(path);

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
} while (true);
