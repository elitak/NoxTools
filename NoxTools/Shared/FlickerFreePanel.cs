using System.Windows.Forms;

public class FlickerFreePanel : Panel
{
	public FlickerFreePanel() : base()
	{
		SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
	}
}