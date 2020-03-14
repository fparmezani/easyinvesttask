using System;

namespace EasyInvestTeste.Model
{
    public class fundos
    {
        #region Properties

        public double capitalInvestido { get; set; }

        public double valorAtual { get; set; }

        public DateTime dataResgate { get; set; }

        public DateTime dataCompra { get; set; }

        public double iof { get; set; }

        public string nome { get; set; }

        public double totalTaxas { get; set; }

        public int quantity { get; set; }

        #endregion
    }
}
