#region

using System.Diagnostics;

#endregion

namespace EFilingWeb.Handler;

public class PdfGenerator {
  private readonly ILogger<PdfGenerator> logger;
  private const string xelatexPath = "xelatex";

  public PdfGenerator(ILogger<PdfGenerator> logger) {
    this.logger = logger;
  }

  public async Task<string> compileTex(string texFilePath, int compileCount, CancellationToken cancellationToken) {
    if (cancellationToken.IsCancellationRequested) {
      logger.LogCritical("Task cancellation requested for compilation of TeX file {TeXFileName}", texFilePath);
      cancellationToken.ThrowIfCancellationRequested();
    }

    logger.LogInformation("compileTex START: TeX file path: {TeXFile} and total compile runs {CompileTimes}",
                          texFilePath, compileCount);
    string fullTexFilePath = Path.GetFullPath(texFilePath);
    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(texFilePath);
    string outputDir = Path.GetDirectoryName(fullTexFilePath) ?? ".";
    string outputFilePath = outputDir + Path.DirectorySeparatorChar + fileNameWithoutExtension + ".pdf";


    for (int i = 1; i <= compileCount; i++) {
      using Process texProcess = getTexProcess(xelatexPath, outputDir, fullTexFilePath);

      try {
        texProcess.Start();
      } catch (Exception e) {
        logger.LogError(e, "Error compiling file {FileName}. Message: {ExceptionMessage}", fileNameWithoutExtension,
                        e.Message);
        throw;
      }


      string output = await texProcess.StandardOutput.ReadToEndAsync(cancellationToken);
      string error = await texProcess.StandardError.ReadToEndAsync(cancellationToken);
      int exitCode = texProcess.ExitCode;
      await texProcess.WaitForExitAsync(cancellationToken);

      if (exitCode == 0 || i < compileCount) {
        logger.LogInformation("Compile iteration {IterationCount} completed without error. Output message: {Output}",
                              i, output);
        continue;
      }

      logger.LogError("Error creating pdf file {fileName}. Error: {ErrorMessage}. Output message: {Output}",
                      fileNameWithoutExtension, error, output);

      throw new Exception("Could not compile TeX file with XeTeX");
    }

    logger.LogInformation("Successfully generated PDF file {OutputFilePath}", outputFilePath);
    return outputFilePath;
  }

  private static Process getTexProcess(string exeFilePath, string outputDir, string fullTexFilePath) {
    return new Process {
      StartInfo = {
        FileName = exeFilePath,
        Arguments = "-verbose " +
                    "-file-line-error " +
                    "-interaction=nonstopmode " +
                    "-synctex=1 " +
                    "-enable-installer " +
                    $@"-output-directory=""{outputDir}"" " +
                    $@"-aux-directory=""{outputDir}"" " +
                    fullTexFilePath,
        WorkingDirectory = outputDir,
        UseShellExecute = false,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        WindowStyle = ProcessWindowStyle.Hidden,
        CreateNoWindow = true,
      },
      EnableRaisingEvents = true
    };
  }
}
