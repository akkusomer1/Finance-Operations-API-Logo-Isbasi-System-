namespace ZeusApp.Application.DTOs.Contact;

public class ContactRequest
{

    /// <summary>
    /// Adres
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Ülke
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// İl
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// İlçe
    /// </summary>
    public string District { get; set; }

    /// <summary>
    /// Posta Kodu
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// E-posta
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Fax
    /// </summary>
    public string Fax { get; set; }

    /// <summary>
    /// Telefon-1
    /// </summary>
    public string PhoneNumber1 { get; set; }

    /// <summary>
    /// Telefon-2
    /// </summary>
    public string PhoneNumber2 { get; set; }

    /// <summary>
    /// Web Sitesi
    /// </summary>
    public string Website { get; set; }
}
