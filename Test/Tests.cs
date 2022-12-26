using System;
using System.Diagnostics;
using System.IO;
using EFiling.Libs;
using EFiling.Model;
using NUnit.Framework;

namespace TestEFiling;

public class Tests {
    private static readonly string relativeFilePath = $"out{Path.DirectorySeparatorChar}file.pdf";

    private static readonly Address advAddress = new("123 Christie St.",
                                                     "Apt 1",
                                                     "Ste 300",
                                                     "Gainesville",
                                                     "Alachua",
                                                     "California",
                                                     123456);

    private static readonly Person advocate = new("Jane",
                                                  "Doe",
                                                  "Ms",
                                                  advAddress);

    private static readonly Address cltAddress = new("1 Ялинцкая Ул",
                                                     null,
                                                     null,
                                                     "नोवोसिबीरस्क",
                                                     "Novosibirsk Oblast",
                                                     "Сибирь",
                                                     432554);

    private static readonly Person client = new("John",
                                                "Malkovich",
                                                "Dr",
                                                cltAddress);

    private static readonly CaseDetails details = new("Writ",
                                                      "12",
                                                      "2021",
                                                      DateTime.Today,
                                                      "Supreme Court of India",
                                                      "State of Kerala",
                                                      "Delhi");

    private static readonly Data data = new(advocate,
                                            client,
                                            details);

    [Test, Order(0)]
    public void generatePdf() {
        var output = Generators.generatePdf(data);

        Console.WriteLine($"Output generated: {output}");

        Assert.NotNull(output);
        Assert.IsNotEmpty(output);
        Assert.IsTrue(output.Contains(relativeFilePath));
    }

    [Test, Ignore("This is used for testing on windows application with a installed PDF viewer. Not for GitHub")]
    public void testOpenPdf() {
        var outputPath = "out" + Path.DirectorySeparatorChar + "file.pdf";

        // Try to open the file in default viewer
        try {
            var p = new Process { StartInfo = new ProcessStartInfo(outputPath) { UseShellExecute = true } };
            var output = p.Start();
            Assert.IsTrue(output);
        } catch (Exception exception) {
            Console.WriteLine(exception);
            Assert.Fail($"Failed with exception {exception.Message}");
        }
    }
}
