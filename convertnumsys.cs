using System;
using System.Linq;


namespace Convertnumsys
{
    public static class ConvertNS
    {
        public static Exception OutOfSystem = new Exception("Out of system exception");
        public static Exception EmptyString = new Exception("Empty string");
		public static Exception wrongSymbolException = new Exception("Wrong symbol exception!!!");


		public delegate void ShowMessage(string msg);
        public static event ShowMessage Notify;


        private static string _str;
        private static string _temps;

        public static int CharToInt(char a)
        {
            if ((a >= '0') && (a <= '9')) return a - '0';   
            if ((a >= 'A') && (a <= 'Z')) return a - 'A' + 10; 
            if ((a >= 'a') && (a <= 'z')) return a - 'a' + 10; 
            if (a == '.' || a == ',') return 0;
            return 33;
        }
        public static char IntToChar(int a)
        {
            if ((a >= 0) && (a < 10)) return (char)(a + '0');
            return (char)(a + 'A' - 10);
        }

        public static string Converting(int fromN, int toN, string numberS)
        {
			_str = numberS;
            string ans = "";
            string temps = "", exstr = "", r;
            int tempI = 0;
			double tempD = 0.0;

			if (numberS == "" || numberS == " ") throw EmptyString;

            if (fromN < 2 || fromN > 36 || toN < 2 || toN > 36) 
                throw OutOfSystem;
            else
            {
                for (int i = 0; i < numberS.Length && _str[i] != '.' && _str[i] != ','; i++)
                {
                        temps = temps + _str[i];
                        tempI = tempI * fromN + CharToInt(temps[i]);
                                   
				}
				if (tempI == 0) 
                    r = "0";
                else
                {
                    while (tempI != 0)
                    {
                        exstr = IntToChar(tempI % toN) + exstr;
                        tempI = tempI / toN;
                    }
                    r = exstr;
                }

                exstr = temps;
                temps = "";

                for (int i = exstr.Length + 1; i < numberS.Length; ++i)
				{
					temps = temps + _str[i];
					if (CharToInt(numberS[i]) >= fromN) throw wrongSymbolException;
				}
				if (temps.Length > 0)
				{
					exstr = "";
					for (int i = temps.Length - 1; i >= 0; --i) 
                        tempD = (CharToInt(temps[i]) + tempD) / fromN;

					while (Math.Truncate(tempD) > 0) 
                        tempD = tempD * 0.1;

					while (tempD > 0)
					{
						tempD = tempD * toN;
						exstr = exstr + IntToChar((int)Math.Truncate(tempD));
						tempD = tempD - Math.Truncate(tempD);
					}
					ans = r + "." + exstr;
				}
                else
					ans = r;
                    
            }
            return ans;
        }
    }
}