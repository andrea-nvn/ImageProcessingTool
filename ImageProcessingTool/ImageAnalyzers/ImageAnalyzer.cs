using ImageProcessingTool.VisionSystems;
using System.Drawing;

namespace ImageProcessingTool.ImageAnalyzers;

public abstract class ImageAnalyzer
{
    public abstract IVisionSystem CreateVisionSystem();

    public abstract double CalculateImageBrightness(
        Bitmap image);
}
