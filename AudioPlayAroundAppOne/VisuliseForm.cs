using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AudioPlayAroundAppOne
{
    public class VisuliseForm: System.Windows.Forms.Form
    {
        private Chart _chart;

        public VisuliseForm(float[] array)
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea
            {
                Name = "ChartArea1"
            };
            _chart = new Chart {Dock = DockStyle.Fill};
            _chart.Legends.Clear();
            _chart.Series.Clear();
            _chart.ChartAreas.Add(chartArea1);
     

  
            _chart.Series.Add("wave");
            _chart.Series["wave"].ChartArea = "ChartArea1";
            _chart.Series["wave"].ChartType = SeriesChartType.FastLine;
            this.Controls.Add(_chart);
            _chart.Show();
            foreach (var item in array)
            {
                _chart.Series["wave"].Points.Add(item); 
            }
    
        }
    }
}