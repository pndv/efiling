using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Antlr4.StringTemplate;
using efiling.model;

namespace efiling.libs {
    public class Generators {
        private const string fName = "file.tex";
        private const string templateFile = "efiling.templates.vakalatnama.stg";
        private static readonly string outputFileRelativePath = "out" + Path.DirectorySeparatorChar + "file.pdf";

        public static String generatePdf(Data data) {
            generateTeX(data);

            Console.Out.WriteLine("Compiling TeX file");
            return compileTex(fName, 3); //Compile thrice to fully generate indices
        }

        private static void generateTeX(Data data) {
            string strTemplate;
            using (var templateStream = typeof(Generators).Assembly.GetManifestResourceStream(templateFile)) {
                if (templateStream == null) {
                    const string emptyStream = "The embedded template could be read";
                    Console.Error.WriteLine(emptyStream);
                    return;
                }

                var streamReader = new StreamReader(templateStream, Encoding.UTF8);
                strTemplate = streamReader.ReadToEnd();
            }

            TemplateGroup templateGroup = new TemplateGroupString(strTemplate);
            var template = templateGroup.GetInstanceOf("main");
            template.Add("data", data);

            var tex = template.Render();

            File.WriteAllText(fName, tex);
        }

        private static string compileTex(string texFileName, int compileCount) {
            var fullTexFilePath = Path.GetFullPath(texFileName);
            var outputFilePath = Path.GetFullPath(outputFileRelativePath);

            using var pProcess = new Process {
                StartInfo = {
                    FileName = "xelatex.exe",
                    Arguments = "-file-line-error -interaction=nonstopmode -synctex=1 " +
                                "-enable-installer -output-directory=out " + $@"""{fullTexFilePath}""",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };

            for (var i = 1; i <= compileCount; i++) {
                try {
                    pProcess.Start();
                } catch (Exception e) {
                    Console.Out.WriteLine(e.Message);
                }

                var output = pProcess.StandardOutput.ReadToEnd();

                var error = pProcess.StandardError.ReadToEnd();
                pProcess.WaitForExit();


                var exitCode = pProcess.ExitCode;

                if (exitCode == 0 || i < compileCount) continue;

                Console.Error.WriteLine("Error creating pdf file");
                // Console.Error.WriteLine(error);
                Console.WriteLine(output);
                return "";
            }

            Console.Out.WriteLine($"PDF file generate {outputFilePath}");
            return outputFilePath;
        }
    }
}