using MathGraph.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                m_Solver.AreaRange = new Vector2((float)value, m_Solver.AreaRange.Y);
                OnPropertyChanged("DrawAreaXMinRange");
            }
        }

        public double DrawAreaXMaxRange
        {
            get => m_Solver.AreaRange.Y;
            set
            {
                m_Solver.AreaRange = new Vector2(m_Solver.AreaRange.X, (float)value);
                OnPropertyChanged("DrawAreaXMaxMRange");
            }
        }

        public double Accuracy
        {
            get => m_Solver.Accuracy;
            set
            {
                m_Solver.Accuracy = value;
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
        public ApplicationViewModel()
        {
            m_Solver = new Solver();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
