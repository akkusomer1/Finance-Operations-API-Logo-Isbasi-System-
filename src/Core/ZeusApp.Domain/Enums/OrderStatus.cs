using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeusApp.Domain.Enums;
public enum OrderStatus
{
    /// <summary>
    /// Kapanmış
    /// </summary>
    Closed,
    /// <summary>
    /// Bekleyen
    /// </summary>
    waiting,

    /// <summary>
    /// Sevk ediliyor-
    /// Eğer ürünün bir kısmı faturalandırıldıysa ve bi kısmıda kaldıysa bu durumda Sevk ediliyor durumuna düşer.
    /// </summary>
    sending
}
