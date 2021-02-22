using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MasterMindWpf
{
    public class GameLogic
    {
        protected const int ellipseSize = 30;
        protected const int ellipseGap = 8;

        protected Random random;
        public Brush SelectedColour { get; set; }

        public int Attempt { get; set; }
        public bool IsEnableStart { get; set; }

        public List<Brush> SearchedColours { get; set; }

        protected int numberOfSearchedColours;

        protected List<Brush> previousPlayersTips;
        protected List<Brush> correctColoursOrPositions;
   
        public GameLogic()
        {
            random = new Random();
            SelectedColour = Brushes.White;
            IsEnableStart = false;
        }

        /// <summary>
        /// Vygeneruje elipsy s hledanými barvami
        /// </summary>
        /// <param name="_numberOfSearchedColours"></param>
        public void NewGame(int _numberOfSearchedColours)
        {
            Attempt = 0;
            numberOfSearchedColours = _numberOfSearchedColours;
            SearchedColours = new List<Brush>();
            for (int i = 0; i < numberOfSearchedColours; i++)
            {
                Colours colour = (Colours)random.Next(0, 5);
                Brush brush = (Brush)new BrushConverter().ConvertFromString(colour.ToString());
                SearchedColours.Add(brush);
            }
            previousPlayersTips = new List<Brush>();
            correctColoursOrPositions = new List<Brush>();
        }

        /// <summary>
        /// Vygeneruje elipsy
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="brush"></param>
        /// <param name="numberOfRows"></param>
        public void GenerateEllipses(Canvas canvas, Brush brush, int numberOfRows)
        {
            canvas.Children.Clear();
            for (int y = 0; y < numberOfRows ; y++)
            {
                for (int i = 0; i < SearchedColours.Count; i++)
                {
                    Ellipse ellipse = new Ellipse()
                    {
                        Width = ellipseSize,
                        Height = ellipseSize
                    };
                    ellipse.Fill = brush;
                    ellipse.Stroke = Brushes.Black;

                    canvas.Children.Add(ellipse);
                    Canvas.SetLeft(ellipse, i * (ellipseGap + ellipseSize));
                    Canvas.SetTop(ellipse, y  * (ellipseGap + ellipseSize) + ellipseGap);
                }
            }
        }

        /// <summary>
        /// Přidá elipsám vlastnost "kliknutí"
        /// </summary>
        /// <param name="canvas"></param>
        public void GenerateTipEllipses(Canvas canvas)
        {
            canvas.Children.Clear();
            GenerateEllipses(canvas, Brushes.SandyBrown,1);

            foreach(var ellipse in canvas.Children)
            {
                ((Ellipse)ellipse).MouseDown += GameLogic_MouseDown;
            }
        }

        private void GameLogic_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((Ellipse)sender).Fill = SelectedColour;
        }

        /// <summary>
        /// Vygeneruje a obarví elipsy minulých pokusů
        /// </summary>
        /// <param name="canvas"></param>
        public void GeneratePreviousPlayerTipEllipses(Canvas canvas)
        {
            canvas.Children.Clear();
            GenerateEllipses(canvas, Brushes.SandyBrown, Attempt);
               
            int y = 0;
            foreach(var ellipse in canvas.Children)
            {               
                ((Ellipse)ellipse).Fill = previousPlayersTips[y++];
            }
        }

        /// <summary>
        /// Vygeneruje a obarví elipsy, které ukazují správnou pozici nebo barvu
        /// </summary>
        /// <param name="canvas"></param>
        public void GeneratePreviousTipEllipses(Canvas canvas)
        {
            canvas.Children.Clear();

            for (int y = 0; y < Attempt * 2; y++)
            {
                for (int i = 0; i < SearchedColours.Count / 2; i++)
                {
                    Ellipse ellipse = new Ellipse()
                    {
                        Width = ellipseSize/2,
                        Height = ellipseSize/2
                    };
                    ellipse.Fill = Brushes.SaddleBrown;
                    ellipse.Stroke = Brushes.Black;

                    canvas.Children.Add(ellipse);
                    Canvas.SetLeft(ellipse, i * (ellipseGap/2 + ellipseSize / 2));
                    Canvas.SetTop(ellipse, y * (ellipseGap/2 + ellipseSize / 2) + ellipseGap);
                }
            }

            int ii = 0;
            foreach (var ellipse in canvas.Children)
            {
                ((Ellipse)ellipse).Fill = correctColoursOrPositions[ii++];
            }
        }

        /// <summary>
        /// Porovnávání hráčova typu s hledanými barvami
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public int ComparingColours(Canvas canvas)
        {
            int brownPlaces = 0;
            List <Brush> currentTipColours = new List<Brush>();
            List <Brush> temporarySearchedColours = new List<Brush>();
            Attempt++;

            foreach (var ellipse in canvas.Children)
            {
                currentTipColours.Add(((Ellipse)ellipse).Fill);
                previousPlayersTips.Add(((Ellipse)ellipse).Fill);
            }
            
            if(currentTipColours.SequenceEqual(SearchedColours))
            {
                return 1;
            }
            if(Attempt == 10)
            {
                return -1;
            }

            foreach(var brush in SearchedColours)
            {
                temporarySearchedColours.Add(brush);
            }

            List<Brush> foundCorrectColours = new List<Brush>();

            for (int i = 0; i < numberOfSearchedColours; i++)
            {
                if (currentTipColours[i].Equals(temporarySearchedColours[i]))
                {
                    correctColoursOrPositions.Add(Brushes.Red);
                    temporarySearchedColours[i] = Brushes.Gray;
                }
            }
            
            for (int i = 0; i < numberOfSearchedColours; i++)
            {
                if (temporarySearchedColours[i] == Brushes.Gray)
                    continue;
                if (temporarySearchedColours.Contains(currentTipColours[i]))
                {
                    if (!foundCorrectColours.Contains(currentTipColours[i]))
                    {
                        correctColoursOrPositions.Add(Brushes.White);
                        foundCorrectColours.Add(currentTipColours[i]);
                    }
                    else
                        brownPlaces++;
                }
                else
                    brownPlaces++;
            }

            for (int i = 0; i < brownPlaces; i++)
            {
                correctColoursOrPositions.Add(Brushes.SaddleBrown);
            }

            return 0;
        }

       
    }
}
