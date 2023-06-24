using EFilingWeb.Controllers;
using EFilingWeb.Generator;
using Microsoft.Extensions.Logging;
using Moq;

namespace EFilingWebTest; 

public class RetainerControllerTest {
    private readonly Mock<ILogger<TexGenerator>> mockTexLogger = new();
    private readonly Mock<ILogger<PdfGenerator>> mockPdfLogger = new();
    private readonly Mock<ILogger<RetainerController>> mockRetainerLogger = new();
    private static readonly CancellationToken cancellationToken = CancellationToken.None;
    private const string xelatexPath = @"xelatex.exe";
    
    [Test]
    public async Task controllerTest() {
        Person advocate = new("AdvLast", "MidA", "AdvFirst", "Advocate",
                              new Address("123 Main St", "Sector 1", "Pocket A", "New Delhi", "Central", "Delhi", 110001));

        Person client = new("CltLast", "MidC", "CltFirst", "Proprietor",
                            new Address("1 NE Crescent", "Ludlow Castle Rd", null, "New Delhi", "South", "Delhi",
                                        110054));

        CaseDetails details = new("Writ", "123", "2023", DateTime.Today, "Court #1", "Acme Corp", "Supreme Court");

        RetainerAgreementData data = new(advocate, client, details);
        
        TexGenerator tg = new(mockTexLogger.Object);
        PdfGenerator pg = new(mockPdfLogger.Object, xelatexPath);
        RetainerController controller = new(tg, pg, mockRetainerLogger.Object);

        Stream retainer = await controller.createRetainer(data, cancellationToken);
        
        Assert.That(retainer.Length, Is.GreaterThan(0));
    }
}
