using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RandImportGenerator.Web.Utility
{
    public static class HtmlHelpers
    {
        public static IHtmlString RenderJsonConfigTag(this HtmlHelper html, string scriptId, string json)
        {
            return html.Raw(RenderJsonConfigToString(scriptId, json));
        }

        public static IHtmlString RenderJsonConfigTags(this HtmlHelper html, Dictionary<string, string> dict)
        {
            var sb = new StringBuilder();
            foreach (var d in dict)
            {
                sb.AppendLine(RenderJsonConfigToString(d.Key, d.Value));
            }

            return html.Raw(sb.ToString());
        }

        private static string RenderJsonConfigToString(string scriptId, string json)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("<script id='{0}' type='application/json'>", scriptId));
            sb.AppendLine(json);
            sb.AppendLine("</script>");

            return sb.ToString();
        }
    }
}