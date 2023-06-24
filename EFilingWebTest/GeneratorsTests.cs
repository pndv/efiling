using EFilingWeb.Generator;
using Microsoft.Extensions.Logging;
using Moq;

namespace EFilingWebTest;

public class Tests {
    private readonly Mock<ILogger<TexGenerator>> mockTexLogger = new();
    private readonly Mock<ILogger<PdfGenerator>> mockPdfLogger = new();
    private static readonly CancellationToken cancellationToken = CancellationToken.None;
    private const string xelatexPath = @"xelatex.exe";

    [SetUp]
    public void Setup() { }

    [Test]
    public void texTest() {
        TexGenerator tg = new(mockTexLogger.Object);
        Person advocate = new("AdvLast", "MidA", "AdvFirst", "Advocate",
                              new Address("123 Main St", "Sector 1", "Pocket A", "New Delhi", "Central", "Delhi", 110001));

        Person client = new("CltLast", "MidC", "CltFirst", "Proprietor",
                            new Address("1 NE Crescent", "Ludlow Castle Rd", null, "New Delhi", "South", "Delhi",
                                        110054));

        CaseDetails details = new("Writ", "123", "2023", DateTime.Today, "Court #1", "Acme Corp", "Supreme Court");

        RetainerAgreementData data = new(advocate, client, details);

        Task<string> generateTeX = tg.generateTeX(data, cancellationToken);
        generateTeX.Wait();
        string filePath = generateTeX.Result;

        Assert.That(File.Exists(filePath), Is.True);
        Console.WriteLine(filePath);
    }
    
    [Test]
    public void pdfTest() {
        string filePath =
            @"D:\Projects\EFilingWeb\EFilingWebTest\bin\Debug\net7.0\out\file.tex";
        PdfGenerator pg = new(mockPdfLogger.Object, xelatexPath);
        Task<string> compileTask = pg.compileTex(filePath, 1, cancellationToken);
        compileTask.Wait();
        string pdfPath = compileTask.Result;
        
        Assert.That(File.Exists(pdfPath), Is.True);
        Console.WriteLine(pdfPath);
    }
}
