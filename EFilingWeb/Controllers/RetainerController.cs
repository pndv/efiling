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
  private readonly TexGenerator tex;
  private readonly PdfGenerator pdf;
  private readonly PdfStreamGenerator pdfStreamGenerator;
  private readonly ILogger<RetainerController> logger;

  public RetainerController(TexGenerator texGenerator,
                            PdfGenerator pdfGenerator,
                            PdfStreamGenerator pdfStreamGenerator,
                            ILogger<RetainerController> logger) {
    tex = texGenerator;
    pdf = pdfGenerator;
    this.logger = logger;
    this.pdfStreamGenerator = pdfStreamGenerator;
  }

  [HttpPost]
  public async Task<Results<BadRequest, FileStreamHttpResult>> createRetainer([FromBody] RetainerAgreementData data,
                                                                              CancellationToken cancellationToken) {

    (string pdfFileName, FileStream fs) = await pdfStreamGenerator.generatePdf(data, cancellationToken);
    await fs.FlushAsync(cancellationToken);
    FileStreamResult result = new(fs, MediaTypeNames.Application.Pdf);
    return TypedResults.Stream(fs, contentType: MediaTypeNames.Application.Pdf, fileDownloadName: pdfFileName);
  }
}
