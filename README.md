#  Botón de Pago Yappy – Librería de .NET

##  Descripción
Botón de Pago Yappy es el checkout oficial de Banco General para integraciones con .NET. Ofrezca Yappy como método de pago a más de 600 mil clientes y reciba pagos con Yappy directamente en su tienda o aplicación.

Rápido, seguro y fácil de implementar en su sitio web.

## Documentación

Encuéntrala en [www.bgeneral.com](Https://www.bgeneral.com/desarrolladores)

## Requerimientos mínimos

.NET Core 3.1

Una cuenta jurídica con Yappy

[Un certificado SSL](https://docs.woocommerce.com/document/ssl-and-https/)

## Resumen de implementación

Esta es una **guía de implementación básica** del la librería de .NET. Usted puede instalarlo como mejor se adapte a la configuración de su aplicación, siguiendo los pasos a continuación:

1. Descarga el archivo
2. Instalación del SDK
3. Configuración del SDK
4. Instalación de la librería del cliente (front-end)
5. Configuración de notificación instantánea de pago (opcional)
6. Conexión de prueba

## Guía de ejemplo
> Antes de comenzar la instalación, asegúrese de contar con las credenciales de conexión proporcionadas en la Banca en Línea Comercial del negocio (el ID del comercio es un valor alfanúmerico de 32 caracteres). Si no cuenta con las credenciales, siga los paso de la [Guía de activación del Botón de Pago Yappy](https://www.bgeneral.com/desarrolladores/boton-de-pago-yappy/activacion-del-boton-de-pago-yappy/).

### 1. Descarga del archivo de este repo.
### 2. Instalación de la librería (back-end)
**Copie los archivos y colóquelos en la configuración más apropiada de su servidor**. Recomendamos que sea una carpeta de fácil acceso para su desarrollo.

**– BgFirma.cs** (librería)

**– PagosbgController.cs** (actualización de estado del pedido)

### 3. Configuración del SDK

A continuación se describirá el paso a paso del proceso de configuración. Como parte de esta guía, contamos con un proyecto de ejemplo ejemplo.zip de referencia. (Descargable del repo).
  - Primero, coloque las credenciales proporcionadas en Banca en Línea Comercial en las variables de entorno del sistema. Para conocer sobre la conexión de prueba (sandbox) ir al paso 6.

```PHP
ID_DEL_COMERCIO=ID // ID del comercio
CLAVE_SECRETA=CLAVE // Clave secreta
MODO_DE_PRUEBAS=false // Al colocar el valor true, se realizarán compras de pruebas
```
  - Importe la librería en la página donde quiere utilizar el Botón de Pago Yappy.
```PHP
using BancoGeneral.Yappy;
```
  - Cree el objeto al instanciar la clase BgFirma y para generar el URL de éxito llame al método GenerateURL.
 ```PHP
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
```
A continuación se describen los campos para realizar la solicitud:

 | Nombre del campo       | Descripción                                                                                                                                                                                                                                                                                                |
 |------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
 | **Subtotal** (obligatorio) | El subtotal de su carrito antes de impuestos, este monto ya debe incluir costos de transportes (shipping) y descuentos. Solo aplican números positivos mayores a cero.                                                                                                                                     |
 | **Taxes** (obligatorio)    | Valor de los impuestos. Aplican cero o números positivos.                                                                                                                                                                                                                                                  |
 | **Total** (obligatorio)    | El monto total a cobrar al cliente. Debe ser la suma de taxes más subtotal. Solo aplican números positivos.                                                                                                                                                                                                |
 | **Order ID** (obligatorio) | Es el número único de pedido generado por su aplicación, por ejemplo un número de pedido o de cliente. Este quedará registrado en el reporte transaccional Banca en Línea Comercial y lo podrá utilizar para conciliar los movimientos. Máximo 15 caracteres alfanuméricos (no se permiten ñ, Ñ o tildes). |
 | **Success URL** (opcional) | Es el URL donde su cliente será regresado o redireccionado en caso de que la transacción sea ejecutada correctamente. Por ejemplo, una página de gracias.                                                                                                                                                  |
 | **Fail URL** (opcional)    | Es el URL donde su cliente será regresado o redireccionado en caso de que el cliente cancele la transacción o suceda un error.                                                                                                                                                                             |
 | **Discount** (opcional)    | Valor del descuento. Solo aplican números negativos o cero y máximo 2 dígitos.                                                                                                                                    |
 | **Shipping** (opcional)    | Valor de los gastos de envío. Solo aplican números positivos o cero y máximo 2 dígitos.                                                                                                                                     |
 | **Tel** (opcional)    | El número de celular del cliente. Solo se permiten números y 8 dígitos.                                                                                                                                             |

 ### 4. **Instalación de la librería del cliente (front-end)**
  - Para mostrar el Botón de Pago Yappy en su aplicación, coloque lo siguiente en su CSHTML:

  ```javascript
      <script src="https://ecom.stage.bgx-digital.com/cdn/yappy.js" type="text/javascript"></script>
   ```

  ![Visual del Botón de Pago Yappy](https://www.bgeneral.com/wp-content/uploads/2021/04/boton%20de%20pago%20libreria%20php%20(4).png)

## Funcionalidades extra
### Personalización del Botón de Pago Yappy
Opcionalmente, puede seleccionar el color del botón que se ajuste mejor al estilo de su tienda. Recomendamos la opción predeterminada.

![Brand (predeterminada)](https://www.bgeneral.com/wp-content/uploads/2020/10/Boton%20de%20pago%20woocommerce/brand.png)"Brand (predeterminada)"

![Dark](https://www.bgeneral.com/wp-content/uploads/2020/10/Boton%20de%20pago%20woocommerce/dark.png)"Dark"

![Light](https://www.bgeneral.com/wp-content/uploads/2020/10/Boton%20de%20pago%20woocommerce/light.png)"Light"

![White](https://www.bgeneral.com/wp-content/uploads/2020/10/Boton%20de%20pago%20woocommerce/white.png)"White"

  - Para personalizar el Botón de Pago Yappy (en un elemento **button** o **div**), se utilizan las siguiente clases CSS:
  - **yappy-ecom**: clase obligatoria.
  - **yappy-brand**: opción predeterminada. Permite seleccionar el estilo azul del Botón de Pago Yappy
  - **yappy-dark**: permite seleccionar el estilo negro del Botón de Pago Yappy
  - **yappy-light**: permite seleccionar el estilo celeste del Botón de Pago Yappy.
  - **yappy-white**: permite seleccionar el estilo blanco del Botón de Pago Yappy.

  ```javascript
<button type="submit" class="yappy-ecom yappy-brand "></button>
```
- yappy-donation: permite reemplazar el botón de pago por uno para donaciones. Aplican los mismos estilos de color.

```javascript
<div class="yappy-ecom yappy-dark yappy-donation"></div>
```
### 5. Configuración de notificación instantánea de pago (opcional)

El Botón de Pago Yappy le permite a su servidor recibir el estado de las transacciones. Esta comunicación permite cerrar el ciclo de su pedido y automatizar los procesos de venta.

El archivo **PagosbgController.cs** se utiliza para validar la información que envía el banco y actualizar el estado del pedido en su aplicación. El Banco le enviará un mensaje de confirmación a su servidor (por ejemplo: https://mitienda.com/pagosbg) de manera asíncrona al flujo de la transacción.

El estado del pedido se puede encontrar en **[FromQuery] string status**. Este query param puede devolver uno de los siguientes estados:

- “E” para **Ejecutado**. El cliente confirmó el pago y se completó la compra.

- “R” para **Rechazado**. Cuando el cliente no confirma el pago dentro de los cinco minutos que dura la vida del pedido.

- “C” para **Cancelado**. El cliente inició el proceso, pero canceló el pedido en el app de Banco General.

```javascript
public IActionResult Get(
    [FromQuery] string orderId,
    [FromQuery] string status,
    [FromQuery] string hash,
    [FromQuery] string domain,
    [FromQuery] string confirmationNumber)
{

    if (BGFirma.VerifyParams(orderId, status, domain, hash))
    {
        // La firma es válida, se debe cambiar el estado de la orden en la base de datos...
        return Ok(new { success = true, confirmation = confirmationNumber });
    }
    else
    {
        return Ok(new { success = false });
    }
}
```
Si la variable **BGFirma.VerifyParams = true**, significa que la transacción fue exitosa y puede continuar con su proceso de negocio.

>Nota: El Banco también le notificará el estado exitoso de un pedido por medio de una confirmación enviada por correo electrónico.

### 6.Conexión de pruebas
El modo de pruebas permite simular transacciones utilizando números de teléfono de prueba para visualizar el flujo de compra sin realizar un pedido real. Debe tener en cuenta que:
- El modo de pruebas **sólo funciona con los números de teléfono de pruebas** (se detallan a continuación).
- El modo de pruebas no descuenta inventario.
- Recuerde deshabilitar el modo de pruebas para aceptar transacciones reales.

| 6000-0000                | 6000-0002                |
|:------------------------:|:------------------------:|
| Transacciones realizadas | Transacciones canceladas |

![Visual del Botón de Pago Yappy](https://www.bgeneral.com/wp-content/uploads/2020/11/Magento%204.png)

Para activar el modo de pruebas, cambie la variable de entorno **MODO_DE_PRUEBAS**:

```javascript
MODO_DE_PRUEBAS=true
```
De esta manera, cada vez que se realice un pago, estará en el modo de prueba y no se debitará de su cuenta.

## Tabla de códigos de error

| Código del error       | Descripción                                                                                                                                                                                                                                                                                                |
|------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **EC-000** | Error genérico.                                                                                                              |
| **EC-001**    | Dominio con formato incorrecto.                                                                                                                                                        |
| **EC-002**    | Credenciales inválidas.                                                                                                                                                        |
| **EC-003**    | No hay certificado SSL en el dominio.                                                                                                                                                        |
| **EC-004**    | El total no cuadra con el subtotal e impuesto enviado.                                                                                                                             |
| **EC-005**    | Formato del total inválido.                                                                                                                                |
| **EC-006**    | Formato del subtotal inválido.                                                                                                                        |
| **EC-007**    | Formato de impuesto inválido.                                                                                                                             |
| **EC-008**    | Formato de descuento inválido.                                                                                                                              |
| **EC-009**    | Formato de shipping inválido.                                                                                                                                           |
| **EC-010**    | Formato de número de pedido inválido.                                                                                                                              |
| **EC-011**    | Formato de celular inválido.                                                                                                                                  |
| **EC-012**    | Formato de URL de éxito inválido.                                                                                                                                  |
| **EC-013**    | Formato de URL de fallo inválido.                                                                                                                                          |

## Preguntas frecuentes

**1. Como comercio, ¿recibiré alguna notificación de las compras realizadas por Yappy?**

Sí. Cada vez que se realice una compra por Yappy, recibirá un correo de confirmación con el número de pedido, el monto, la hora de la transacción, el número de confirmación de la transacción y el nombre del cliente. Como método opcional, el comercio puede recibir una notificación instantánea de pago.

**3. ¿Dónde puedo conseguir mis credenciales para el Botón de Pago Yappy?**

Sus credenciales las puede conseguir en Banca en Línea Comercial. Siga este enlace para verificar los pasos de generación de credenciales.

**4.¿Para qué países está disponible Yappy?**

El Botón de Pago solo está disponible para Panamá.

**5. Veo un mensaje que dice “Algo salió mal, contacta al administrador.” ¿Qué debo hacer?**
- Asegúrese de que su sitio web cumple con los requerimientos mínimos.
- Revise que cuenta con la versión más reciente del módulo.
- Confirme que sus credenciales son correctas y que fueron colocadas en los campos correspondientes.
- Confirmar que el URL de su tienda está registrado correctamente en el perfil de su Botón de Pago Yappy en Banca en Línea Comercial.

**6. ¿Dónde puedo ver reportes de ventas de mi Botón de Pago Yappy?**

Las ventas del Botón de Pago se verán reflejadas en su Banca en Línea Comercial. Acceda a las mismas desde Reportes > Reportes Yappy.

## Resolución de problemas

¿Tiene algún problema? Siga estos pasos para asegurar la correcta configuración de la extensión:
- Asegúrese que su sitio cumple con los requerimientos mínimos.
- Revise que cuenta con la versión más reciente del módulo.
- Revise las preguntas frecuentes por si su pregunta se ve reflejada en las mismas.
- Confirme que sus credenciales son correctas y que fueron colocadas en los campos correspondientes.
- Confirme que no está habilitado el modo de pruebas (sandbox).
- Revise que el dominio de su tienda (URL) sea igual al que definió en su perfil de Botón de Pago en Banca en Línea Comercial, ya que se utilizará para validar la solicitud.

## ¿Tiene alguna pregunta?
Consulte nuestra sección de Preguntas Frecuentes o de Resolución de problemas para resolver los inconvenientes más comunes. Si requiere mayor soporte, comuníquese con nosotros por correo electrónico a botondepagoyappy@bgeneral.com.
