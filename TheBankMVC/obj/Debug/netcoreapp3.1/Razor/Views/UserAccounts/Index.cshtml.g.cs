#pragma checksum "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2a39dd9ac8222d7c5a69d58c04ea3531d5ee3a8b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserAccounts_Index), @"mvc.1.0.view", @"/Views/UserAccounts/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2a39dd9ac8222d7c5a69d58c04ea3531d5ee3a8b", @"/Views/UserAccounts/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c733950f50a74e244449f730944ba63839f17cd9", @"/Views/_ViewImports.cshtml")]
    public class Views_UserAccounts_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TheBankMVC.ViewModels.UserAccountViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Index</h1>\r\n\r\n<p>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2a39dd9ac8222d7c5a69d58c04ea3531d5ee3a8b3746", async() => {
                WriteLiteral("Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Banks.First().BankName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 19 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.UserAccountName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 25 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.PhoneNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 28 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ShareSubmitted));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 31 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.FineSubmitted));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 34 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.InterestSubmitted));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 37 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.AmountOnLoan));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 40 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.IsActive));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 46 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 49 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Banks.Where(b => b.BankId == item.BankId).First().BankName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 52 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.ActionLink(item.UserAccountName , "Edit", new { Id = item.UserAccountId }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 55 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 58 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.PhoneNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 61 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(item.ShareSubmitted.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 64 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(item.FineSubmitted.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 67 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(item.InterestSubmitted.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 70 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(item.AmountOnLoan.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 73 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.IsActive));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n");
            WriteLiteral("        </tr>\r\n");
#nullable restore
#line 81 "C:\Users\Anshul.vanawat\Documents\GitHub\BankMVC\TheBankMVC\Views\UserAccounts\Index.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TheBankMVC.ViewModels.UserAccountViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
