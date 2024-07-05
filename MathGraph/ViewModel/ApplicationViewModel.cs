using MathGraph.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MathGraph.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        private ISolver m_Solver;

        public double DrawAreaXMinRange
        {
            get => m_Solver.AreaRange.X;
            set
            {
                if (value < m_Solver.AreaRange.Y)
                {
                    m_Solver.AreaRange = new Vector2((float)value, m_Solver.AreaRange.Y);
                    AutoAccuracyEval();
                    OnPropertyChanged("DrawAreaXMinRange");
                }
                else
                {
                    DrawAreaXMinRange = DrawAreaXMaxRange - 1;
                    OnError?.Invoke(1, "");
                }   
            }
        }

        public double DrawAreaXMaxRange
        {
            get => m_Solver.AreaRange.Y;
            set
            {
                if (value > m_Solver.AreaRange.X)
                {
                    m_Solver.AreaRange = new Vector2(m_Solver.AreaRange.X, (float)value);
                    AutoAccuracyEval();
                    OnPropertyChanged("DrawAreaXMaxMRange");
                }
                else
                {
                    DrawAreaXMaxRange  = DrawAreaXMinRange + 1;
                    OnError?.Invoke(2, "");
                }
            }
        }

        public double Accuracy
        {
            get => m_Solver.Accuracy;
            set
            {
                if(value > 0)
                {
                    m_Solver.Accuracy = value;
                }
                else
                {
                    OnError?.Invoke(7, "");
                    AutoAccuracyEval();
                }
                OnPropertyChanged("Accuracy");
            }
        }

        public string Function
        {
            get => m_Solver.Function;
            set
            {
                m_Solver.Function = value;
                OnPropertyChanged("Function");
            }
        }

        private BaseCommand m_SolveDrawCommand;
        public BaseCommand SolveDrawCommand
        {
            get => m_SolveDrawCommand;
        }

        public Action<List<Point>> OnGraphSolved { get; set; }
        // обработчик ошибок
        // 0 - ошика вычисления
        // 1 - ошибка установки минимального значения иксов
        // 2 - ошибка установки минимального значения иксов
        public Action<int, string> OnError { get; set; }

        public ApplicationViewModel()
        {
            m_Solver = new Solver();

            m_SolveDrawCommand = new BaseCommand()
            {
                Action = (object? param) =>
                {
                    try { m_Solver.SolveGraph(); }
                    catch (Exception e) { OnError?.Invoke(0, e.Message); }
                    OnGraphSolved?.Invoke(m_Solver.GetGraph());
                    }
            };
        }

        // посчитывает аккуратность, чтобы каждое значение x занимало 1 пиксель
        private void AutoAccuracyEval()
        {
            Vector2 range = m_Solver.AreaRange;
            float drange = Math.Abs(range.X - range.Y);
            Accuracy = drange / App.MAP_AREA_WIDTH;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
