using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using EFilingWeb.Handler;
using EFilingWeb.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EFilingWeb.Pages;

public class IndexModel : PageModel {
  private readonly ILogger<IndexModel> logger;
  private readonly PdfStreamGenerator pdfStreamGenerator;

  [BindProperty]
  [Required]
  public RetainerAgreementData Data { get; set; }


  public IndexModel(ILogger<IndexModel> logger, PdfStreamGenerator pdfStreamGenerator) {
    this.logger = logger;
    this.pdfStreamGenerator = pdfStreamGenerator;
    Data = generateSampleData();
  }


  public IActionResult OnPostAsync(CancellationToken token) {
    logger.LogInformation("Generating PDF for data {@RetainerAgreementData}", Data);
    Task<(string fileName, FileStream)> resultTask = pdfStreamGenerator.generatePdf(Data, token);
    resultTask.Wait(token);
    (string fileName, FileStream fileStream) = resultTask.Result;

    if (fileStream.Length <= 0) {
      return new RedirectToPageResult("Error");
    }

    logger.LogInformation("Received response with code {@Result}", fileStream);

    FileStreamResult result = new(fileStream, contentType: MediaTypeNames.Application.Pdf) {
      FileDownloadName = fileName
    };
    return result;
  }

  public void OnGet() { }

  private static RetainerAgreementData generateSampleData() {
    Address address = new("TestLine1",
                          string.Empty,
                          string.Empty,
                          "TestCity",
                          string.Empty,
                          "TestState",
                          "TestPinCode");
    return new RetainerAgreementData(new Person("TestAdvocate",
                                                string.Empty,
                                                "TestAdvocate",
                                                "TestAdvocate",
                                                address),
                                     new Person("TestRespondent",
                                                string.Empty,
                                                "TestRespondent",
                                                "TestRespondent",
                                                address),
                                     new CaseDetails("Writ Petition",
                                                     "123",
                                                     "2023",
                                                     DateTime.Today,
                                                     "Supreme Court",
                                                     "TestPetitioner",
                                                     "TestJurisdiction"));
  }
}
