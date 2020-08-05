using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAssignmentOne.Operation
{
    public static class Validation
    {
        public static bool IsNumber(string value)
        {
            try
            {
                int.Parse(value.Trim());
                return true; 
            }catch(Exception ex)
            {
                return false;
            }
        }
        public static bool IsFloat(string value)
        {
            try
            {
                float.Parse(value.Trim());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool IsEmpty(string value)
        {
            return value.Trim().Length == 0;
        }

        public static bool IsOnlyDigitInString(string value)
        {
            if(value.Trim().Length == 0 )
            {
                return false;
            }
            for (int index = 0; index < value.Length; index++)
            {
                if (!char.IsDigit(value[index]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
