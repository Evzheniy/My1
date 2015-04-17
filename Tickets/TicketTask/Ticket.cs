using System;
using System.Collections.Generic;
using System.Text;

namespace TicketTask
{
    /// <summary>
    /// Класс Билетик
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Конструктор. Длина выражения
        /// </summary>
        /// <param name="length"></param>
        public Ticket(int length)
        {
            cur_length = length;
            _length = length;
        }

        private int _length;

        public double[] numbers = new double[7];
        private double[] Numbers = new double[7];
        public int cur_length;

        //Временные переменные
        double[] nums = new double[7];
        int nums_length;

        List<string>[] Per;

        /// <summary>
        /// Для записи элементов перебора в случае получения 100
        /// </summary>
        public List<string> Variants = new List<string>();

        /// <summary>
        /// Заполнение массива цифр билетика
        /// </summary>
        /// <param name="Text">Текст для заполнения</param>
        public void Fill(string Text)
        {
            for (int i = 0; i < cur_length; i++)
            {
                numbers[i] = Double.Parse(Text[i].ToString());
            }
        }

        /// <summary>
        /// Проверка на равенство выражения со значением 100
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Calculate(string action)
        {
            Variants.Clear();
            cur_length = _length;
            //Копия чисел
            for (int i = 0; i < cur_length; i++)
            {
                nums[i] = numbers[i];
            }
            nums_length = cur_length;
            //Слияние чисел 3 6 = 36
            while (action.IndexOf("0") >= 0)
            {
                if (!doCalculate(action.IndexOf("0"), 0)) return false;
                action = action.Remove(action.IndexOf("0"), 1);
            }
            int n = action.Length;
            //Получаем все варианты перебора для оставшихся чисел
            List<string> variants = GetVariant(n);
            //копия чисел
            for (int i = 0; i < nums_length; i++)
            {
                Numbers[i] = nums[i];
            }
            cur_length = nums_length;
            foreach(string variant in variants){
                string vrnt = variant;
                string ac = "";
                //Копия вариантов действий
                for (int i = 0; i < n; i++)
                {
                    ac += action[i];
                }
                nums_length = cur_length;
                for (int i = 0; i < nums_length; i++)
                {
                    nums[i] = Numbers[i];
                }
                //Проходим по одному из вариантов перебора и выполняем действие за действием по порядку
                for (int c = 1; c <= n; c++)
                {
                    int position = vrnt.IndexOf(c.ToString());
                    if (!doCalculate(position, int.Parse(ac[position].ToString()))) return false;
                    ac = ac.Remove(position, 1);
                    vrnt = vrnt.Remove(position, 1);

                }
                //Если после вычислений получилось 100 запоминаем текущий вариант перестановки
                if (nums[0] == 100) {
                    Variants.Add(variant);
                    //return true;
                }
            }
            //Если хоть один вариант получился равным 100 - True
            return Variants.Count > 0;
        }

        /// <summary>
        /// Выполнить то или иное вычисление
        /// </summary>
        /// <param name="pos">Позиция в строке действий</param>
        /// <param name="act">Дейстие</param>
        private bool doCalculate(int pos, int act)
        {
            double x = 0;
            switch (act)
            {
                case 0: //слитно
                    if (nums[pos] == 0){
                        //не учитываем, чтоб не считались числа типа 001+099
                        return false;
                    }
                    x = nums[pos] * 10 + nums[pos + 1];
                    break;
                case 1: // Умножение
                    x = nums[pos] * nums[pos + 1];
                    break;
                case 2: //Деление
                    x = nums[pos] / nums[pos + 1];
                    break;
                case 3: // Сложение
                    x = nums[pos] + nums[pos + 1];
                    break;
                case 4: // Вычитание
                    x = nums[pos] - nums[pos + 1];
                    break;
            }
            nums[pos] = x;
            //Сдвигаем элементы массива находящиеся правее
            for (int i = pos + 1; i < nums_length; i++) nums[i] = nums[i + 1];
            nums_length--;
            return true;
        }

        /// <summary>
        /// Заполняет все возможные массивы для перестановок
        /// </summary>
        public void FillVariants(int n)
        {
            Per = new List<string>[n+1];
            for (int i = 1; i <= n; i++)
            {
                if (Per[i] == null) Per[i] = new List<string>();
                Per[i] = Util.getAllVariants(i);
            }
        }

        private List<string> GetVariant(int n)
        {
            return Per[n];
        }

    }
}
