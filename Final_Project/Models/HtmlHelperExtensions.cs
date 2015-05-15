using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Text;
using System.Web.Mvc;


namespace Final_Project.Models
{
    public static class HtmlHelperExtensions
    {
        //public static MvcHtmlString HiddenFieldsListFor(this HtmlHelper htmlHelper, Expression expression)
        //{
        //    var name = ExpressionHelper.GetExpressionText(expression);
        //    var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
        //    return HiddenFieldsList(htmlHelper, name, metadata.Model as string);
        //}
 
        private static MvcHtmlString HiddenFieldsList(this HtmlHelper helper, string nameValuePairPropertyName, string nameValuePairPayload)
        {
            var stringBuilder = new StringBuilder();
            var nameValuePairs = nameValuePairPayload.Split('&').ToList();
            nameValuePairs.ForEach(nvp => stringBuilder.Append(CreateHiddenInput(nvp).ToString(TagRenderMode.SelfClosing)));
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "hiddenFields");
            div.InnerHtml = stringBuilder.ToString();
            return MvcHtmlString.Create(div.ToString());
 
        }
 
        private static TagBuilder CreateHiddenInput(string nameValuePair)
        {
            var hiddenInput = new TagBuilder("input");
            var nvpPortions = nameValuePair.Split('=');
            hiddenInput.MergeAttribute("type", "hidden");
            hiddenInput.MergeAttribute("id", nvpPortions[0]);
            hiddenInput.MergeAttribute("name", nvpPortions[0]);
            hiddenInput.MergeAttribute("value", nvpPortions[1]);
            return hiddenInput;
        }
    }
}