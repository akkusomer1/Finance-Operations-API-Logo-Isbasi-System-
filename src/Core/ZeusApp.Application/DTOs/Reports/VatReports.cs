using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeusApp.Application.DTOs.Reports;
public class VatReports
{
    public int Id { get; set; }
    /// <summary>
    /// "Dönem"
    /// </summary>
   
    public string Period { get; set; }

    /// <summary>
    /// "Satın Alma Kdv Matrahı"
    /// </summary> 
    public decimal PurchasingVatBase { get; set; }

    /// <summary>
    /// "Satın Alma Kdv "
    /// </summary> 
    public decimal PurchasingVat { get; set; }
    /// <summary>
    /// "Satın Alma İade Kdv Matrahı"
    /// </summary> 
       
    public decimal PurchasingReturnVatBase { get; set; }

    /// <summary>
    /// "Satın Alma İade Kdv "
    /// </summary> 
    public decimal PurchasingReturnVat { get; set; }


    /// <summary>
    /// "Satış Kdv Matrahı "
    /// </summary> 
    public decimal SalesVatBase { get; set; }

    /// <summary>
    /// "Satış Kdv "
    /// </summary> 
    public decimal SalesVat { get; set; }

    /// <summary>
    /// "Satış İade Kdv Matrahı "
    /// </summary> 
    
    public decimal SalesReturnVatBase { get; set; }
    /// <summary>
    /// "Satış İade Kdv  "
    /// </summary> 
   
    public decimal SalesReturnVat { get; set; }
    /// <summary>
    /// "Toplam Kdv Ödenecek "
    /// </summary> 
    
    public decimal TotalVatPayable { get; set; }

    /// <summary>
    /// "Toplam Kdv Alınacak "
    /// </summary> 
    public decimal TotalVatCharged { get; set; }
}
