using System;

namespace EasyInvestTeste.Model.RendaFixa
{
    public class lci
    {
        #region Properties

        public double capitalInvestido { get; set; }

        public double capitalAtual { get; set; }

        public double quantidade { get; set; }

        public DateTime vencimento { get; set; }

        public double iof { get; set; }

        public double outrasTaxas { get; set; }

        public double taxas { get; set; }

        public string indice { get; set; }

        public string tipo { get; set; }

        public string nome { get; set; }

        public bool guarantidoFGC { get; set; }

        public DateTime dataOperacao { get; set; }

        public double precoUnitario { get; set; }

        public bool primario { get; set; }

        #endregion
    }
}
