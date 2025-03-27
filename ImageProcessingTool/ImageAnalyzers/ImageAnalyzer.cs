using ImageProcessingTool.VisionSystem;
using System.Drawing;
using System.Text;

namespace ImageProcessingTool.ImageAnalyzers;

public abstract class ImageAnalyzer
{
    private List<FileInfo> _fileList;


    public abstract IVisionSystem CreateVisionSystem();

    public string ListLoadedImages()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < _fileList.Count; i++)
        {
            var file = _fileList[i];

            sb.AppendLine($"[{i}] {file.Name} - {file.Length} byte - {file.CreationTimeUtc}");
        }

        return sb.ToString();
    }

    public double CalculateImageBrightness(
        int imageIndex)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(imageIndex, _fileList.Count);
        var img = new Bitmap(_fileList[imageIndex].FullName);

        long totalBytes = 0;
        long pixelCount = 0;

        for (int y = 0; y < img.Height; y++)
        {
            for (int x = 0; x < img.Width; x++)
            {
                var pixelColor = img.GetPixel(x, y);

                totalBytes += pixelColor.R + pixelColor.G + pixelColor.B;
                pixelCount++;
            }
        }

        var extimatedBrightness = (double)totalBytes / pixelCount;

        var processedImage = new ProcessedImage(_fileList[imageIndex].Name, _fileList[imageIndex].Length, extimatedBrightness, DateTimeOffset.Now);
        FileService.WriteToCsv(processedImage);

        return extimatedBrightness;
    }
}
