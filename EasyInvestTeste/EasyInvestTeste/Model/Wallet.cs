using System.Collections.Generic;

namespace EasyInvestTeste.Model
{
    public class Wallet
    {
        public double valorTotal { get; set; }

        public ICollection<Investiment> Investimentos { get; set; }

    }
}
