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
        public MainWindow()
        {
            InitializeComponent();

            

            DataContext = new ApplicationViewModel();
            ((ApplicationViewModel)DataContext).OnGraphSolved += DrawGraph;
        }

        private void DrawGraph(List<double> points)
        {
            Canvas_DrawArea.Children.Clear();

            Polyline polyline = new Polyline();
            polyline.Stroke = Brushes.Red;
            polyline.StrokeThickness = 2;

            int width = (int)Canvas_DrawArea.Width; // ширина области
            int height = (int)Canvas_DrawArea.Height; // высота области
            int ofsx = (int)VisualOffset.X; // координата x верхнего левого угла области
            int ofsy = (int)VisualOffset.Y;  // координата y верхнего левого угла области
            double ymin = points.Min(); // минимальное значение функции на промежутке
            double ymax = points.Max(); // максимальное значения функции на промежутке
            double xmin = double.Parse(TB_MinX.Text.Replace(".", ",")); // минимальное значение х на промежутке
            double xmax = double.Parse(TB_MaxX.Text.Replace(".", ",")); // максимальное значение х на промежутке
            double acc = double.Parse(TB_Accuracy.Text.Replace(".", ",")); // знаение точности

            int centerx = width / 2; // центр области х
            double xproj = width / (xmax - xmin); // размер пикселя на значение
            int centery = height / 2; // центр области у
            double yproj = height / (xmax - xmin); // размер пикселя на значение

            double cx = xmin + ofsx;
            for (int i = 0; i < points.Count; i++)
            {
                double cy = points[i] + ofsy;
                cy *= -1; // координаты элементов в wpf имеет противоположную направленность относительно декартовых координат
                int x1coord = (int)(cx * xproj) + centerx;
                int y1coord = (int)(cy * yproj) + centery;

                cx += acc;

                if (y1coord >= height || y1coord <= ofsy)
                    continue;

                polyline.Points.Add(new Point(x1coord, y1coord));
            }

            Canvas_DrawArea.Children.Add(polyline);
        }
    }
}