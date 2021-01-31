using System;
using System.Linq;
using System.Windows.Forms;

namespace GameEngine
{

	public class Canvas : Form
	{
		public static event EventHandler OnNewGameClick;
		public static event EventHandler<bool> OnPauseResumeClick;
		public static event EventHandler<bool> OnShowMapClick;
		public static event EventHandler OnExitClick;

		public Canvas()
		{
			DoubleBuffered = true;
			InitializeComponent();
		}

		void InitializeComponent()
		{
			var toolStrip = new ToolStrip();
			toolStrip.SuspendLayout();
			SuspendLayout();
			toolStrip.BackColor = System.Drawing.Color.Transparent;
			toolStrip.GripStyle = ToolStripGripStyle.Hidden;
			toolStrip.Items.AddRange(new[] { GetToolbar() });

			Controls.Add(toolStrip);
			toolStrip.ResumeLayout(false);
			toolStrip.PerformLayout();
			toolStrip.Renderer = new MyToolStripRenderer();
			ResumeLayout(false);
			PerformLayout();
		}

		ToolStripItem GetToolbar()
		{
			var toolbar = new ToolStripDropDownButton
			{
				BackColor = System.Drawing.Color.White,
				DisplayStyle = ToolStripItemDisplayStyle.Text
			};
			toolbar.DropDownItems.AddRange(_toolstripItemsMap.Select(x => x.MenuItem).ToArray());
			toolbar.Text = "File";
			return toolbar;
		}

		readonly IToolstripItem[] _toolstripItemsMap = new IToolstripItem[]{
			new ToolstripItem("New Game", ()=> OnNewGameClick?.Invoke(null, null)),
			new BooleanToolstripItem("Pause", (ToolStripMenuItem menuItem, bool isChecked) => {
				menuItem.Text = isChecked ? "Resume" : "Pause";
				OnPauseResumeClick?.Invoke(null, isChecked);
			}),
			new BooleanToolstripItem("Show map", (ToolStripMenuItem _, bool isChecked) => OnShowMapClick?.Invoke(null, isChecked)),
			new ToolstripItem("Exit", ()=>OnExitClick?.Invoke(null, null) ),
		};

		public interface IToolstripItem
		{
			ToolStripMenuItem MenuItem { get; set; }
		}

		public class ToolstripItem : IToolstripItem
		{
			public ToolstripItem(string text, Action onClick)
			{
				MenuItem = new ToolStripMenuItem { Text = text };
				MenuItem.Click += (object sender, EventArgs e) => onClick();
			}
			public ToolStripMenuItem MenuItem { get; set; }
		}

		public class BooleanToolstripItem : IToolstripItem
		{
			public BooleanToolstripItem(string text, Action<ToolStripMenuItem, bool> onChecked = null)
			{
				MenuItem = new ToolStripMenuItem { Text = text, CheckOnClick = true, };
				MenuItem.CheckedChanged += (object sender, EventArgs e) => onChecked((ToolStripMenuItem)sender, ((ToolStripMenuItem)sender).Checked);
			}
			public ToolStripMenuItem MenuItem { get; set; }
		}

		public class MyToolStripRenderer : ToolStripSystemRenderer
		{
			//This fixes the system bug for white underline https://stackoverflow.com/questions/1918247/how-to-disable-the-line-under-tool-strip-in-winform-c
			protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
			{
				//base.OnRenderToolStripBorder(e);
			}
		}
	}
}
