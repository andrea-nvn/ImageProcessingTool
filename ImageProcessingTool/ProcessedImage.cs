namespace ImageProcessingTool;

public sealed record ProcessedImage(
    string NomeFile,
    long Dimensione,
    double LuminositàStimata,
    DateTimeOffset DataAnalisi);
