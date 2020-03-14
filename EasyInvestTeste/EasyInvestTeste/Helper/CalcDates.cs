using System;

namespace easyinvestteste.helper
{
    public class CalcDates
    {

        /// <summary>
        /// definindo os números de dias em um mês; index 0=> janeiro e 11=> dezembro
        /// fevereiro contém ou 28 ou 29 dias, por isso temos o valor -1
        /// o que iremos usar para calcular
        /// </summary>
        private int[] monthdays = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        
        /// <summary>
        /// contém a data inicial
        /// </summary>
        private DateTime startdate;
        
        /// <summary>
        /// contém a data final
        /// </summary>
        private DateTime enddate;

        public CalcDates(DateTime d1, DateTime d2)
        {
            int increment = 0;

            if (d1 > d2)
            {
                this.startdate = d2;
                this.enddate = d1;
            }
            else
            {
                this.startdate = d1;
                this.enddate = d2;
            }
            
            if (this.startdate.Day > this.enddate.Day)
            {
                increment = this.monthdays[this.startdate.Month - 1];
            }
            
            if (increment == -1)
            {
                increment = (DateTime.IsLeapYear(this.startdate.Year))? 29:28;
            }
            if (increment != 0)
            {
                days = (this.enddate.Day + increment) - this.startdate.Day;
                increment = 1;
            }
            else
            {
                days = this.enddate.Day - this.startdate.Day;
            }
            
            if ((this.startdate.Month + increment) > this.enddate.Month)
            {
                this.months = (this.enddate.Month + 12) - (this.startdate.Month + increment);
                increment = 1;
            }
            else
            {
                this.months = (this.enddate.Month) - (this.startdate.Month + increment);
                increment = 0;
            }

            TimeSpan t = d2 - d1;
            totalDays = (int)t.TotalDays;

            this.years = this.enddate.Year - (this.startdate.Year + increment);
        }

        public override string ToString()
        {
            return this.years + " anos(s), " + this.months + " mes(es), " + this.days + " dia(s)";
        }

        public int years { get; }

        public int months { get; }

        public int days { get; }

        public int totalDays { get; }
    }
}
