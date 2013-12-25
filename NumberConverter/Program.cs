using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        private static string NumConvert(double source, int reserveNum)
        {
            bool isUseSuffixK = false;
            bool isUseSuffixM = false;
            bool isUseSuffixB = false;

            double result = source;

            //double a =  Math.Pow(10, 3);
            int len = result.ToString().Length;
            if ((len - reserveNum) > 9)
            {
                return "error. source too large .";
            }

            //确定是否需要使用K,M,B后缀
            if (result >= 1000000000)
            {
                //a = 9;
                result = result / 1000000000;
                isUseSuffixB = true;
            }
            else if (result >= 1000000)
            {
                //a = 6;
                result = result / 1000000;
                if (Math.Truncate(result) >= Math.Pow(10, reserveNum))
                {
                    result = result / 1000;
                    isUseSuffixB = true;
                }
                else
                {
                    isUseSuffixM = true;
                }
            }
            else if (result >= 1000)
            {
                //a = 3;
                result = result / 1000;
                if (Math.Truncate(result) >= Math.Pow(10, reserveNum))
                {
                    result = result / 1000;
                    isUseSuffixM = true;
                }
                else
                {
                    isUseSuffixK = true;
                }
            }

            int integralLen = Math.Truncate(result).ToString().Length;
            int count = reserveNum - integralLen - 1;
            string numlayoutFormat = "0.0";
            for (int i = 0; i < count; i++)
            {
                numlayoutFormat += "0";
            }

            return result.ToString(numlayoutFormat);
        }
    }
}
