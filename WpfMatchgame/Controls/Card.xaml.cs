using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace WpfMatchgame.Controls
{
  
    public partial class Card : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum eState
        {
            Inactive,
            Idle,
            Flipped,
            Matched
        }
        public Card()
        {
            InitializeComponent();
            DataContext = this;

            this.Loaded += Card_Loaded;
        }
        public GameWindow Owner { get; set; }
        private eState state { get; set; } = eState.Inactive;


        public eState State {
            get { return state; }
            set
            {
                if(value != state)
                {
                    state = value;
                    Interactable = (state == eState.Idle);
                    Show = (state == eState.Flipped || state == eState.Matched);
                    NotifyPropertyChanged("State");

                }
            }
        }

        public bool Show {
            set
            {
                if (value)
                {
                    lblSymbol.Visibility = Visibility.Visible;
                    imgCard.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblSymbol.Visibility = Visibility.Hidden;
                    imgCard.Visibility = Visibility.Visible;
                }

            }
        }


        private bool interactable;

        public bool Interactable
        {
            get { return interactable; }
            set
            {
                if (value != interactable)
                {
                    interactable = value;
                    NotifyPropertyChanged("Interactable");
                }
            }

        }

        private string symbol;

        public string Symbol { get { return symbol; }
            set
             {
                if (value != Symbol)
                {
                    symbol = value;
                    NotifyPropertyChanged("Symbol");
                }
            }
        }
        private void Card_Loaded(object sender, RoutedEventArgs e) 
        {
            Owner = (GameWindow)Window.GetWindow(this);
            Owner.RegisterCard(this);
        }

        private void btnCard_Click(object sender, RoutedEventArgs e)
        {
            State = eState.Flipped;
            Owner.SelectCard(this);
        }
    }
}
