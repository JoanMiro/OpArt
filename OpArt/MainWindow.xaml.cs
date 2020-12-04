namespace OpArt
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            if (this.CurrentApp?.MainWindow != null)
            {
                this.CurrentApp.MainWindow.SizeChanged += this.ContentResized;
            }
        }

        private Application CurrentApp => Application.Current;

        private void LayoutRootLoaded(object sender, RoutedEventArgs e)
        {
            this.GenerateArt();
        }

        private void GenerateArt()
        {
            this.Hirst();
            this.Riley();
        }

        private void ContentResized(object sender, SizeChangedEventArgs e)
        {
            var pieceHeight = this.CurrentApp?.MainWindow?.ActualHeight / 25;
            var pieceWidth = this.CurrentApp?.MainWindow?.ActualWidth / 25;

            foreach (UIElement childElement in this.LayoutRoot.Children)
            {
                if (childElement is Grid childGrid)
                {
                    foreach (var child in childGrid.Children)
                    {
                        if (child is Rectangle childRectangle)
                        {
                            childRectangle.Height = pieceHeight ?? 0;
                            childRectangle.Width = pieceWidth ?? 0;
                        }

                        if (child is Ellipse childEllipse && pieceHeight > 10)
                        {
                            childEllipse.Height = (double)(pieceHeight - 10);
                            childEllipse.Width = childEllipse.Height;
                        }
                    }
                }
            }
        }

        private Grid GetVisibleGrid()
        {
            if (this.LayoutRoot.Children.Count > 0)
            {
                foreach (UIElement child in this.LayoutRoot.Children)
                {
                    if (child is Grid grid && grid.Visibility == Visibility.Visible)
                    {
                        return grid;
                    }
                }
            }

            return null;
        }

        private void Riley()
        {
            var rileyGrid = new Grid();

            for (var index = 0; index < 25; index++)
            {
                rileyGrid.ColumnDefinitions.Add(new ColumnDefinition());
                rileyGrid.RowDefinitions.Add(new RowDefinition());
            }

            var random = new Random();

            var pieceHeight = this.CurrentApp?.MainWindow?.ActualHeight / 25;
            var pieceWidth = this.CurrentApp?.MainWindow?.ActualWidth / 25;

            for (var columnIndex = 0; columnIndex < 25; columnIndex++)
            {
                for (var rowIndex = 0; rowIndex < 25; rowIndex++)
                {
                    var rectangle = new Rectangle
                    {
                        Height = pieceHeight ?? 0,
                        Width = pieceWidth ?? 0,
                        Fill =
                        new SolidColorBrush
                        {
                            Color =
                            new Color
                            {
                                A = 255,
                                B = (byte)random.Next(255),
                                G = (byte)random.Next(255),
                                R = (byte)random.Next(255)
                            }
                        }
                    };

                    var transformGroup = new TransformGroup();
                    transformGroup.Children.Add(new SkewTransform { AngleY = -45 });
                    // transformGroup.Children.Add(new TranslateTransform { Y = -45 });
                    // transformGroup.Children.Add(new ScaleTransform { ScaleX = 1.5, ScaleY = 1.5 });
                    rectangle.RenderTransform = transformGroup;
                    rectangle.SetValue(Grid.RowProperty, rowIndex);
                    rectangle.SetValue(Grid.ColumnProperty, columnIndex);
                    rileyGrid.Children.Add(rectangle);
                }
            }

            this.RileyGrid.Children.Add(rileyGrid);
        }

        private void Hirst()
        {
            //if (this.LayoutRoot.Children.Count > 0)
            //{
            //    this.LayoutRoot.Children.RemoveAt(0);
            //}

            var hirstGrid = new Grid();

            for (var index = 0; index < 25; index++)
            {
                hirstGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (var index = 0; index < 40; index++)
            {
                hirstGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            var random = new Random();

            for (var columnIndex = 0; columnIndex < 40; columnIndex++)
            {
                for (var rowIndex = 0; rowIndex < 25; rowIndex++)
                {
                    var circle = new Ellipse
                    {
                        Height = 15,
                        Width = 15,
                        Fill =
                        new SolidColorBrush
                        {
                            Color =
                            new Color
                            {
                                A = 255,
                                B = (byte)random.Next(255),
                                G = (byte)random.Next(255),
                                R = (byte)random.Next(255)
                            }
                        }
                    };

                    // var transformGroup = new TransformGroup();
                    // transformGroup.Children.Add(new SkewTransform { AngleY = -45 });
                    // transformGroup.Children.Add(new TranslateTransform { Y = -45 });
                    // transformGroup.Children.Add(new ScaleTransform { ScaleX = 1.5, ScaleY = 1.5 });
                    // circle.RenderTransform = transformGroup;
                    circle.SetValue(Grid.RowProperty, rowIndex);
                    circle.SetValue(Grid.ColumnProperty, columnIndex);
                    hirstGrid.Children.Add(circle);
                }
            }

            this.HirstGrid.Children.Add(hirstGrid);
        }
    }
}