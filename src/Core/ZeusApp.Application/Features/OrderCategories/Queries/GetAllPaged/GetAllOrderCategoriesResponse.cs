using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeusApp.Application.Features.OrderCategories.Queries.GetAllPaged;
public class GetAllOrderCategoriesResponse
{
    public int Id { get; set; }
    [Display(Name = "Kategori")]
    public string Name { get; set; }

}
