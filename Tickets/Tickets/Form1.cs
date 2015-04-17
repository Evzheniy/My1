using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tickets
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
        public Ticket(int length) {
            cur_length = length;
        }

        public double[] numbers = new double[7];
        public int cur_length;

        //Временные переменные
        double[] num = new double[7];
        int num_length;

        /// <summary>
        /// Заполнение массива цифр билетика
        /// </summary>
        /// <param name="Text">Текст для заполнения</param>
        public void Fill(string Text)
        {
            for (int i = 0; i < cur_length; i++){
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
            for (int i = 0; i < cur_length; i++)
            {
                num[i] = numbers[i];
            }
            num_length = cur_length;
            for (int i = 0; i <= 4; i++)
            {
                var ch = i.ToString();
                while (action.IndexOf(ch) >= 0)
                {
                    doCalculate(action.IndexOf(ch), Int32.Parse(ch));
                    action = action.Remove(action.IndexOf(ch), 1);
                }
            }
            return num[0]==100;
        }

        /// <summary>
        /// Выполнить то или иное вычисление
        /// </summary>
        /// <param name="pos">Позиция в строке действий</param>
        /// <param name="act">Дейстие</param>
        private void doCalculate(int pos, int act)
        {
            double x=0;
            switch (act){
                case 0: //слитно
                    x = num[pos] * 10 + num[pos + 1];
                    break;
                case 1: // Умножение
                    x = num[pos] * num[pos + 1];
                    break;
                case 2: //Деление
                    x = num[pos] / num[pos + 1];
                    break;
                case 3: // Сложение
                    x = num[pos] + num[pos + 1];
                    break;
                case 4: // Вычитание
                    x = num[pos] - num[pos + 1];
                    break;
            }
            num[pos] = x;
            for (int i = pos+1; i < num_length; i++) num[i] = num[i + 1];
            num_length--;
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public const int max_symbols=6;
        Ticket ticket = new Ticket(max_symbols);
        public List<string> actions = new List<string>();

        /// <summary>
        /// Заполняем массив действий
        /// </summary>
        public void FillAction()
        {
            var s = "00000";
            string maxS = "44444";
            var i = 0;
            while (!s.Equals(maxS))
            {
                i++;
                s = Util.getTrueStr(Util.getNum(i, 5), 5, '0');
                actions.Add(s);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidTicket()) {
                ticket.Fill(textBox1.Text);
                FillAction();
                foreach (var action in actions){
                    if (ticket.Calculate(action)) {
                        MessageBox.Show(ShowAnswer(action));
                    }
                }
                MessageBox.Show("Поиск завершен!");
            }
            else {
                MessageBox.Show("Некорректно введены данные!");
            }
        }

        /// <summary>
        /// Отображает понятное выражение
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private string ShowAnswer(string action)
        {
            var s = "";
            var ch = "";
            for (int i = 0; i < 5; i++)
            {
                switch (action[i])
                {
                    case '0': ch = "";
                        break;
                    case '1': ch = "*";
                        break;
                    case '2': ch = "/";
                        break;
                    case '3': ch = "+";
                        break;
                    case '4': ch = "-";
                        break;
                }
                s += ticket.numbers[i]+ch;
            }
            s += ticket.numbers[5] + "=100";
            return s;
        }



        /// <summary>
        /// Проверка равильности ввода
        /// </summary>
        /// <returns>Правилен ли ввод</returns>
        private bool ValidTicket()
        {
            var s = textBox1.Text;
            if (s.Length != max_symbols) return false;
            //Проверка все ли символы - цифры
            foreach (char c in s)
                if (!Char.IsDigit(c)) return false;
            return true;
        }
    }
}
