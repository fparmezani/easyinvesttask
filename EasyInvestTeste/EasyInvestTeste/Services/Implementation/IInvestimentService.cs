using EasyInvestTeste.Model;
using System.Collections.Generic;

namespace EasyInvestTeste.Services.Implementation
{
    interface IInvestimentService
    {
        List<Investiment> FindAll();
    }
}
