using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeusApp.Application.Features.LoanCategories.Queries.GetAllPaged;
public class GetAllLoanCategoriesResponse
{
    public int Id { get; set; }
    [Display(Name = "Kategori")]
    public string Name { get; set; }
}
