#pragma checksum "C:\Users\wasim\source\repos\eVotingClientApplication\Views\Admin\Success.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39c0e824117ae934150993ead8d30cea7772132d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Success), @"mvc.1.0.view", @"/Views/Admin/Success.cshtml")]
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
#line 1 "C:\Users\wasim\source\repos\eVotingClientApplication\Views\_ViewImports.cshtml"
using eVotingClientApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\wasim\source\repos\eVotingClientApplication\Views\_ViewImports.cshtml"
using eVotingClientApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39c0e824117ae934150993ead8d30cea7772132d", @"/Views/Admin/Success.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c247989789af52d1d46e8158f977509f9b3fc486", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Success : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\wasim\source\repos\eVotingClientApplication\Views\Admin\Success.cshtml"
  
    ViewData["Title"] = "Success";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"container\">\r\n        <h1 class=\"display-3\">Added Successfully</h1>\r\n        ");
#nullable restore
#line 8 "C:\Users\wasim\source\repos\eVotingClientApplication\Views\Admin\Success.cshtml"
   Write(Html.ActionLink("Add Contender","AddContender","Admin",null,new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591