using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duality.Components.Networking;
using Duality.Drawing;
using Duality.Editor;
using Duality.Properties;
using Duality.Resources;

namespace Duality.Components.Diagnostics
{
	/// <summary>
	/// A diagnostic <see cref="Duality.Component"/> that displays current network measurements.
	/// </summary>
	[RequiredComponent(typeof(NetworkingComponent))]
	[EditorHintCategory(CoreResNames.CategoryDiagnostics)]
	[EditorHintImage(CoreResNames.ImageProfileRenderer)] //TODO: Create a resource for this image
	public class NetworkStatsRenderer : Component, ICmpRenderer, ICmpUpdatable
	{
		[DontSerialize] private FormattedText textReport = null;
		[DontSerialize] private VertexC1P3T2[] textReportIconVert = null;
		[DontSerialize] private VertexC1P3T2[][] textReportTextVert = null;

		public float BoundRadius
		{
			get
			{
				return float.MaxValue;
			}
		}

		public void Draw(IDrawDevice device)
		{
			Canvas canvas = new Canvas(device);
			canvas.State.SetMaterial(new BatchInfo(DrawTechnique.Alpha, ColorRgba.White, null));

			//TODO: Do we need to calculate a better position for this?
			Rect textReportRect = new Rect(
				10,
				10,
				750,
				(int)device.TargetSize.Y - 20);

			if (this.textReport == null)
			{
				this.textReport = new FormattedText();
				this.textReport.Fonts = new[] { Font.GenericMonospace10 };
			}
			this.textReport.MaxWidth = (int)textReportRect.W;
			this.textReport.SourceText = "IP Address: " + this.GameObj.GetComponent<NetworkingComponent>().IPAddress; //TODO: populate with stuff!

			canvas.DrawText(this.textReport, ref this.textReportTextVert, ref this.textReportIconVert, textReportRect.X, textReportRect.Y, drawBackground: true);
		}

		public bool IsVisible(IDrawDevice device)
		{
			return 
				DualityApp.ExecContext == DualityApp.ExecutionContext.Game &&
				(device.VisibilityMask & VisibilityFlag.ScreenOverlay) != VisibilityFlag.None &&
				(device.VisibilityMask & VisibilityFlag.AllGroups) != VisibilityFlag.None;
		}

		public void OnUpdate()
		{
			//TODO: calculate networking stats, etc
		}
	}
}
