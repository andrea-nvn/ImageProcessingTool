namespace ImageProcessingTool.AnalysisResults;

public sealed record AnalysisResults(
    string NomeFile,
    long Dimensione,
    double LuminositàStimata,
    DateTimeOffset DataAnalisi);
