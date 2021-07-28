using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BancoGeneral.Yappy;

namespace Ecommerce.NET.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        { }
        public void OnPost()
        {
            var bgFirma = new BGFirma(
                domain: "https://www.mitienda.com", // Dominio registrado en Banca en Línea Comercial
                total: 10.70,
                subtotal: 10.00,
                taxes: 0.70,
                successUrl: "https://www.mitienda.com/success?orderId=123",
                failUrl: "https://www.mitienda.com/fail?orderId=123",
                orderId: "123",
                discount: 0.00,
                shipping: 0.00,
                tel: "66666666"
            );

            var yappyPayment = bgFirma.GenerateURL();

            // "yappyPayment" cuenta con la información necesaria para procesar el pago,
            // el cliente es libre de manejar el redirect hacia la URL generada,
            // así como el manejo del error en caso de fallo.

            // Información acerca de "yappyPayment":
            //  * yappyPayment.success -> Booleano que indica si se ha generado la URL para el redirect
            //  * yappyPayment.url -> Contiene la URL con la información del pago
            //  * yappyPayment.error -> Contiene información del error que no permitió generar la URL
        }
    }
}
