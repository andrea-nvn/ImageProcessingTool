namespace ImageUtilities;

public interface IAnalysisFileHandler
{
    void SaveAnalysis(
        ImageAnalysisResults processedImage);

    void SearchByBrightnessThreshold(
        double threshold);
}
