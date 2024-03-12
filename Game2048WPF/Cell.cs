using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game2048WPF
{
    /// <summary>Класс ячейки</summary>
    public class Cell : INotifyPropertyChanged
    {
        private CellValueEnum _value = CellValueEnum.None;
        private bool _isCalculated = false;
        private bool isNewValue;

        /// <summary>Значение ячейки</summary>
        public CellValueEnum Value { get => _value; set { _value = value; OnPropertyChanged(); } }
        /// <summary>Флаг - значение ячейки получено вычислением.
        /// Служит для предотвращения двойной обработки ячейки в одном шаге.
        /// <see langword="true"/> если значение получено слиянием в текущем шаге</summary>
        public bool IsCalculated { get => _isCalculated; set { _isCalculated = value; OnPropertyChanged(); } }
        /// <summary>Номер строки</summary>
        public int Row { get; }
        /// <summary>Номер колонки</summary>
        public int Column { get; }
        /// <summary>Свойство выделяющее ячейку с последним добавленным значением</summary>
        public bool IsNewValue { get => isNewValue; set { isNewValue = value; OnPropertyChanged(); } }

        /// <summary>Конструктор с заданием строки и колонки</summary>
        /// <param name="row">Номер строки</param>
        /// <param name="column">Номер колонки</param>
        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>Метод перемещения значения ячейки в указанную ячейку</summary>
        /// <param name="target">Ячейка куда надо переместить значение (цель)</param>
        /// <returns>Ячейку в которую было перемещено значение или <see langword="null"/> если значение переместить нельзя</returns>
        public Cell MoveTo(Cell target)
        {
            if (
                    Value == CellValueEnum.None // Если значение ячейки None
                    || target == null // если цель == null
                    || target.IsCalculated || IsCalculated // если значение цели или ячейки уже вычислялись на этом шаге
                    || (target.Value != CellValueEnum.None && target.Value != Value) // если значение цели не None и не равно значению ячейки
                )
                return null;

            if (target.Value == CellValueEnum.None) // если значение цели None
            {
                target.Value = Value;
                Value = CellValueEnum.None;
                target.IsCalculated = IsCalculated = false;
                return target;
            }
            if (target.Value == Value) // если значения ячеек одинаковые
            {
                int newValue = (int)target.Value + (int)Value;
                if (!Enum.IsDefined(typeof(CellValueEnum), newValue))
                    throw new Exception("Метод \"Cell MoveTo(Cell target)\".\r\nПолучено непредвиденное значение!");
                target.Value = (CellValueEnum)newValue;
                target.IsCalculated = true;
                Value = CellValueEnum.None;
                IsCalculated = false;
                return target;
            }
            throw new Exception("Метод \"Cell MoveTo(Cell target)\".\r\nНепредвиденная ошибка!");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
