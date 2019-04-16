using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorldWeather
{
    public class TemperatureRangeRules : ValidationRule
    {
        private int _min;
        private int _max;

        public TemperatureRangeRules()
        {
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int temp = 0;

            try
            {
                if (((string)value).Length > 0)
                    temp = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((temp < Min) || (temp > Max))
            { 
                return new ValidationResult(false,
                  "Please enter an age in the range: " + Min + " - " + Max + ".");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
