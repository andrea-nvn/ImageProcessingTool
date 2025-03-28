using ImageProcessingTool.ImageDisk;
using System.Drawing;
using System.Drawing.Imaging;

namespace Test;

public class ImageDiskImageAnalyzerTest
{
    [Fact]
    public void Black_Image_Brightness_Should_Be_Zero()
    {
        var imageDiskImageAnalyzer = new ImageDiskImageAnalyzer();
        var blackImage = new Bitmap(50, 50, PixelFormat.Format64bppArgb);

        var imageBrightness = imageDiskImageAnalyzer.CalculateImageBrightness(blackImage);

        Assert.Equal(0, imageBrightness);
    }
}