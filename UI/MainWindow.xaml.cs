using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

using SudokuGeneration;

namespace Sudoku
{
    public partial class MainWindow : Window
    {
        SudokuGeneration.Grid SudokuGridTemp;
        SudokuGeneration.Grid AnswerGrid;

        TextBox[,] textBoxes = new TextBox[9,9];

        public MainWindow()
        {
            InitializeComponent();

            SudokuGridTemp = new SudokuGeneration.Grid();
            AnswerGrid = new SudokuGeneration.Grid();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox tb = new TextBox();
                    tb.Name = "textBox" + Convert.ToString(i) + Convert.ToString(j);
                    tb.Text = Convert.ToString(i+1) + Convert.ToString(j+1);
                    tb.TextAlignment = TextAlignment.Center;
                    tb.MaxLength = 2;
                    tb.AcceptsReturn = false;
                    tb.AcceptsTab = false;
                    tb.FontSize = 17;
                    Grid.Children.Add(tb);
                    textBoxes[i, j] = tb;
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem btn = (MenuItem)sender;
            switch(btn.Name)
            {
                case "Create":
                    {
                        GridGen.GenGrid(matrix: ref SudokuGridTemp, answer: ref AnswerGrid);
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                textBoxes[i, j].IsReadOnly = false;
                                textBoxes[i, j].Background = Brushes.White;
                                if (SudokuGridTemp.GridTable[i, j] == 0)
                                {
                                    textBoxes[i, j].Text = "";
                                    textBoxes[i, j].Background = Brushes.Aqua;
                                    // textBoxes[i, j].TextChanged += GridUpdate;
                                }
                                else
                                {
                                    textBoxes[i, j].Text = Convert.ToString(SudokuGridTemp.GridTable[i, j]);
                                    textBoxes[i, j].IsReadOnly = true;
                                    textBoxes[i, j].Focus();
                                }
                            }
                        }
                        goto default;
                    }
                case "Check":
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                if (textBoxes[i, j].Text == "")
                                    SudokuGridTemp.GridTable[i, j] = 0;
                                else
                                    SudokuGridTemp.GridTable[i, j] = Convert.ToInt32(textBoxes[i, j].Text);
                            }
                        }

                        if (SudokuGridTemp == AnswerGrid)
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                for (int j = 0; j < 9; j++)
                                {
                                    if (SudokuGridTemp.GridTable[i, j] == 0 || SudokuGridTemp.GridTable[i, j] != AnswerGrid.GridTable[i, j])
                                        textBoxes[i, j].Background = Brushes.Aqua;
                                }
                            }
                            MessageBox.Show("Вы выйграли!", "Победа!", MessageBoxButton.OK);
                        }
                        else
                        {
                            // Timer timer = new Timer(new TimerCallback(Stop),null,1, 4000);
                            for (int i = 0; i < 9; i++)
                            {
                                for (int j = 0; j < 9; j++)
                                {
                                    if (SudokuGridTemp.GridTable[i, j] == 0 || SudokuGridTemp.GridTable[i, j] != AnswerGrid.GridTable[i, j])
                                        textBoxes[i, j].Background = Brushes.Red;
                                    else if (textBoxes[i, j].IsReadOnly == false)
                                        textBoxes[i, j].Background = Brushes.Aqua;
                                }
                            }
                        }

                        goto default;
                    }
                case "Settings":
                default: break;
            }
        }

        /*
        private void GridUpdate(object sender, TextChangedEventArgs e)
        {
            
            TextBox tb = (TextBox)sender;
            int i = Convert.ToInt32(Convert.ToString(tb.Name[7]));
            int j = Convert.ToInt32(Convert.ToString(tb.Name[7]));
            if (tb.Text == "")
                SudokuGridTemp.GridTable[i, j] = 0;
            else
                SudokuGridTemp.GridTable[i, j] = Convert.ToInt32(tb.Text);
        }
        */
        /*
        private void Stop(object obj)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (SudokuGrid.GridTable[i, j] == 0)
                    {

                        textBoxes[i, j].Background = Brushes.White;
                    }
                }
            }
        }
        */
    }
}
