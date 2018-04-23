using System;
using System.Linq;
using System.Text;
using System.Web;

using NLog;
using NLog.Config;
using NLog.LayoutRenderers;

namespace Gems.Logging.NLog.Extensions.LayoutRenderers.Web
{
	[LayoutRenderer("gems-aspnet-form-collection")]
	public class AspNetRequestFormCollectionLayoutRenderer : LayoutRenderer
	{
		private static readonly string[] SkipValues = new[]
		{
			null,
			string.Empty,
			"__VIEWSTATE",
			"__EVENTVALIDATION",
			"__VSTATE"
		};

		private static readonly string DefaultSeparator = Environment.NewLine;

		[DefaultParameter]
		public string Separator { get; set; }

		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			var context = HttpContext.Current;

			if (context == null)
				return;

			for (int ii = 0; ii < context.Request.Form.Count; ii++)
			{
				var key = context.Request.Form.GetKey(ii);

				if (SkipValues.Contains(key))
					continue;

				if (ii > 0)
					builder.Append(Separator ?? DefaultSeparator);

				builder.Append(key + "=" + context.Request.Form[ii]);
			}
		}
	}
}
