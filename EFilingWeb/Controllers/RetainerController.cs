#region

using System.Net.Mime;
using EFilingWeb.Handler;
using EFilingWeb.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace EFilingWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class RetainerController : Controller {
  private readonly PdfStreamGenerator pdfStreamGenerator;
  private readonly ILogger<RetainerController> logger;

  public RetainerController(PdfStreamGenerator pdfStreamGenerator,
                            ILogger<RetainerController> logger) {
    this.logger = logger;
    this.pdfStreamGenerator = pdfStreamGenerator;
  }

  [HttpPost]
  public async Task<Results<BadRequest, FileStreamHttpResult>> createRetainer([FromBody] RetainerAgreementData data,
                                                                              CancellationToken cancellationToken) {

    logger.BeginScope(data);
    
    logger.LogInformation("Generating Pdf file for case {@CaseDetails}", data.CaseDetails);
    (string pdfFileName, FileStream fs) = await pdfStreamGenerator.generatePdf(data, cancellationToken);
    
    logger.LogInformation("Flushing the stream for file {FileName}", pdfFileName);
    await fs.FlushAsync(cancellationToken);
    
    logger.LogInformation("Return the generated pdf file {FileName}", pdfFileName);
    return TypedResults.Stream(fs, contentType: MediaTypeNames.Application.Pdf, fileDownloadName: pdfFileName);
  }
}
