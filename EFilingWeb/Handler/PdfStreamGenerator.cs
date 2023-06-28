using EFilingWeb.Model;

namespace EFilingWeb.Handler; 

public class PdfStreamGenerator {
  private const int compileCount = 2;
  private readonly ILogger<PdfStreamGenerator> logger;
  private readonly TexGenerator tex;
  private readonly PdfGenerator pdf;
  
  public PdfStreamGenerator(TexGenerator tex, PdfGenerator pdf, ILogger<PdfStreamGenerator> logger) {
    this.tex = tex;
    this.pdf = pdf;
    this.logger = logger;
  }

  public async Task<FileStream> generatePdf(RetainerAgreementData data, CancellationToken cancellationToken) {
      if (cancellationToken.IsCancellationRequested) {
        logger.LogCritical("generatePdf: Request cancelled");
        cancellationToken.ThrowIfCancellationRequested();
      }

      logger.LogInformation("START generatePdf: Retainer Agreement generation for data {@Data}", data);

      Task<string> task = tex.generateTeX(data, cancellationToken)
                             .ContinueWith(async texTask => {
                                             cancellationToken.ThrowIfCancellationRequested();
                                             return await pdf.compileTex(texTask.Result, compileCount,
                                                                         cancellationToken);
                                           }, TaskContinuationOptions.OnlyOnRanToCompletion)
                             .Unwrap();

      logger.LogInformation("generatePdf: Waiting for compilation to finish");
      string pdfFilePath = await task;

      logger.LogInformation("generatePdf: Generating file stream for pdf {PdfPath}", pdfFilePath);
      FileStream fs = new(pdfFilePath, FileMode.Open);
      logger.LogInformation("generatePdf: Generated file stream for pdf {PdfPath}", pdfFilePath);
      
      return fs;
  }
  
}
