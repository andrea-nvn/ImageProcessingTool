using ImageUtilities.Interfaces;

namespace ImageProcessingTool.ImageDisk;

public sealed class ImageDiskVisionSystem : IVisionSystem
{
    public List<FileInfo> AcquireImages()
    {
        Console.WriteLine("Inserisci il percorso dei file da analizzare: ");
        var path = Console.ReadLine();

        if (!Directory.Exists(path))
        {
            throw new Exception("La directory selezionata non esiste.");
        }

        return LoadImagesFromDirectory(path);
    }


    private static List<FileInfo> LoadImagesFromDirectory(
        string path)
    {
        var pngImages = Directory.GetFiles(path, "*.png");
        var jpgImages = Directory.GetFiles(path, "*.jpg");
        var images = pngImages.Concat(jpgImages);

        var imagesFileInfo = new List<FileInfo>();

        foreach (var image in images)
        {
            var fileInfo = new FileInfo(image);

            imagesFileInfo.Add(fileInfo);
        }

        return imagesFileInfo;
    }
}
