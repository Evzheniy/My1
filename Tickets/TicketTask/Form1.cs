using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TicketTask
{

    public partial class Form1 : Form
    {

        #region Class Ticket

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        public const int max_symbols = 6;

        /// <summary>
        /// Объект "Билетик"
        /// </summary>
        Ticket ticket = new Ticket(max_symbols);

        /// <summary>
        /// Массив действий(все сочетания знаков)
        /// </summary>
        public List<string> actions = new List<string>();


        /// <summary>
        /// Заполняем массив действий
        /// </summary>
        public void FillAction()
        {
            actions.Clear();
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
            //listBox1.Items.Clear();
        	tbMemo.Text = "";
            //Util.getAllVariants(5);
            ticket.FillVariants(5);
            if (ValidTicket())
            {
                ticket.Fill(textBox1.Text);
                FillAction();
                foreach (var action in actions)
                {
                    if (ticket.Calculate(action))
                    {
						foreach (string variant in ticket.Variants){
							var s = ShowAnswer(action, variant);
							//listBox1.Items.Add(s);
							tbMemo.Text += s + "\r\n";
						}
                    	//MessageBox.Show(ShowAnswer(action));
                    }
                }
                MessageBox.Show("Поиск завершен!");
            }
            else
            {
                MessageBox.Show("Некорректно введены данные!");
            }
        }

        /// <summary>
        /// Отображает понятное выражение
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private string ShowAnswer(string action, string variant)
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
                s += ticket.numbers[i] + ch;
            }
            s += ticket.numbers[5] + "=100 ("+variant+")";
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

		private void Form1_Load(object sender, EventArgs e)
		{

		}

    }
}
