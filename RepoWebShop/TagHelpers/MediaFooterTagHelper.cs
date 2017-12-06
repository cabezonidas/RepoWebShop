using Microsoft.AspNetCore.Razor.TagHelpers;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.TagHelpers
{
    public class MediaFooterTagHelper : TagHelper
    {
        public IEnumerable<MediaViewModel> Media { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.Attributes.SetAttribute("id", "contact");
            output.Attributes.SetAttribute("class", "contact");
            output.Content.AppendHtml(
            @"<div class='container' >
                <div class='row'>
                    <div class='col-lg-6 col-md-6 hidden-sm hidden-xs text-center'>
                        <h2 class='section-heading'>
                            Seguinos
                            <i class='fa fa-heart fa-beat'></i>
                        </h2>
                    </div>
                    <div class='col-lg-6 col-md-6 col-sm-12 col-xs-12 text-center'>
                        <ul class='list-inline list-social'>"
                            +
                            String.Concat(Media.Select(x =>
                            $"<li class='list-inline-item {x.Class}'>" +
                                $"<a href='{x.Ref}'>" +
                                    $"<i class='fa {x.Icon}'></i>" + 
                                @"</a>
                            </li>"))
                            +
                            @"<li class='list-inline-item'>
                                <a href='http://qr.afip.gob.ar/?qr=-3Dafrs7S3zSZ4ok4IozMQ,,' target='_F960AFIPInfo'>
                                    <img src='http://www.afip.gob.ar/images/f960/DATAWEB.jpg' border='0'>
                                </a>
                            </li>"
                            +
                        @"</ul>
                    </div>
                    <div class='hidden-lg hidden-md hidden-sm col-xs-12 text-center'>
                        <h2 class='section-heading'>
                            &zwnj;
                        </h2>
                    </div>
                </div>
            </div>");
        }
    }
}
