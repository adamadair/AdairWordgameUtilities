using System.ComponentModel;
using System.Runtime.CompilerServices;
using WordamentSolver.Annotations;

namespace WordamentSolver
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WordamentModel WordamentModel { get; set; }

        public MainViewModel()
        {
            WordamentModel = new WordamentModel();
            
        }
    }
}
