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
using System.Windows.Media.Media3D;
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

        // ссылка на данные графика
        private List<Point> m_Points;

        public MainWindow()
        {
            InitializeComponent();



            DataContext = new ApplicationViewModel();
            ((ApplicationViewModel)DataContext).OnGraphSolved += InitDrawingGraph;
            ((ApplicationViewModel)DataContext).OnError += ErrorHandler;

            TB_MaxY.LostFocus += TBMaxYLostFocus;
            TB_MinY.LostFocus += TBMinYLostFocus; ;
        }

        private void TBMinYLostFocus(object sender, RoutedEventArgs e)
        {
            if(m_Points != null)
            {
                try
                {
                    DrawGraph(m_Points, double.Parse(TB_MinY.Text), double.Parse(TB_MaxY.Text));
                }
                catch (Exception)
                {
                    ErrorHandler(6, "");
                }
            }
            else
            {
                ErrorHandler(3, "");
            }    
        }

        private void TBMaxYLostFocus(object sender, RoutedEventArgs e)
        {
            if (m_Points != null)
            {
                try
                {
                    DrawGraph(m_Points, double.Parse(TB_MinY.Text), double.Parse(TB_MaxY.Text));
                }
                catch (Exception)
                {
                    ErrorHandler(6, "");
                }
            }
            else
            {
                ErrorHandler(3, "");
            }
        }

        // обработчик ошибок
        // 0 - ошика вычисления
        // 1 - ошибка установки минимального значения иксов
        // 2 - ошибка установки минимального значения иксов
        // 3 - изменение игреков, а функция еще не решена
        // 4 -  минимальный игрек больше максимульного
        // 5 - максимальный игрек меньше минимального
        // 6 - неправильный формат
        private void ErrorHandler(int code, string errmsg)
        {
            switch (code)
            {
                case 0:
                    MessageBox.Show($"Ошибка расчета функции\nПодробнее: {errmsg}", "Ошибка");
                    break;
                case 1:
                    MessageBox.Show("Значение должно быть меньше значения максимального икса");
                    break;
                case 2:
                    MessageBox.Show("Значение должно быть больше значения минимального икса");
                    break;
                case 3:
                    double _var = 0;
                    if (double.TryParse(TB_MaxY.Text, out _var) || double.TryParse(TB_MinY.Text, out _var))
                    {
                        MessageBox.Show("Вы не можете изменить значения y, до того как решена функция");
                        TB_MaxY.Text = "-";
                        TB_MinY.Text = "-";
                    }
                    break;
                case 4:
                    MessageBox.Show("Значение должно быть меньше значения максимального игрека");
                    TB_MinY.Text = (double.Parse(TB_MaxY.Text) - 1).ToString();
                    break;
                case 5:
                    MessageBox.Show("Значение должно быть больше значения минимального игрека");
                    TB_MaxY.Text = (double.Parse(TB_MinY.Text) + 1).ToString();
                    break;
                case 6:
                    MessageBox.Show("Неправильный формат");
                    break;
                default:
                    MessageBox.Show($"Ошибка\nПодробнее: {errmsg}", "Ошибка");
                    break;
            }
        }

        private void InitDrawingGraph(List<Point> points)
        {
            m_Points = points;

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

            TB_MaxY.Text = ymax.ToString();
            TB_MinY.Text = ymin.ToString();

            DrawGraph(points, ymin, ymax);
        }

        private void DrawGraph(List<Point> points, double ymin, double ymax)
        {
            Canvas_DrawArea.Children.Clear();

            // основная линия графика
            Polyline polyline = new Polyline();
            polyline.Stroke = Brushes.Red;
            polyline.StrokeThickness = 2;

            int width = (int)Canvas_DrawArea.Width; // ширина области
            int height = (int)Canvas_DrawArea.Height; // высота области

            double xmin = double.Parse(TB_MinX.Text.Replace(".", ",")); // минимальное значение х на промежутке
            double xmax = double.Parse(TB_MaxX.Text.Replace(".", ",")); // максимальное значение х на промежутке

            double xproj = width / (xmax - xmin); // размер пикселя на значение
            double yproj = height / (ymax - ymin); // размер пикселя на значение
            int centerx = (int)(width / 2 - (xmax + xmin) / 2 * xproj); // центр области х
            int centery = height - (int)(height / 2 - (ymax + ymin) / 2 * yproj); ; // центр области у

            // отрисовка осей
            DrawAxes(centery, centerx, xmin, xmax, ymin, ymax, xproj, yproj);

            for (int i = 0; i < points.Count; i++)
            {
                double cx = points[i].X;
                double cy = points[i].Y;
                cy *= -1; // координаты элементов в wpf имеет противоположную направленность относительно декартовых координат

                // если значению отсутствует делаем разрыв
                if (double.IsNaN(cx) && !m_MathGap)
                {
                    m_MathGap = true;
                    Canvas_DrawArea.Children.Add(polyline);
                    polyline = new Polyline();
                    polyline.Stroke = Brushes.Red;
                    polyline.StrokeThickness = 2;
                }

                if (m_MathGap && !double.IsNaN(cx))
                    m_MathGap = false;

                int x1coord = (int)(cx * xproj) + centerx;
                int y1coord = (int)(cy * yproj) + centery;

                if ((y1coord >= height || y1coord <= 0) || (x1coord >= width || x1coord <= 0))
                    continue;

                polyline.Points.Add(new Point(x1coord, y1coord));
            }

            Canvas_DrawArea.Children.Add(polyline);
        }

        // отрисовка осей
        private void DrawAxes(int centery, int centerx, double xmin, double xmax, double ymin, double ymax, double xproj, double yproj)
        {
            int width = (int)Canvas_DrawArea.Width; // ширина области
            int height = (int)Canvas_DrawArea.Height; // высота области

            // центры расположения осей
            int cx = centerx;
            int cy = centery;
            if (xmin > 0) cx = 0;
            if (xmax < 0) cx = width;
            if (ymin > 0) cy = height;
            if (ymax < 0) cy = 0;

            // x
            Line line = new Line(); line.Stroke = Brushes.Black; line.StrokeThickness = 2;
            line.X1 = 0; line.X2 = width; line.Y1 = cy; line.Y2 = cy;
            Canvas_DrawArea.Children.Add(line);
            double step = Math.Pow(5, Math.Floor(Math.Log10(xmax - xmin)) - 1);
            if (step <= 0) step = 1;
            for (double i = xmin; i <= xmax + 0.00001; i += step)
            {
                line = new Line(); line.Stroke = Brushes.Black; line.StrokeThickness = 2;
                line.X1 = (int)(i * xproj) + centerx; line.X2 = (int)(i * xproj) + centerx; line.Y1 = cy - 2; line.Y2 = cy + 2;

                // метка для значения x
                int decimals = (int)Math.Log(step, 0.1) + 1;
                if (decimals <= 0)
                    decimals = 1;
                if (decimals > 28)
                    decimals = 28;
                Label l = new Label(); l.FontSize = 8; l.Content = Math.Round((decimal)i, decimals).ToString();
                // вычисляем размер текста до рендера, чтобы отцентрировать метку
                l.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size s = l.DesiredSize;
                l.Margin = new Thickness((int)(i * xproj) + centerx - s.Width / 2, cy + 2, 0, 0);

                Canvas_DrawArea.Children.Add(line);
                Canvas_DrawArea.Children.Add(l);
            }
            // y
            line = new Line(); line.Stroke = Brushes.Black; line.StrokeThickness = 2;
            line.X1 = cx; line.X2 = cx; line.Y1 = 0; line.Y2 = height;
            Canvas_DrawArea.Children.Add(line);
            step = Math.Pow(5, Math.Floor(Math.Log10(ymax - ymin)) - 1);
            if (step <= 0) step = 1;
            for (double i = ymin; i <= ymax + 0.00001; i += step)
            {
                line = new Line(); line.Stroke = Brushes.Black; line.StrokeThickness = 2;
                line.X1 = cx - 2; line.X2 = cx + 2; line.Y1 = (int)(i * -1 * yproj) + centery; line.Y2 = (int)(i * -1 * yproj) + centery;

                // метка для значения y
                int decimals = (int)Math.Log(step, 0.1) + 1;
                if (decimals <= 0)
                    decimals = 1;
                if (decimals > 28)
                    decimals = 28;
                Label l = new Label(); ; l.FontSize = 8; l.Content = Math.Round((decimal)i, decimals).ToString();
                // вычисляем размер текста до рендера, чтобы отцентрировать метку
                l.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size s = l.DesiredSize;
                l.Margin = new Thickness(cx + 2, (int)(i * yproj * -1) + centery - s.Height / 2, 0, 0);

                Canvas_DrawArea.Children.Add(line);
                Canvas_DrawArea.Children.Add(l);
            }
        }
    }
}