namespace ImageUtilities;

public sealed record ImageAnalysisResults(
    string NomeFile,
    long Dimensione,
    double LuminositàStimata,
    DateTimeOffset DataAnalisi);
