using System.Text;
using System.Web;

using NLog;
using NLog.LayoutRenderers;

namespace Gems.Logging.NLog.Extensions.LayoutRenderers.Web
{
	[LayoutRenderer("gems-aspnet-request-url")]
	public class AspNetRequestUrlLayoutRenderer : LayoutRenderer
	{
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			var context = HttpContext.Current;

			if (context == null)
				return;

			builder.Append(context.Request.Url);
		}
	}
}
