#region

using EFilingWeb.Controllers;
using EFilingWeb.Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

#endregion

namespace EFilingWebTest;

public class RetainerControllerTest {
  private readonly Mock<ILogger<TexGenerator>> mockTexLogger = new();
  private readonly Mock<ILogger<PdfGenerator>> mockPdfLogger = new();
  private readonly Mock<ILogger<PdfStreamGenerator>> mockpdfStreamGenLogger = new();
  private readonly Mock<ILogger<RetainerController>> mockRetainerLogger = new();
  private static readonly CancellationToken cancellationToken = CancellationToken.None;

  [Fact]
  public async Task controllerTest() {
    RetainerAgreementData data = GeneratorsTests.getRetainerAgreementData();

    TexGenerator tg = new(mockTexLogger.Object);
    PdfGenerator pg = new(mockPdfLogger.Object);
    PdfStreamGenerator psg = new(tg, pg, mockpdfStreamGenLogger.Object);
    RetainerController controller = new(tg, pg, psg, mockRetainerLogger.Object);

    ActionResult<Stream> result = await controller.createRetainer(data, cancellationToken);
    
    Assert.NotNull(result.Value);
    Assert.True(result.Value.Length > 0);
  }
}
