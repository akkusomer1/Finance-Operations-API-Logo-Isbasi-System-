using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Cases.Queries.GetById;
public class GetCaseByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CurrencyType Currency { get; set; }

    public decimal OpeningBalance { get; set; }

    public DateTime OpeningBalanceDate { get; set; }
}
