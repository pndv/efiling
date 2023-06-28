#region

using System.Net.Mime;
using EFilingWeb.Handler;
using EFilingWeb.Model;
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
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [Produces(contentType: MediaTypeNames.Application.Octet, Type = typeof(Stream))]
  public async Task<ActionResult<Stream>> createRetainer([FromBody] RetainerAgreementData data,
                                                         CancellationToken cancellationToken) {

    FileStream fs = await pdfStreamGenerator.generatePdf(data, cancellationToken);
    await fs.FlushAsync(cancellationToken);
    return new FileStreamResult(fs, MediaTypeNames.Application.Octet);
  }
}
