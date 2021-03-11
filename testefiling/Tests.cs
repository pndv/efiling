using System;
using System.Diagnostics;
using System.IO;
using efiling.libs;
using efiling.model;
using NUnit.Framework;

namespace testefiling {
    public class Tests {
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

        [Test]
        public void generatePdf() {
            var output = Generators.generatePdf(data);

            Assert.AreEqual(0, output);
        }

        [Test]
        public void testOpenPdf() {
            var outputPath = "out" + Path.DirectorySeparatorChar + "file.pdf";
            
            // Try to open the file in default viewer
            try {
                var p = new Process {StartInfo = new ProcessStartInfo(outputPath) {UseShellExecute = true}};
                var output = p.Start();
                Assert.IsTrue(output);
            } catch (Exception exception) {
                Console.WriteLine(exception);
                Assert.Fail($"Failed with exception {exception.Message}");
            }
        }
    }
}