using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using efiling.libs;
using efiling.model;

namespace efiling {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private async void btnGenerate_Click(object sender, RoutedEventArgs e) {
           TxtBlkOutputMessage.Text = "Generating output...";

           var addrAdv = new Address(TxtAdvAddr1.Text,
                                      TxtAdvAddr2.Text,
                                      TxtAdvAddr3.Text,
                                      TxtAdvCity.Text,
                                      TxtAdvDistrict.Text,
                                      CmbAdvState.Text,
                                      int.Parse(TxtAdvPinCode.Text));
            var advocate = new Person(TxtAdvLName.Text,
                                      TxtAdvFName.Text,
                                      TxtAdvTitle.Text,
                                      addrAdv);

            var addrResp = new Address(TxtCltAddr1.Text,
                                       TxtCltAddr2.Text,
                                       TxtCltAddr3.Text,
                                       TxtCltCity.Text,
                                       TxtCltDistrict.Text,
                                       CmbCltState.Text,
                                       int.Parse(TxtCltPinCode.Text));
            var respondent = new Person(TxtCltLName.Text,
                                        TxtCltFName.Text,
                                        TxtCltTitle.Text,
                                        addrResp);

            var caseDetails = new CaseDetails(TxtCaseType.Text,
                                              TxtCaseNo1.Text,
                                              TxtCaseNo2.Text,
                                              DtFilingDate.SelectedDate.GetValueOrDefault(),
                                              CmbCtName.Text,
                                              TxtPetitioner.Text,
                                              TxtJurisdiction.Text);
            var data = new Data(advocate, respondent, caseDetails);

            var task = Task.Run(() => Generators.generatePdf(data, TxtBlkOutputMessage));
            var outputPath = await task;

            var message = string.IsNullOrWhiteSpace(outputPath) ? 
                              "Error generating file" : 
                              $"File written to:\n{outputPath}";

            TxtBlkOutputMessage.Text = message;

            if (string.IsNullOrWhiteSpace(outputPath)) return;
            
            // Try to open the file in default viewer
            try {
                var p = new Process {StartInfo = new ProcessStartInfo(outputPath) {UseShellExecute = true}};
                p.Start();
            } catch (Exception exception) {
                MessageBox.Show(exception.StackTrace);
            }
        }

        
    }
}