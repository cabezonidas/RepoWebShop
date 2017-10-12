using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RepoWebShop.TagHelpers
{
    public class TimeFrameTagHelper : TagHelper
    {
        public int DayId { get; set; }
        public string ClassName { get; set; }
        public string ActionType { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.Attributes.SetAttribute("class", "table-addtimeframe");
            output.Content.AppendHtml(
                @"<tbody>
                    <tr>
                        <td>
                            <div class='input-group bootstrap-timepicker timepicker'>" +
                                $"<input id='{DayId}' type='text' class='form-control input-small startingat {ClassName}'>" +
                                @"<span class='input-group-addon'><i class='glyphicon glyphicon-time'></i></span>
                            </div>
                        </td>
                        <td>
                            <div class='input-group bootstrap-timepicker timepicker'>" +
                                $"<input id='{DayId}' type='text' class='form-control input-small finishingat {ClassName}'>" +
                                @"<span class='input-group-addon'><i class='glyphicon glyphicon-time'></i></span>
                            </div>
                        </td>
                        <td>" +
                            $"<a id='{DayId}' class='{ActionType}' href='#'>Agregar</a>" +
                        @"</td>
                    </tr>
                    <tr>" + 
                        $"<td colspan='3' id='{ClassName}-{DayId}'></td>" +
                    @"</tr>
                </tbody>"
            );
        }
    }
}


