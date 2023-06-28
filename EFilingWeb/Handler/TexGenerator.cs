#region

using EFilingWeb.Model;
using EFilingWeb.Resources;

#endregion

namespace EFilingWeb.Handler;

public class TexGenerator {
  private static readonly string outputDir = "." + Path.DirectorySeparatorChar + "out";
  private readonly ILogger<TexGenerator> logger;

  public TexGenerator(ILogger<TexGenerator> logger) {
    this.logger = logger;
    if (Directory.Exists(outputDir)) {
      return;
    }

    this.logger.LogInformation("TexGenerator: creating the output directory for TEX files");
    DirectoryInfo info = Directory.CreateDirectory(outputDir);
    if (!info.Exists) {
      this.logger.LogCritical("TexGenerator: could not create directory {OutputDir}", outputDir);
      throw new FileNotFoundException($"Error creating directory {outputDir}");
    }

    string fullPath = Path.GetFullPath(outputDir);
    this.logger.LogInformation("TexGenerator: Output directory created at {OutputDirPath}", fullPath);
  }


  public async Task<string> generateTeX(RetainerAgreementData data, CancellationToken cancellationToken) {
    if (cancellationToken.IsCancellationRequested) {
      logger.LogCritical("Cancellation requested for TeX generation");
      cancellationToken.ThrowIfCancellationRequested();
    }


    logger.LogInformation("generateTex Start: case info {CaseInfo} for advocate {AdvocateName}",
                          data.CaseDetails.getCaseInfo(), data.Advocate.getFullName());
    string outputFile = outputDir + Path.DirectorySeparatorChar + $"file-{Guid.NewGuid()}.tex";
    string outputFilePath = Path.GetFullPath(outputFile);

    Retainer retainer = new(data);

    logger.LogInformation("generateTex: Generating tex");
    string texCode = retainer.TransformText();

    logger.LogInformation("generateTex: Writing to tex file {OutputFilePath}", outputFilePath);
    await File.WriteAllTextAsync(outputFilePath, texCode, cancellationToken);

    logger.LogInformation("generateTex END: Successfully wrote to TEX file {OutputFilePath}", outputFilePath);
    return outputFilePath;
  }
}
