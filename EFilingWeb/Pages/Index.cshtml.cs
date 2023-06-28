using System.Net.Mime;
using EFilingWeb.Handler;
using EFilingWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EFilingWeb.Pages;

public class IndexModel : PageModel {
  private readonly ILogger<IndexModel> logger;
  private readonly PdfStreamGenerator retainerController;

  [BindProperty] public RetainerAgreementData Data { get; set; }


  public IndexModel(ILogger<IndexModel> logger, PdfStreamGenerator retainerController) {
    this.logger = logger;
    this.retainerController = retainerController;
    Data = generateSampleData();
  }


  public IActionResult OnPostAsync(CancellationToken token) {
    logger.LogInformation("Generating PDF for data {@RetainerAgreementData}", Data);
    Task<FileStream> resultTask = retainerController.generatePdf(Data, token);
    resultTask.Wait(token);
    ActionResult<Stream> result = resultTask.Result;

    if (result.Value == null) {
      return new RedirectToPageResult("Error");
    }

    logger.LogInformation("Received response with code {@Result}", result);

    return new FileStreamResult(result.Value, MediaTypeNames.Application.Octet);
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
