namespace ImageProcessingTool.AnalysisResults;

public sealed record ImageAnalysisResults(
    string NomeFile,
    long Dimensione,
    double LuminositàStimata,
    DateTimeOffset DataAnalisi);
