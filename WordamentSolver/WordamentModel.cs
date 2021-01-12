using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using WordamentSolver.Annotations;
using WordGameLib;
using WordGameLib.Wordament;

namespace WordamentSolver
{
    public class WordamentModel : INotifyPropertyChanged
    {
        public WordamentModel()
        {
            _Cells = new string[16];
            for (int i = 0; i < 16; i++)
            {
                _Cells[i] = "";
            }

            ClearCommand = new RelayCommand(o => Clear());
            SolveCommand = new RelayCommand(o=>Solve(), n => CanSolve());
            SendSelectedCommand = new RelayCommand(o => SendSelected(), n => CanSendSelected());
            SendAllCommand = new RelayCommand(o => SendAll(), n => CanSendAll());
            SendToRemoteCommand = new RelayCommand(o => SendToRemote(), n => CanSendAll());

            _solver = new WordamentSolve();
            _solver.LoadDictionary("TWL06.txt");
            SolutionWords = new ObservableCollection<string>();
            _className = "ApplicationFrameWindow";
            _windowName = "Microsoft Ultimate Word Games";
            IpAddress = "127.0.0.1";
            SendCount = "100";
        }

        private WordamentSolve _solver;
        private string[] _Cells;
        private bool _text00IsFocused;
        private ICommand _sendSelectedCommand;
        private ICommand _sendAllCommand;

        public string Text00
        {
            get => _Cells[0];
            set
            {
                if (value == _Cells[0]) return;
                _Cells[0] = value;
                OnPropertyChanged();
            }
        }

        public bool Text00IsFocused
        {
            get => _text00IsFocused;
            set
            {
                _text00IsFocused = value;
                OnPropertyChanged();
            }
        }

        public string Text01
        {
            get => _Cells[1];
            set
            {
                if (value == _Cells[1]) return;
                _Cells[1] = value;
                OnPropertyChanged();
            }
        }

        public string Text02
        {
            get => _Cells[2];
            set
            {
                if (value == _Cells[2]) return;
                _Cells[2] = value;
                OnPropertyChanged();
            }
        }

        public string Text03
        {
            get => _Cells[3];
            set
            {
                if (value == _Cells[3]) return;
                _Cells[3] = value;
                OnPropertyChanged();
            }
        }

        public string Text10
        {
            get => _Cells[4];
            set
            {
                if (value == _Cells[4]) return;
                _Cells[4] = value;
                OnPropertyChanged();
            }
        }

        public string Text11
        {
            get => _Cells[5];
            set
            {
                if (value == _Cells[5]) return;
                _Cells[5] = value;
                OnPropertyChanged();
            }
        }

        public string Text12
        {
            get => _Cells[6];
            set
            {
                if (value == _Cells[6]) return;
                _Cells[6] = value;
                OnPropertyChanged();
            }
        }

        public string Text13
        {
            get => _Cells[7];
            set
            {
                if (value == _Cells[7]) return;
                _Cells[7] = value;
                OnPropertyChanged();
            }
        }

        public string Text20
        {
            get => _Cells[8];
            set
            {
                if (value == _Cells[8]) return;
                _Cells[8] = value;
                OnPropertyChanged();
            }
        }

        public string Text21
        {
            get => _Cells[9];
            set
            {
                if (value == _Cells[9]) return;
                _Cells[9] = value;
                OnPropertyChanged();
            }
        }

        public string Text22
        {
            get => _Cells[10];
            set
            {
                if (value == _Cells[10]) return;
                _Cells[10] = value;
                OnPropertyChanged();
            }
        }

        public string Text23
        {
            get => _Cells[11];
            set
            {
                if (value == _Cells[11]) return;
                _Cells[11] = value;
                OnPropertyChanged();
            }
        }

        public string Text30
        {
            get => _Cells[12];
            set
            {
                if (value == _Cells[12]) return;
                _Cells[12] = value;
                OnPropertyChanged();
            }
        }

        public string Text31
        {
            get => _Cells[13];
            set
            {
                if (value == _Cells[13]) return;
                _Cells[13] = value;
                OnPropertyChanged();
            }
        }

        public string Text32
        {
            get => _Cells[14];
            set
            {
                if (value == _Cells[14]) return;
                _Cells[14] = value;
                OnPropertyChanged();
            }
        }

