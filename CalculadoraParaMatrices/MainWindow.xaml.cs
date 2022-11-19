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

namespace CalculadoraParaMatrices
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>h
    public partial class MainWindow : Window
    {
        private readonly GridLength gridLength = 
            new GridLength(1, GridUnitType.Star);
        private readonly Dictionary<int,List<TextBox>> DictionaryRowsA = 
            new Dictionary<int,List<TextBox>>();
        private readonly Dictionary<int, List<TextBox>> DictionaryColumnsA =
            new Dictionary<int, List<TextBox>>();

        private readonly Dictionary<int, List<TextBox>> DictionaryRowsB =
            new Dictionary<int, List<TextBox>>();
        private readonly Dictionary<int, List<TextBox>> DictionaryColumnsB =
            new Dictionary<int, List<TextBox>>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeRowsAndColumnsA();
            InitializeRowsAndColumnsB();
        }

        private void InitializeRowsAndColumnsA()
        {

            List<TextBox> placedTextBoxesInMatrixA = new List<TextBox>();
            foreach (var child in this.Grid_MatrizA.Children)
            {
                placedTextBoxesInMatrixA.Add(child as TextBox);
            }

            this.DictionaryRowsA.Add(0, new List<TextBox>() { placedTextBoxesInMatrixA.ElementAt(0), placedTextBoxesInMatrixA.ElementAt(1) });
            this.DictionaryRowsA.Add(1, new List<TextBox>() { placedTextBoxesInMatrixA.ElementAt(2), placedTextBoxesInMatrixA.ElementAt(3) });
            this.DictionaryColumnsA.Add(0, new List<TextBox>() { placedTextBoxesInMatrixA.ElementAt(0), placedTextBoxesInMatrixA.ElementAt(2) });
            this.DictionaryColumnsA.Add(1, new List<TextBox>() { placedTextBoxesInMatrixA.ElementAt(1), placedTextBoxesInMatrixA.ElementAt(3) });
        }

        private void InitializeRowsAndColumnsB()
        {

            List<TextBox> placedTextBoxesInMatrixB = new List<TextBox>();
            foreach (var child in this.Grid_MatrizA.Children)
            {
                placedTextBoxesInMatrixB.Add(child as TextBox);
            }

            this.DictionaryRowsB.Add(0, new List<TextBox>() { placedTextBoxesInMatrixB.ElementAt(0), placedTextBoxesInMatrixB.ElementAt(1) });
            this.DictionaryRowsB.Add(1, new List<TextBox>() { placedTextBoxesInMatrixB.ElementAt(2), placedTextBoxesInMatrixB.ElementAt(3) });
            this.DictionaryColumnsB.Add(0, new List<TextBox>() { placedTextBoxesInMatrixB.ElementAt(0), placedTextBoxesInMatrixB.ElementAt(2) });
            this.DictionaryColumnsB.Add(1, new List<TextBox>() { placedTextBoxesInMatrixB.ElementAt(1), placedTextBoxesInMatrixB.ElementAt(3) });
        }

        private void AddRowToMatrixA(object sender, RoutedEventArgs e)
        {
            RowDefinition newRow = new RowDefinition();
            newRow.Height = this.gridLength;
            this.Grid_MatrizA.RowDefinitions.Add(newRow);
            int columnCount= this.Grid_MatrizA.ColumnDefinitions.Count;
            int newRowIndex = this.Grid_MatrizA.RowDefinitions.Count-1;

            List<TextBox> newTextBoxRow = new List<TextBox>();
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                TextBox textBox = GetNewTextBox(columnIndex, newRowIndex);
                this.Grid_MatrizA.Children.Add(textBox);
                newTextBoxRow.Add(textBox);
            }

            this.DictionaryRowsA.Add(newRowIndex, newTextBoxRow);
            DEBUG_PrintMatrixSizes();
        }

        private void RemoveRowFromMatrixA(object sender, RoutedEventArgs e)
        {
            int lastRowAddedIndex = this.Grid_MatrizA.RowDefinitions.Count - 1;

            if (this.DictionaryRowsA.ContainsKey(lastRowAddedIndex))
            {
                List<TextBox> textBoxesToRemove = this.DictionaryRowsA[lastRowAddedIndex];

                foreach (TextBox textBox in textBoxesToRemove)
                {
                    this.Grid_MatrizA.Children.Remove(textBox);
                }
                this.DictionaryRowsA.Remove(lastRowAddedIndex);
                this.Grid_MatrizA.RowDefinitions.RemoveAt(lastRowAddedIndex);
            }
            DEBUG_PrintMatrixSizes();
        }

        private void RemoveColumnFromMatrixA(object sender, RoutedEventArgs e)
        {
            int lastColumnAddedIndex = 
                this.Grid_MatrizA.ColumnDefinitions.Count - 1;
            if (this.DictionaryColumnsA.ContainsKey(lastColumnAddedIndex))
            {
                List<TextBox> textBoxesToRemove =
                this.DictionaryColumnsA[lastColumnAddedIndex];

                foreach (TextBox textBox in textBoxesToRemove)
                {
                    this.Grid_MatrizA.Children.Remove(textBox);
                }

                this.DictionaryColumnsA.Remove(lastColumnAddedIndex);
                this.Grid_MatrizA.ColumnDefinitions.RemoveAt(lastColumnAddedIndex);
            }
            DEBUG_PrintMatrixSizes();

        }

        private void DEBUG_PrintMatrixSizes()
        {
            Console.WriteLine("A = " + this.DictionaryRowsA.Count + " x " + this.DictionaryColumnsA.Count);
            Console.WriteLine("B = " + this.DictionaryRowsB.Count + " x " + this.DictionaryColumnsB.Count);
        }

        private void AddColumnToMatrixA(object sender, RoutedEventArgs e)
        {
            ColumnDefinition newColumn = new ColumnDefinition();
            newColumn.Width = this.gridLength;
            this.Grid_MatrizA.ColumnDefinitions.Add(newColumn);
            int rowCount = this.Grid_MatrizA.RowDefinitions.Count;
            int newColumnIndex = this.Grid_MatrizA.ColumnDefinitions.Count - 1;

            List<TextBox> newTextBoxColumn = new List<TextBox>();
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                TextBox textBox = GetNewTextBox(newColumnIndex,rowIndex);
                this.Grid_MatrizA.Children.Add(textBox);
                newTextBoxColumn.Add(textBox);
            }
            
            this.DictionaryColumnsA.Add(newColumnIndex, newTextBoxColumn);
            DEBUG_PrintMatrixSizes();
        }

        private void AddRowToMatrixB(object sender, RoutedEventArgs e)
        {
            RowDefinition newRow = new RowDefinition();
            newRow.Height = this.gridLength;
            this.Grid_MatrizB.RowDefinitions.Add(newRow);
            int columnCount = this.Grid_MatrizB .ColumnDefinitions.Count;
            int newRowIndex = this.Grid_MatrizB.RowDefinitions.Count - 1;

            List<TextBox> newTextBoxRow = new List<TextBox>();
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                TextBox textBox = GetNewTextBox(columnIndex, newRowIndex);
                this.Grid_MatrizB.Children.Add(textBox);
                newTextBoxRow.Add(textBox);
            }

            this.DictionaryRowsB.Add(newRowIndex, newTextBoxRow);
            DEBUG_PrintMatrixSizes();
        }

        private void RemoveRowFromMatrixB(object sender, RoutedEventArgs e)
        {
            int lastRowAddedIndex = this.Grid_MatrizB.RowDefinitions.Count - 1;

            if (this.DictionaryRowsB.ContainsKey(lastRowAddedIndex))
            {
                List<TextBox> textBoxesToRemove = this.DictionaryRowsB[lastRowAddedIndex];

                foreach (TextBox textBox in textBoxesToRemove)
                {
                    this.Grid_MatrizB.Children.Remove(textBox);
                }
                this.DictionaryRowsB.Remove(lastRowAddedIndex);
                this.Grid_MatrizB.RowDefinitions.RemoveAt(lastRowAddedIndex);
            }
            DEBUG_PrintMatrixSizes();
        }

        private void RemoveColumnFromMatrixB(object sender, RoutedEventArgs e)
        {
            int lastColumnAddedIndex =
                this.Grid_MatrizB.ColumnDefinitions.Count - 1;
            if (this.DictionaryColumnsB.ContainsKey(lastColumnAddedIndex))
            {
                List<TextBox> textBoxesToRemove =
                this.DictionaryColumnsB[lastColumnAddedIndex];

                foreach (TextBox textBox in textBoxesToRemove)
                {
                    this.Grid_MatrizB.Children.Remove(textBox);
                }

                this.DictionaryColumnsB.Remove(lastColumnAddedIndex);
                this.Grid_MatrizB.ColumnDefinitions.RemoveAt(lastColumnAddedIndex);
            }
            DEBUG_PrintMatrixSizes();
        }

        private void AddColumnToMatrixB(object sender, RoutedEventArgs e)
        {
            ColumnDefinition newColumn = new ColumnDefinition();
            newColumn.Width = this.gridLength;
            this.Grid_MatrizB.ColumnDefinitions.Add(newColumn);
            int rowCount = this.Grid_MatrizB.RowDefinitions.Count;
            int newColumnIndex = this.Grid_MatrizB.ColumnDefinitions.Count - 1;

            List<TextBox> newTextBoxColumn = new List<TextBox>();
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                TextBox textBox = GetNewTextBox(newColumnIndex, rowIndex);
                this.Grid_MatrizB.Children.Add(textBox);
                newTextBoxColumn.Add(textBox);
            }

            this.DictionaryColumnsB.Add(newColumnIndex, newTextBoxColumn);
            DEBUG_PrintMatrixSizes();
        }

        private TextBox GetNewTextBox(int columnIndex, int rowIndex)
        {
            TextBox textBox = new TextBox();
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            textBox.SetValue(Grid.ColumnProperty, columnIndex);
            textBox.SetValue(Grid.RowProperty, rowIndex);
            return textBox;
        }
    }
}
