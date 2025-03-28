namespace ImageUtilities.Interfaces;

public interface IAnalysisFileHandler
{
    void SaveAnalysis(
        ImageAnalysisResults processedImage);

    void SearchByBrightnessThreshold(
        double threshold);
}