        public string Text33
        {
            get => _Cells[15];
            set
            {
                if (value == _Cells[15]) return;
                _Cells[15] = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _solutionWords;
        public ObservableCollection<string> SolutionWords
        {
            get => _solutionWords;
            set
            {
                _solutionWords = value;
                OnPropertyChanged();
            }
        }

        private string _selectedWord;
        private string _windowName;
        private string _className;
        private bool _waitThree;
        private string _ipAddress;
        private string _sendCount;
        private ICommand _clearCommand;
        private ICommand _solveCommand;
        private ICommand _sendToRemoteCommand;

        public string SelectedWord
        {
            get => _selectedWord;
            set
            {
                _selectedWord = value;
                OnPropertyChanged();
            }
        }

        public bool WaitThree
        {
            get => _waitThree;
            set
            {
                if (value == _waitThree) return;
                _waitThree = value;
                OnPropertyChanged();
            }
        }

        public string ClassName
        {
            get => _className;
            set
            {
                if (value == _className) return;
                _className = value;
                OnPropertyChanged();
            }
        }

        public string WindowName
        {
            get => _windowName;
            set
            {
                if (value == _windowName) return;
                _windowName = value; 
                OnPropertyChanged();
            }
        }

        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                if (value == _ipAddress) return;
                _ipAddress = value;
                OnPropertyChanged();
            }
        }

        public string SendCount
        {
            get => _sendCount;
            set
            {
                if (value == _sendCount) return;
                _sendCount = value;
                OnPropertyChanged();
            }
        }


        public ICommand ClearCommand
        {
            get => _clearCommand;
            set => _clearCommand = value;
        }

        public ICommand SolveCommand
        {
            get => _solveCommand;
            set => _solveCommand = value;
        }

        public ICommand SendSelectedCommand
        {
            get => _sendSelectedCommand;
            set => _sendSelectedCommand = value;
        }

        public ICommand SendAllCommand
        {
            get => _sendAllCommand;
            set => _sendAllCommand = value;
        }

        public ICommand SendToRemoteCommand
        {
            get => _sendToRemoteCommand;
            set => _sendToRemoteCommand = value;
        }

        public void Clear()
        {
            Text00 = "";
            Text01 = "";
            Text02 = "";
            Text03 = "";
            Text10 = "";
            Text11 = "";
            Text12 = "";
            Text13 = "";
            Text20 = "";
            Text21 = "";
            Text22 = "";
            Text23 = "";
            Text30 = "";
            Text31 = "";
            Text32 = "";
            Text33 = "";
            SolutionWords.Clear();
            Text00IsFocused = true;
        }

        public void Solve()
        {
            var words = _solver.FindWords(_Cells);
            SolutionWords = new ObservableCollection<string>(words);
        }

        public bool CanSolve()
        {
            return _Cells.All(c => c.Trim() != "");
        }

        private List<string> GetGridElements()
        {
            return new List<string>
            {
                Text00.Trim(),

            };
        }

        public bool CanSendSelected()
        {
            return !string.IsNullOrWhiteSpace(SelectedWord);
        }

        public void SendSelected()
        {
            if (WaitThree)
            {
                Wait(3);
            }
            SendKeysUtil.SendKeyStrokes(ClassName, WindowName, SelectedWord);
        }

        public bool CanSendAll()
        {
            return SolutionWords.Count > 0;
        }

        public void SendAll()
        {
            if (WaitThree)
            {
                Wait(3);
            }

            foreach (var word in SolutionWords)
            {
                SendKeysUtil.SendKeyStrokes(ClassName, WindowName, word);
                Thread.Sleep(100);
            }
        }

        public void SendToRemote()
        {
            var startIndex = 0;
            if (!string.IsNullOrWhiteSpace(SelectedWord))
            {
                startIndex = SolutionWords.IndexOf(SelectedWord);
            }

            var sendCount = int.Parse(SendCount);
            var index = 0;
            if (WaitThree)
            {
                Wait(3);
            }
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                var port = 13000;
                var client = new TcpClient(IpAddress, port);

                var stream = client.GetStream();

                while (index < sendCount && startIndex + index < SolutionWords.Count)
                {
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(SolutionWords[startIndex + index]);
                    stream.Write(data, 0, data.Length);
                    data = new byte[256];

                    // String to store the response ASCII representation.
                    var responseData = string.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    var bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);
                    index++;
                }
                // Close everything.
                stream.Close();
                client.Close();
                MessageBox.Show("Done!");
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show($"ArgumentNullException: {e}");
            }
            catch (SocketException e)
            {
                MessageBox.Show($"SocketException: {e}");
            }
        }

        private void Wait(int n)
        {
            Thread.Sleep(new TimeSpan(0,0,n));
        }


        public event PropertyChangedEventHandler PropertyChanged;



        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}