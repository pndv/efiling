#region

using EFilingWeb.Handler;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;

#endregion

namespace EFilingWebTest;

public class GeneratorsTests {
  private readonly ITestOutputHelper testOutputHelper;
  private readonly Mock<ILogger<TexGenerator>> mockTexLogger = new();
  private readonly Mock<ILogger<PdfGenerator>> mockPdfLogger = new();
  private static readonly CancellationToken cancellationToken = CancellationToken.None;

  public GeneratorsTests(ITestOutputHelper testOutputHelper) {
    this.testOutputHelper = testOutputHelper;
  }


  [Fact]
  public void texTest() {
    TexGenerator tg = new(mockTexLogger.Object);
    RetainerAgreementData data = getRetainerAgreementData();

    Task<string> generateTeX = tg.generateTeX(data, cancellationToken);
    generateTeX.Wait();
    string filePath = generateTeX.Result;

    Assert.True(File.Exists(filePath));
    testOutputHelper.WriteLine(filePath);
  }
  
  [Fact]
  public void pdfTest() {
    const string filePath = @"EFilingWebTest\bin\Debug\net7.0\out\file.tex";
    PdfGenerator pg = new(mockPdfLogger.Object);
    Task<string> compileTask = pg.compileTex(filePath, 1, cancellationToken);
    compileTask.Wait();
    string pdfPath = compileTask.Result;

    Assert.True(File.Exists(pdfPath));
    testOutputHelper.WriteLine(pdfPath);
  }
  
  internal static RetainerAgreementData getRetainerAgreementData() {
    Person advocate = new("AdvLast", "MidA", "AdvFirst", "Advocate",
                          new Address("123 Main St", "Sector 1", "Pocket A", "New Delhi", "Central", "Delhi", "110001"));

    Person client = new("CltLast", "MidC", "CltFirst", "Proprietor",
                        new Address("1 NE Crescent", "Ludlow Castle Rd", null, "New Delhi", "South", "Delhi",
                                    "110054"));

    CaseDetails details = new("Writ", "123", "2023", DateTime.Today, "Court #1", "Acme Corp", "Supreme Court");

    RetainerAgreementData data = new(advocate, client, details);

    return data;
  }
}
