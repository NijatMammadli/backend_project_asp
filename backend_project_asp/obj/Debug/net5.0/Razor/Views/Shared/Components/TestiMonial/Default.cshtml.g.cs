#pragma checksum "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\Shared\Components\TestiMonial\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02e06a967469d0c715262ad94bd463550d5c9062"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_TestiMonial_Default), @"mvc.1.0.view", @"/Views/Shared/Components/TestiMonial/Default.cshtml")]
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
#line 1 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\_ViewImports.cshtml"
using backend_project_asp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\_ViewImports.cshtml"
using backend_project_asp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\_ViewImports.cshtml"
using backend_project_asp.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02e06a967469d0c715262ad94bd463550d5c9062", @"/Views/Shared/Components/TestiMonial/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9af22fe791b9f4b64de105a8ab88a5a347a515cb", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_TestiMonial_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("testimonial"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\Shared\Components\TestiMonial\Default.cshtml"
  
    List<TestiMonial> testiMonials = Model;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"testimonial-area pt-110 pb-105 text-center\">\r\n    <div class=\"container\">\r\n        <div class=\"row\">\r\n            <div class=\"testimonial-owl owl-theme owl-carousel\">\r\n");
#nullable restore
#line 10 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\Shared\Components\TestiMonial\Default.cshtml"
                 foreach (var item in testiMonials)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""col-md-8 col-md-offset-2 col-sm-12"">
                  
                        <div class=""single-testimonial"">
                            <div class=""testimonial-info"">
                               
                                <div class=""testimonial-img"">
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "02e06a967469d0c715262ad94bd463550d5c90625025", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 654, "~/img/testimonial/", 654, 18, true);
#nullable restore
#line 18 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\Shared\Components\TestiMonial\Default.cshtml"
AddHtmlAttributeValue("", 672, item.Image, 672, 11, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </div>\r\n                                <div class=\"testimonial-content\">\r\n                                    <p>");
#nullable restore
#line 21 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\Shared\Components\TestiMonial\Default.cshtml"
                                  Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                    <h4>");
#nullable restore
#line 22 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\Shared\Components\TestiMonial\Default.cshtml"
                                   Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                                    <h5>");
#nullable restore
#line 23 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\Shared\Components\TestiMonial\Default.cshtml"
                                   Write(item.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                </div>\r\n                                \r\n                            </div>\r\n                        </div>\r\n\r\n                    \r\n                </div>\r\n");
#nullable restore
#line 31 "C:\Users\Nijat Mammadli\source\repos\backend_project_asp\backend_project_asp\Views\Shared\Components\TestiMonial\Default.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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