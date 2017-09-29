using Microsoft.AspNetCore.Razor.TagHelpers;
using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.TagHelpers
{
    public class WorkingHoursTagHelper : TagHelper
    {
        public IEnumerable<IWorkingHours> WorkingHours { get; set; }
        public int DayId { get; set; }
        public string HoursType { get; set; }
        public string DeleteClass { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            //output.Attributes.SetAttribute("class", "timeframestable table table-striped table-condensed");
            output.Attributes.SetAttribute("class", "timeframestable");
            output.Attributes.SetAttribute("style", "vertical-align:top; width:100%;");
            output.Content.AppendHtml(
                @"<thead>
                    <tr>
                        <td>Id</td>
                        <td>Abre</td>
                        <td>Cierra</td>
                        <td></td>
                    </tr>
                </thead>
                <tbody>" +
                    String.Concat(
                        WorkingHours.Where(x => x.DayId == DayId).Select(x =>
                            String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                                x.Id,
                                x.StartingAt.ToString("hh\\:mm"),
                                x.StartingAt.Add(x.Duration).ToString("hh\\:mm"),
                                $"<a id='{x.Id}' class='{DeleteClass}' href='#'>Eliminar</a>")
                        )
                    ) +
                "</tbody>"
            );
        }
    }
}
