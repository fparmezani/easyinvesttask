using System;

namespace EasyInvestTeste.Model.TesouroDireto
{
    public class tds
    {
        #region Propeties

        public double valorInvestido { get; set; }

        public double valorTotal { get; set; }

        public DateTime vencimento { get; set; }

        public DateTime dataDeCompra { get; set; }

        public int iof { get; set; }

        public string indice { get; set; }

        public string tipo { get; set; }

        public string nome { get; set; }

        #endregion
    }
}
