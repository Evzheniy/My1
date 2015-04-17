using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tickets
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
    }
}
