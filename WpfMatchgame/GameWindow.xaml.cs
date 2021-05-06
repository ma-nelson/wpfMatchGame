using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using WpfMatchgame.Controls;

namespace WpfMatchgame
{
    
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
        }
        public List<Card> cards { get; set; } = new List<Card>();
        private Random random = new Random();
        private DispatcherTimer timer = new DispatcherTimer();

        private List<string> symbols = new List<string>()
            {
            "!", "!", "N", "N", ",", ",",
            "b", "b", "v", "v", "w", "w",
        };

        private Card card1 = null;
        private Card card2 = null;

        public void RegisterCard(Card card)
        {
            card.State = Card.eState.Idle;
            int r = random.Next(symbols.Count);
            card.Symbol = symbols[r];

            symbols.RemoveAt(r);
            cards.Add(card);
        }

        public void SelectCard(Card card)
        {
            if (card1 == null)
            {
                card1 = card;
            }
            else
            {
                if (card2 == null)
                {
                    card2 = card;

                    if (card1.Symbol == card2.Symbol)
                    {
                        card1.State = Card.eState.Matched;
                        card2.State = Card.eState.Matched;
                        card1 = card2 = null;
                    }
                    else
                    {
                        
                        foreach (Card c in cards)
                        {
                            if (c.State == Card.eState.Idle)
                            {
                                c.State = Card.eState.Inactive;
                            }
                        }
                        timer.Start();
                    }
                }
            }

        }
      
        private void TimerTick(object sender, EventArgs e)
        {
            timer.Stop();
            foreach (Card c in cards)
            {
                if (c.State != Card.eState.Matched)
                {
                    c.State = Card.eState.Idle;
                }
            }
            card1 = card2 = null;
        }
    }
}
