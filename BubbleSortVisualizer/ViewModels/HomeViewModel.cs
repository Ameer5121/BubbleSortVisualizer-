using BubbleSortVisualizer.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BubbleSortVisualizer.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        private int _columnAmount = 2;
        private int _rowAmount = 1;
        private double _speed = 0.1;
        private bool _controlsEnabled = true;
        public ObservableCollection<int> RandomizedNumbers { get; set; }
        public int ColumnAmount
        {
            get => _columnAmount;
            set => SetPropertyValue(ref _columnAmount, value);
        }
        public int RowAmount
        {
            get => _rowAmount;
            set => SetPropertyValue(ref _rowAmount, value);
        }
        public double Speed
        {
            get => _speed;
            set => SetPropertyValue(ref _speed, value);
        }

        public bool ControlsEnabled
        {
            get => _controlsEnabled;
            set => SetPropertyValue(ref _controlsEnabled, value);
        }

        public ICommand StartSort => new RelayCommand(AssignNumbersAsync);
        public HomeViewModel()
        {
            RandomizedNumbers = new ObservableCollection<int>();
        }
        private async Task AssignNumbersAsync()
        {
            DisableControls();
            if (RandomizedNumbers.Count() > 1) ClearCollection();
            var amountToAssign = ColumnAmount * RowAmount;
            Random random = new Random();
            while (RandomizedNumbers.Count() < amountToAssign)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(5));
                var numberToAdd = random.Next(0, 1000);
                if (!RandomizedNumbers.Contains(numberToAdd))
                {
                    RandomizedNumbers.Add(numberToAdd);
                }
            }
            await BubbleSortAsync(RandomizedNumbers);
            EnableControls();
        }

        private async Task BubbleSortAsync(ObservableCollection<int> collection)
        {

            for (int i = 0; i < collection.Count(); i++)
            {
                for (int j = i + 1; j < collection.Count(); j++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(Speed));
                    if (collection[i] > collection[j])
                    {
                        var temp = collection[i];
                        collection[i] = collection[j];
                        collection[j] = temp;
                    }
                }
            }
        }

        private void DisableControls()
        {
            ControlsEnabled = false;
        }
        private void EnableControls()
        {
            ControlsEnabled = true;
        }

        private void ClearCollection()
        {
            RandomizedNumbers.Clear();
        }

    }
}
