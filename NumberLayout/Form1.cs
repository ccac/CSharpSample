using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NumberLayout
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string NumConvert(double source, int reserveNum)
        {
            //后缀
            string[] NumSuffix = { "", " K", " M", " B" };
            int currentSuffixIndex = 0;

            //整数部分长度
            int integralLen = Math.Truncate(Math.Abs(source)).ToString().Length;

            if ((integralLen - reserveNum) > 9)
            {
                return "error. source too large or reserverNum to small";
            }

            //确定是否需要使用K,M,B后缀
            if (Math.Abs(source) >= 1000000000)
            {
                //a = 9;
                source = source / 1000000000;
                integralLen -= 9;
                currentSuffixIndex = 3;
            }
            else if (Math.Abs(source) >= 1000000)
            {
                //a = 6;
                source = source / 1000000;
                if (Math.Truncate(source) >= Math.Pow(10, reserveNum))
                {
                    source = source / 1000;
                    integralLen = 1;
                    currentSuffixIndex = 3;
                }
                else
                {
                    integralLen -= 6;
                    currentSuffixIndex = 2;
                }
            }
            else if (Math.Abs(source) >= 1000)
            {
                //a = 3;
                source = source / 1000;
                if (Math.Truncate(source) >= Math.Pow(10, reserveNum))
                {
                    source = source / 1000;
                    integralLen = 1;
                    currentSuffixIndex = 2;
                }
                else
                {
                    integralLen -= 3;
                    currentSuffixIndex = 1;
                }
            }


            string numlayoutFormat = "0"; //输入时的转换格式

            if (reserveNum > integralLen)
            {
                //小数部分长度
                int decimalLen = reserveNum - integralLen - 1;
                numlayoutFormat = "0.0";
                for (int i = 0; i < decimalLen; i++)
                {
                    numlayoutFormat += "0";
                }
            }

            return source.ToString(numlayoutFormat) + NumSuffix[currentSuffixIndex];
        }

        private static string NumConvertByString(double source, int reserveNum)
        {
            //后缀
            string[] NumSuffix = { "", " K", " M", " B" };
            int currentSuffixIndex = 0;

            string strSource = Math.Abs(source).ToString();

            //整数部分长度
            int integralLen = strSource.IndexOf(".");
            //完全是个整数
            if (integralLen < 0)
            {
                integralLen = strSource.Length;
                strSource += "0.0";
            }

            if (reserveNum < 3)
            {
                throw new OverflowException();
                return "";
            }

            if ((integralLen - reserveNum) > 9)
            {
                throw new OverflowException();
                return "";
            }

            //确定是否需要使用K,M,B后缀

            int carryTime = (integralLen - 1) / 3;  //进位次数
            carryTime = carryTime > 3 ? 3 : carryTime;
            integralLen -= carryTime * 3;
            strSource = MoveRadixPointInDecimal(strSource, integralLen);
            currentSuffixIndex = carryTime;

            //if (integralLen > 9)
            //{
            //    integralLen -= 9;
            //    strSource = MoveRadixPointInDecimal(strSource, integralLen);
            //    currentSuffixIndex = 3;
            //}
            //else if (integralLen > 6)
            //{
            //    integralLen -= 6;
            //    strSource = MoveRadixPointInDecimal(strSource, integralLen);
            //    currentSuffixIndex = 2;
            //}
            //else if (integralLen > 3)
            //{
            //    integralLen -= 3;
            //    strSource = MoveRadixPointInDecimal(strSource, integralLen);
            //    currentSuffixIndex = 1;
            //}

            string strTarget = strSource;

            strTarget = strTarget.PadRight(reserveNum + 2, '0').Substring(0, reserveNum + 2); //+2.其中一位是小数位。另外多取一位是为了四舍五入

            
            //四舍五入。
            double param = Math.Pow(10, reserveNum - integralLen);
            int intTarget = (int)(double.Parse(strTarget) * param + 0.5);

            //如果四舍五入后进位导致位数多出一位
            //只有整数位都是9才会出现这种四舍五入后整数位进1的情况
            if (intTarget.ToString().Length > reserveNum)
            {
                //还原数字。递归调用
                return NumConvertByString(intTarget * Math.Pow(10, carryTime * 3 - (reserveNum - integralLen)),reserveNum);
            }

            //if (intTarget >= Math.Pow(10, reserveNum))
            //{
            //    integralLen++;

            //    if ((integralLen > 3) && (currentSuffixIndex < NumSuffix.Length - 1))
            //    {
            //        integralLen -= 3;
            //        currentSuffixIndex++;
            //    }

            //    if (integralLen > reserveNum)
            //    {
            //        return "error. source too large or reserverNum to small";
            //    }
            //}


            return (Math.Sign(source) == -1 ? "-" : "")
                + MoveRadixPointInDecimal(intTarget.ToString(), integralLen)
                + NumSuffix[currentSuffixIndex];
        }


        private static string MoveRadixPointInDecimal(string strSouce,int newPos)
        {
            string[] numSplit = strSouce.Split('.');
            string strTemp = "";
            for (int i = 0; i < numSplit.Length; i++)
            {
                strTemp += numSplit[i];
            }
            if (newPos <= 0)
            {
                return "0." + strTemp;
            }
            else if (newPos == strTemp.Length)
            {
                return strTemp;
            }
            else
            {
                return strTemp.Substring(0, newPos) + "." + strTemp.Substring(newPos);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            double source = double.Parse(txtSource.Text.Trim());
            int reserve = int.Parse(txtReserve.Text.Trim());

            txtResult.Text = NumConvertByString(source, reserve);
        }
    }
}
