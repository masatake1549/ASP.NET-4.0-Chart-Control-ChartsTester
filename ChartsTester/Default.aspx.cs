using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ChartsTester
{
	public partial class Default : System.Web.UI.Page
	{
		Dictionary<DateTime, int> testData = new Dictionary<DateTime, int>();

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (!IsPostBack)
			{
				// bind chart type names to ddl
				ddlChartType.DataSource = Enum.GetNames(typeof(SeriesChartType));
				ddlChartType.DataBind();
			}

			DataBind();
		}

		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);

			// define test data
			Random rnd = new Random(Guid.NewGuid().GetHashCode());

			for (int i = 0; i < Convert.ToInt32(rblValueCount.SelectedValue); i++)
			{
				testData.Add(DateTime.Now.AddDays(i), rnd.Next(1, 50));
			}

			cTestChart.Series["Testing"].Points.DataBind(testData, "Key", "Value", string.Empty);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			// update chart rendering
			cTestChart.Series["Testing"].ChartTypeName = ddlChartType.SelectedValue;
			cTestChart.ChartAreas[0].Area3DStyle.Enable3D = cbUse3D.Checked;
			cTestChart.ChartAreas[0].Area3DStyle.Inclination = Convert.ToInt32(rblInclinationAngle.SelectedValue);
		}
	}
}