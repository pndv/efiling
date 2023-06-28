#region

using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

#endregion

namespace EFilingWebTest;

public class RetainerControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>> {
  private readonly WebApplicationFactory<Program> factory;
  private readonly ITestOutputHelper testOutputHelper;

  public RetainerControllerIntegrationTest(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper) {
    this.factory = factory;
    this.testOutputHelper = testOutputHelper;
  }


  [Fact]
  public async Task testPdfGenEndpoint() {
    using HttpClient client = factory.CreateClient();
    RetainerAgreementData data = GeneratorsTests.getRetainerAgreementData();
    HttpContent content = JsonContent.Create(data);

    HttpResponseMessage response = await client.PostAsync("/retainer", content);

    response.EnsureSuccessStatusCode();
    testOutputHelper.WriteLine(response.StatusCode.ToString());
    Assert.True((await response.Content.ReadAsStreamAsync()).Length > 0);
  }
}
