using ImageProcessingTool.ImageAnalyzers;
using ImageProcessingTool.VisionSystem;
using System.Drawing;
using System.Text;

namespace ImageProcessingTool;

public sealed class ImageDiskImageAnalyzer : ImageAnalyzer
{
    public ImageDiskImageAnalyzer()
    {
        FileList = [];
    }


    public List<FileInfo> FileList { get; private set; }


    public override IVisionSystem CreateVisionSystem()
    {
        return new ImageDiskVisionSystem();
    }

    public void AcquireImages(
        IVisionSystem visionSystem)
    {
        FileList = visionSystem.AcquireImages();
    }

    public string ListLoadedImages()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < FileList.Count; i++)
        {
            var file = FileList[i];

            sb.AppendLine($"[{i}] {file.Name} - {file.Length} byte - {file.CreationTimeUtc}");
        }

        return sb.ToString();
    }

    public override double CalculateImageBrightness(
        Bitmap image)
    {
        long totalBytes = 0;
        long pixelCount = 0;

        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                var pixelColor = image.GetPixel(x, y);

                totalBytes += pixelColor.R + pixelColor.G + pixelColor.B;
                pixelCount++;
            }
        }

        var extimatedBrightness = (double)totalBytes / pixelCount;

        return extimatedBrightness;
    }
}
