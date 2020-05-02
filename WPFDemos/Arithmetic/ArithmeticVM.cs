using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDemos
{
    /// <summary>
    /// This is the View Model class of the Arithmetic window.
    /// </summary>

    public class ArithmeticVM : INotifyPropertyChanged
    {
        /// <summary>
        /// This section of the project is a math quiz game. The screen will show random addition exercises,
        /// and the user will need to click the right answer before the time runs out.
        /// </summary>

        // These are the two numbers of the quiz.
        private int num1;
        private int num2;

        /// <summary>
        /// Each property here is tied to the "PropertyChanged" event. 
        /// WPF Uses that event to know when the UI Elements needs to be updated.
        /// </summary>

        // The string of the quiz.
        private string questionString;
        public string QuestionString
        {
            get
            {
                return questionString;
            }
            set
            {
                questionString = value;
                OnPropertyChanged("QuestionString");
            }
        }

        // The first option from the set of 4 options.
        private string solution1;
        public string Solution1
        {
            get
            {
                return solution1;
            }
            set
            {
                solution1 = value;
                OnPropertyChanged("Solution1");
            }
        }

        // The second option from the set of 4 options.
        private string solution2;
        public string Solution2
        {
            get
            {
                return solution2;
            }
            set
            {
                solution2 = value;
                OnPropertyChanged("Solution2");
            }
        }

        // The third option from the set of 4 options.
        private string solution3;
        public string Solution3
        {
            get
            {
                return solution3;
            }
            set
            {
                solution3 = value;
                OnPropertyChanged("Solution3");
            }
        }

        // The forth option from the set of 4 options.
        private string solution4;
        public string Solution4
        {
            get
            {
                return solution4;
            }
            set
            {
                solution4 = value;
                OnPropertyChanged("Solution4");
            }
        }

        // The number of seconds before the time runs out.
        private int timeLeft;
        public int TimeLeft
        {
            get
            {
                return timeLeft;
            }
            set
            {
                timeLeft = value;
                OnPropertyChanged("TimeLeft");
            }
        }

        // When this boolean is true - a notify for the user to hurry up is shown.
        private bool hurryUpNotify;
        public bool HurryUpNotify
        {
            get
            {
                return hurryUpNotify;
            }
            set
            {
                hurryUpNotify = value;
                OnPropertyChanged("HurryUpNotify");
            }
        }

        // The state of the game. Can be Running, Won or Lost.
        private GameState gameState;
        public GameState GameState
        {
            get
            {
                return gameState;
            }
            set
            {
                gameState = value;
                OnPropertyChanged("GameState");
            }
        }

        // A command that made for the option buttons.
        public DelegateCommand<string> ChooseAnswer { get; set; }

        // This event is triggered when one the properties above is changed.
        public event PropertyChangedEventHandler PropertyChanged;

        public ArithmeticVM()
        {
            StartGame();
        }

        // Initializes and starts the quiz.
        // The game will re-start every time the user chooses an answer.
        private void StartGame()
        {
            GameState = GameState.Running;
            HurryUpNotify = false;
            TimeLeft = 31;

            ChooseAnswer = new DelegateCommand<string>((answer) => Execute(answer));

            SetRandomQuestion();

            GameTimer();
        }

        // This method is being called when the user clicks one of the option buttons.
        public void Execute(string answer)
        {
            if (answer == (num1+num2).ToString())
            {
                GameState = GameState.Won;
                MessageBox.Show("Correct!");
                StartGame();
            }
            else
            {
                GameState = GameState.Lost;
                MessageBox.Show($"Wrong! The correct answer is {num1 + num2}");
                StartGame();
            }
        }

        // If the game is not in "Running" state, the option buttons will be disabled.
        public bool CanExecute()
        {
            if (GameState == GameState.Running)
                return true;
            else
                return false;
        }

        // This method handles the countdown timer and the hurry-up notification.
        private void GameTimer()
        {
            Task.Run(() =>
            {
                while (GameState == GameState.Running)
                {  
                    // WPF Dispacher is needed here in order to change a UI element.
                    // That's because we are in a Task and not in the UI thread.
                    Application.Current.Dispatcher.Invoke(() => TimeLeft-- );
                    if (TimeLeft <= 15)
                    {
                        HurryUpNotify = true;
                    }
                    if (TimeLeft == 0)
                    {
                        GameState = GameState.Lost;
                        MessageBox.Show($"Time is up! The correct answer is {num1 + num2}");
                        StartGame();
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        // When the user closes the window this function is called, so the game will stop running.
        public void OnWindowClosed(object sender, EventArgs e)
        {
            GameState = GameState.Closed;
        }

        // Sets a random math addition qusetion to the screen.
        private void SetRandomQuestion()
        {
            Random rand = new Random();

            num1 = rand.Next(2, 11);
            num2 = rand.Next(2, 11);

            int rightSolution = num1 + num2;

            int solutionNumber = rand.Next(1, 5);

            switch (solutionNumber)
            {
                case 1:
                    Solution1 = rightSolution.ToString();

                    Solution2 = (rightSolution + 2).ToString();
                    Solution3 = (rightSolution - 2).ToString();
                    Solution4 = (rightSolution + 1).ToString();
                    break;
                case 2:
                    Solution2 = rightSolution.ToString();

                    Solution1 = (rightSolution + 2).ToString();
                    Solution3 = (rightSolution - 2).ToString();
                    Solution4 = (rightSolution + 1).ToString();
                    break;
                case 3:
                    Solution3 = rightSolution.ToString();

                    Solution1 = (rightSolution + 2).ToString();
                    Solution2 = (rightSolution - 2).ToString();
                    Solution4 = (rightSolution + 1).ToString();
                    break;
                case 4:
                    Solution4 = rightSolution.ToString();

                    Solution1 = (rightSolution + 2).ToString();
                    Solution2 = (rightSolution - 2).ToString();
                    Solution3 = (rightSolution + 1).ToString();
                    break;
            }

            QuestionString = $"{num1} + {num2} = ?";
        }

        // This handles the "PropertyChanged" event triggering
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    // The GameState enum:

    public enum GameState
    {
        Running,
        Won,
        Lost,
        Closed
    }
}
