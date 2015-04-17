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
    public class Ticket
    {
        public Ticket(int length) {
            cur_length = length;
        }
        public double[] numbers = new double[7];
        public int cur_length;

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
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public const int max_symbols=6;



        Ticket ticket = new Ticket(max_symbols);

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidTicket()) {
                ticket.Fill(textBox1.Text);

            }
            else {
            }
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
            return true;
        }
    }
}
