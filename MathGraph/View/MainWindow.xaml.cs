using MathGraph.Model;
using MathGraph.ViewModel;
using System.Data;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MathGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // разрыв графика функции
        private bool m_MathGap;

        public MainWindow()
        {
            InitializeComponent();

            

            DataContext = new ApplicationViewModel();
            ((ApplicationViewModel)DataContext).OnGraphSolved += DrawGraph;
        }

        private void DrawGraph(List<Point> points)
        {
            Canvas_DrawArea.Children.Clear();

            Polyline polyline = new Polyline();
            polyline.Stroke = Brushes.Red;
            polyline.StrokeThickness = 2;

            int points_count = points.Count;
            int width = (int)Canvas_DrawArea.Width; // ширина области
            int height = (int)Canvas_DrawArea.Height; // высота области
            int ofsx = (int)Canvas_DrawArea.Margin.Left; // координата x верхнего левого угла области
            int ofsy = (int)Canvas_DrawArea.Margin.Top;  // координата y верхнего левого угла области

            double ymin = 999999; // минимальное значение функции на промежутке
            double ymax = -999999; // максимальное значения функции на промежутке
            for (int i = 0; i < points.Count; i++)
            {
                double val = points[i].Y;
                if (val > ymax)
                    ymax = val;
                if (val < ymin)
                    ymin = val;
            }
            
            double xmin = double.Parse(TB_MinX.Text.Replace(".", ",")); // минимальное значение х на промежутке
            double xmax = double.Parse(TB_MaxX.Text.Replace(".", ",")); // максимальное значение х на промежутке
            double acc = double.Parse(TB_Accuracy.Text.Replace(".", ",")); // точность при подсчете

            int centerx = width / 2; // центр области х
            double xproj = width / (xmax - xmin); // размер пикселя на значение
            int centery = height / 2; // центр области у
            double yproj = height / (xmax -xmin); // размер пикселя на значение

            for (int i = 0; i < points.Count; i++)
            {
                double cx = points[i].X;
                double cy = points[i].Y;
                cy *= -1; // координаты элементов в wpf имеет противоположную направленность относительно декартовых координат

                if (double.IsNaN(cx) && !m_MathGap)
                {
                    m_MathGap = true;
                    Canvas_DrawArea.Children.Add(polyline);
                    polyline = new Polyline();
                    polyline.Stroke = Brushes.Red;
                    polyline.StrokeThickness = 2;
                }

                if(m_MathGap && !double.IsNaN(cx))
                    m_MathGap = false;

                int x1coord = (int)(cx * xproj) + centerx;
                int y1coord = (int)(cy * yproj) + centery;

                if (y1coord >= height || y1coord <= ofsy)
                    continue;
                if (x1coord >= width || x1coord <= 0)
                    continue;

                polyline.Points.Add(new Point(x1coord, y1coord));
            }

            Canvas_DrawArea.Children.Add(polyline);
        }
    }
}