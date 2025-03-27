﻿using ImageProcessingTool.ImageAnalyzers;
using ImageProcessingTool.VisionSystems;
using System.Drawing;

namespace ImageProcessingTool;

public sealed class ImageDiskImageAnalyzer : ImageAnalyzer
{
    public override IVisionSystem CreateVisionSystem()
    {
        return new ImageDiskVisionSystem();
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
