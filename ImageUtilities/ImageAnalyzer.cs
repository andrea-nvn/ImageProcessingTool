using ImageUtilities.Interfaces;
using System.Drawing;

namespace ImageUtilities;

public abstract class ImageAnalyzer
{
    public abstract IVisionSystem CreateVisionSystem();

    public abstract double CalculateImageBrightness(
        Bitmap image);
}
