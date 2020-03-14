using System;

namespace EasyInvestTeste.Model
{
    public class Investiment
    {
        public string nome { get; set; }

        public double valorInvestido { get; set; }

        public double valorTotal { get; set; }

        public DateTime vencimento { get; set; }

        public double Ir { get; set; }

        public double valorResgate { get; set; }
    }
}
