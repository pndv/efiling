using EFilingWeb.Generator;
using EFilingWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;

namespace EFilingWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class RetainerController : Controller {
    private const int compileCount = 1;
    private readonly TexGenerator tex;
    private readonly PdfGenerator pdf;
    private readonly ILogger<RetainerController> logger;

    public RetainerController(TexGenerator texGenerator,
                              PdfGenerator pdfGenerator,
                              ILogger<RetainerController> logger) {
        tex = texGenerator;
        pdf = pdfGenerator;
        this.logger = logger;
    }

    [HttpPost]
    public async Task<Stream> createRetainer([FromBody] RetainerAgreementData data,
                                             CancellationToken cancellationToken) {
        
        logger.LogInformation("START createRetainer: Retainer Agreement generation for data {@Data}", data);

        Task<string> task = tex.generateTeX(data, cancellationToken)
                                 .ContinueWith(async texTask => {
                                                   cancellationToken.ThrowIfCancellationRequested();
                                                   return await pdf.compileTex(texTask.Result, compileCount,
                                                                               cancellationToken);
                                               }, TaskContinuationOptions.OnlyOnRanToCompletion)
                                 .Unwrap();

        logger.LogInformation("createRetainer: Waiting for compilation to finish");
        string pdfFilePath = await task;
        
        logger.LogInformation("createRetainer: Generating file stream");
        FileStream fs = new(pdfFilePath, FileMode.Open);
        await fs.FlushAsync(cancellationToken);
        return fs;
    }
}
