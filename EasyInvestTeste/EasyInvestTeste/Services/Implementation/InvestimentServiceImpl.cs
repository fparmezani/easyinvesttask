using easyinvestteste.helper;
using EasyInvestTeste.Model;
using EasyInvestTeste.Model.RendaFixa;
using EasyInvestTeste.Model.TesouroDireto;
using System;
using System.Collections.Generic;

namespace EasyInvestTeste.Services.Implementation
{
    public class InvestimentServiceImpl : BaseService, IInvestimentService
    {
        #region Variables

        private static readonly string _uri = "http://www.mocky.io/";

        #endregion

        #region Constructor

        public InvestimentServiceImpl() : base(_uri)
        {

        }

        #endregion

        #region Other Methods

        public Wallet Find() {

            var wallet = new Wallet();
            var investments = FindAll();

            foreach (var item in investments)
            {
                wallet.valorTotal += item.valorTotal;
            }

            wallet.Investimentos = investments;

            return wallet;
        }


        public List<Investiment> FindAll()
        {
            var investiments = new List<Investiment>();

            var fundo = Post<Fundo>("v2/5e342ab33000008c00d96342", null);

            var rendafixa = Post<RendaFixa>("v2/5e3429a33000008c00d96336", null);

            var tesourodireto = Post<TesouroDireto>("v2/5e3428203000006b00d9632a", null);

            getFundosToInvestiments(fundo, investiments);

            getRendaFixaToInvestiments(rendafixa, investiments);

            getTesouroDiretoToInvestiments(tesourodireto, investiments);

            return investiments;
        }


        public void getFundosToInvestiments(Fundo fundo, List<Investiment> investments)
        {

            foreach (var item in fundo.fundos)
            {
                investments.Add(new Investiment()
                {
                    nome = item.nome,
                    valorInvestido = item.capitalInvestido,
                    valorTotal = item.valorAtual,
                    vencimento = item.dataResgate,
                    Ir = getIR(item.valorAtual, item.capitalInvestido, Aplicacao.RendaFixa),
                    valorResgate = calcRescue(item.dataCompra, item.dataResgate, item.capitalInvestido)
                });
            }
        }

        public void getTesouroDiretoToInvestiments(TesouroDireto tesouro, List<Investiment> investments)
        {
            foreach (var item in tesouro.tds)
            {
                investments.Add(new Investiment()
                {
                    nome = item.nome,
                    valorInvestido = item.valorInvestido,
                    valorTotal = item.valorTotal,
                    vencimento = item.vencimento,
                    Ir = getIR(item.valorTotal, item.valorInvestido, Aplicacao.TesouroDireto),
                    valorResgate = calcRescue(item.dataDeCompra, item.vencimento, item.valorInvestido)
                });
            }
        }

        /// <summary>
        /// Recupera o valor do resgate se for Renda Fixa
        /// </summary>
        /// <param name="renda">valor Renda Fixa</param>
        /// <param name="investments">Lista de Investimentos</param>
        public void getRendaFixaToInvestiments(RendaFixa renda, List<Investiment> investments)
        {
            foreach (var item in renda.lcis)
            {
                investments.Add(new Investiment()
                {
                    nome = item.nome,
                    valorInvestido = item.capitalInvestido,
                    valorTotal = item.capitalAtual,
                    vencimento = item.vencimento,
                    Ir = getIR(item.capitalAtual, item.capitalInvestido, Aplicacao.Fundo),
                    valorResgate = calcRescue(item.dataOperacao, item.vencimento, item.capitalInvestido)
                });
            }
        }

        /// <summary>
        /// Retorna o valor do IR do Investimento de acordo com o tipo de aplicação
        /// </summary>
        /// <param name="valorTotal">Valor Total</param>
        /// <param name="valorInvestido">Valor Investido</param>
        /// <param name="tipo">Tipo de Investimento (1 = Fundo, 2, RendaFixa, )</param>
        /// <returns>Valor do IR</returns>
        public double getIR(double valorTotal, double valorInvestido, Aplicacao aplicacao)
        {
            double ir = 0.0;

            var rentabilidade = (valorTotal - valorInvestido);

            switch (aplicacao)
            {
                case Aplicacao.Fundo:
                    ir = (rentabilidade - (0.85 * rentabilidade)); // 15%
                    break;
                case Aplicacao.RendaFixa:
                    ir = (rentabilidade - (0.95 * rentabilidade)); // 5%
                    break;
                case Aplicacao.TesouroDireto:
                    ir = (rentabilidade - (0.9 * rentabilidade)); // 10%
                    break;
                default:
                    break;
            }

            return ir;
        }

        /// <summary>
        /// Calcula o Valor do Resgate de Acordo com a Regra definida
        ///  1- Investimento com mais da metade do tempo em custodia perde 15% do valor investido
        ///  2 - Investimento com até 3 meses para vencer - Perde 6% do valor investido
        ///  3 - Outros - 30% do valor investido
        /// </summary>
        /// <param name="dataCompra">Data da compra do investimento</param>
        /// <param name="dataResgate">Data do resgate do investimento</param>
        /// <param name="capitalInvestido">Valor do campital investido</param>
        /// <returns> Valor do Resgate</returns>
        public double calcRescue(DateTime startDate, DateTime rescueDate, double valueInvestment)
        {
            var rescueValue = 0.0;

            //caso investimento com mais da metade do tempo em custódia - 15% valor investido
            if (IsHalfCustodyTime(startDate, rescueDate))
            {
                rescueValue = (0.85 * valueInvestment); //perde 15%
            }
            else if (TreeMonths(startDate, rescueDate))//Investimento com até 3 meses para vencer
            {
                rescueValue = (0.94 * valueInvestment); //perde 6%
            }
            else //outros 
            {
                rescueValue = (0.70 * valueInvestment); //perde 30%
            }

            return rescueValue;
        }

        /// <summary>
        /// Verifica se o prazo é metade da custodia
        /// </summary>
        /// <param name="startDate">Data da Compra</param>
        /// <param name="rescueDate">Data do Resgate</param>
        /// <returns></returns>
        public bool IsHalfCustodyTime(DateTime startDate, DateTime rescueDate) {

            bool result;
            try
            {
                var dateDiffTotal = new CalcDates(startDate, rescueDate).totalDays;

                var halfTime = dateDiffTotal / 2;

                var dateDiffToday = new CalcDates(startDate, DateTime.Now).totalDays;

                result = (halfTime >= dateDiffToday);
            }catch{
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Investimento com até 3 meses para vencer
        /// </summary>
        /// <param name="startDate">Data da Compra</param>
        /// <param name="rescueDate">Data do Resgate</param>
        /// <returns></returns>
        public bool TreeMonths(DateTime startDate, DateTime rescueDate)
        {

            bool result;
            try
            {
                var dateDiffTotal = new CalcDates(startDate, rescueDate).totalDays;

                var dateDiffToday = new CalcDates(startDate, DateTime.Now).totalDays;

                var days = dateDiffTotal - dateDiffToday;

                result = (days <= 90);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        #endregion

    }
}
