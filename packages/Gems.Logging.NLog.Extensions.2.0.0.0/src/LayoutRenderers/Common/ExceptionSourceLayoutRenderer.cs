using System.Text;

using NLog;
using NLog.LayoutRenderers;

namespace Gems.Logging.NLog.Extensions.LayoutRenderers.Common
{
	[LayoutRenderer("gems-exception-source")]
	public class ExceptionSourceLayoutRenderer : LayoutRenderer
	{
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			var ex = logEvent.Exception;

			if (ex == null)
				return;

			builder.Append(ex.Source);
		}
	}
}
