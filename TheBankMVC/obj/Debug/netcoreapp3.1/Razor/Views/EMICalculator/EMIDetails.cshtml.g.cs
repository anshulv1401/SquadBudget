#pragma checksum "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d25a6134fe588b579de3e5daf0fab705cad4fb66"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EMICalculator_EMIDetails), @"mvc.1.0.view", @"/Views/EMICalculator/EMIDetails.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\_ViewImports.cshtml"
using TheBankMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\_ViewImports.cshtml"
using TheBankMVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d25a6134fe588b579de3e5daf0fab705cad4fb66", @"/Views/EMICalculator/EMIDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c733950f50a74e244449f730944ba63839f17cd9", @"/Views/_ViewImports.cshtml")]
    public class Views_EMICalculator_EMIDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TheBankMVC.ViewModels.EMIDetails>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
  
    ViewData["Title"] = "EMIDetails";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<h1>EMIConfig</h1>\r\n\r\n<table id=\"EMIConfig\" class=\"table table-­bordered table-­hover”>\">\r\n    <tbody>\r\n        <tr>\r\n            <td>LoanAmount</td>\r\n            <td>");
#nullable restore
#line 13 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Model.EMIConfig.LoanAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>MonthlyRateOfInterest</td>\r\n            <td>");
#nullable restore
#line 17 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Model.EMIConfig.MonthlyRateOfInterest);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>NoOfInstallment</td>\r\n            <td>");
#nullable restore
#line 21 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Model.EMIConfig.NoOfInstallment);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>LockInPeriod</td>\r\n            <td>");
#nullable restore
#line 25 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Model.EMIConfig.LockInPeriod);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n    </tbody>\r\n</table>\r\n\r\n<h1>EMIDetails</h1>\r\n\r\n<table id=\"EMIDetails\" class=\"table table-­bordered table-­hover”>\">\r\n    <tbody>\r\n        <tr>\r\n            <td>MonthlyRateOfInterest</td>\r\n            <td>");
#nullable restore
#line 36 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Model.EMIHeader.MonthlyRateOfInterest.ToString("0.00000"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>EMIAmount</td>\r\n            <td>");
#nullable restore
#line 40 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Model.EMIHeader.EMIAmount.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n    </tbody>\r\n</table>\r\n\r\n<h1>TimePeriod</h1>\r\n\r\n<table id=\"TimePeriod\" class=\"table table-­bordered table-­hover”>\">\r\n    <tbody>\r\n        <tr>\r\n            <td>MonthlyRateOfInterest</td>\r\n            <td>");
#nullable restore
#line 51 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Model.TimePeriod.StartTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>EMIAmount</td>\r\n            <td>");
#nullable restore
#line 55 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Model.TimePeriod.EndTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>DateFormat</td>\r\n            <td>");
#nullable restore
#line 59 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Model.TimePeriod.DateFormat);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
        </tr>
    </tbody>
</table>

<h1>Installments</h1>

<table id=""Installments"" class=""table table-­bordered table-­hover”>"">
    <thead>
        <tr>
            <th>InstallmentNo</th>
            <th>DateOfInstallment</th>
            <th>Opening</th>
            <th>PrincipalAmount</th>
            <th>InterestAmount</th>
            <th>EMIAmount</th>
            <th>Closing</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 79 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
         foreach (var installment in Model.Installments)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 82 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(installment.InstallmentNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 83 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(installment.DateOfInstallment);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 84 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(installment.Opening.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 85 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(installment.PrincipalAmount.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 86 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(installment.InterestAmount.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 87 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
             if (installment.Difference > 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>\r\n                    ");
#nullable restore
#line 90 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
               Write(installment.EMIAmount.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" + ");
#nullable restore
#line 90 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
                                                         Write(installment.Difference.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n");
#nullable restore
#line 92 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
            }
            else if (installment.Difference == 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>\r\n                    ");
#nullable restore
#line 96 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
               Write(installment.EMIAmount.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n");
#nullable restore
#line 98 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>\r\n                    ");
#nullable restore
#line 102 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
               Write(installment.EMIAmount.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 102 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
                                                         Write(installment.Difference.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n");
#nullable restore
#line 104 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <td>");
#nullable restore
#line 105 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
           Write(Math.Abs(installment.Closing).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 107 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\EMICalculator\EMIDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TheBankMVC.ViewModels.EMIDetails> Html { get; private set; }
    }
}
#pragma warning restore 1591
