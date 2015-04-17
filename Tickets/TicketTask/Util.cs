using System;
using System.Collections.Generic;
using System.Text;

namespace TicketTask
{
    public static class Util
    {
        /// <summary>
        /// Дописывает символы в начале строки до определенной длины
        /// </summary>
        /// <param name="S"></param>
        /// <param name="l"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string getTrueStr(string S, int l, char ch)
        {
            while (S.Length < l) S = ch + S;
            return S;
        }

        /// <summary>
        /// Перевод числа в Nричную систему
        /// </summary>
        /// <param name="N">Исходное число</param>
        /// <param name="M">Система счисления</param>
        /// <returns></returns>
        public static string getNum(int N, int M)
        {
            var s = "";
            while (N >= M)
            {
                s += (N % M).ToString();
                N = Convert.ToInt32(N / M);
            }
            s += N.ToString();
            var S = "";
            for (int i = s.Length - 1; i >= 0; i--)
                S += s[i];
            return S;
        }

        /// <summary>
        /// Возвращает возможные варианты перестановок N чисел
        /// </summary>
        /// <param name="N">Размерность перестановочого массива</param>
        /// <returns></returns>
        public static List<string> getAllVariants(int N){
            List<string> list = new List<string>();
            int[] s = new int[N];
            int[] obr = new int[N];
            int pos = 0;
            int min = 0;
            int tmp = 0;
            for (int i = 0; i < N; i++)
            {
                s[i] = i+1;
            }
            bool flag;
            while (true)
            {
                string S = "";
                for (int i = 0; i < N; i++)
                {
                    S += s[i];
                }
                list.Add(S);
                flag = false;
                for (int i = N - 2; i >= 0; i--) {
                    if (s[i] < s[i + 1]) {
                        flag = true;
                        pos = i;
                        break;
                    }
                }
                if (!flag) break;
                int raz = s[pos + 1];
                for (int j = pos + 1; j < N; j++)
                {
                    if (((s[j] - s[pos]) < raz) && (s[pos] < s[j]))
                    {
                        min = j;
                    }
                }

                tmp = s[pos];
                s[pos] = s[min];
                s[min] = tmp;
                for (int j = pos + 1; j < N; j++)
                {
                    obr[j] = s[j];
                }
                int l = pos + 1;
                for (int k = N - 1; k >= pos + 1; k--)
                {
                    s[l] = obr[k];
                    l++;
                }
            }
            return list;
        }

        private static string UStr(int n) { return n.ToString(); }
        private static int UInt(string s){ return int.Parse(s); }
    }
}
