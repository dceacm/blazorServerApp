# blazorServerApp
Aplicación **Blazor Server** ASP.NET Core 7 que consume una API externa usando *OpenApiReference* para generar el código de la API y *IHttpClientFactory* para gestionar eficientemente las peticiones HTTP.

- Acerca de *OpenApiReference*. [Leer más](https://stevetalkscode.co.uk/openapireference-commands#codegenerator-element)
- Acerca de *IHttpClientFactory*. [Leer más](https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory)

## ¿Cuál es el objetivo?
En este proyecto, se explicará como poder gestionar y controlar las excepciones no controladas en el Front end de una aplicación **Blazor Server**.

## Esquema
El siguiente esquema representa el funcionamiento de lo que se quiere explicar:

<img width="1010" alt="esquema" src="https://github.com/dceacm/blazorServerApp/assets/42771648/eef9ae2e-9eae-429d-9dbf-956bf15434d9">

## Soluciones separadas
Como representa el esquema, se ha creado una solución para el Front end y otra solución para el Back end.
- ### Blazor Server App
  El código donde puedes descargar la aplicación se encuentra en: https://github.com/dceacm/blazorServerApp
- ### Web API
  El código donde puedes descargar la API se encuentra en: https://github.com/dceacm/webApi
  
## Excepciones en Blazor Server

### Comportamiento por defecto
Cuando se produce una excepción no controlada, **Blazor Server** la trata como un *fatal error* y deja la aplicación en un estado indefinido. Como resultado, la aplicación deja de funcionar, y el usuario recibe un mensaje como *"An unhandled error has occurred."*.

![image](https://github.com/dceacm/blazorServerApp/assets/42771648/6d979e0b-4004-4969-bc4d-86922b9a4efd)

### Solución 1
#### Envolviendo el *body* con el componente *ErrorBoundary*
Una posible solución para mejorar esto, es envolver el *body* con el componente *ErrorBoundary*. De esta manera, estaremos atacando globalmente el problema, ya que cualquier contenido que provoque una excepción será manejada por el componente *ErrorBoundary*.

Para ello, hay que modificar el componente *MainLayout* y envolver el *body* con el componente *ErrorBoundary*.

<div class="page">
    <div class="sidebar">
        <NavMenu />
    </div>

    <main>
        <div class="top-row px-4">
            <a href="https://docs.microsoft.com/aspnet/" target="_blank">About</a>
        </div>

        <article class="content px-4">
            <ErrorBoundary>
                @Body
            </ErrorBoundary>
        </article>
    </main>
</div>

Con lo anterior, obtendremos el siguiente resultado:

![image](https://github.com/dceacm/blazorServerApp/assets/42771648/aa081730-0a17-4f1d-9d4e-952896a78d9d)

Probablemente, lo anterior no se suficiente, o no al menos para todos los casos, ya que el único mensaje que nos proporciona *ErrorBoundary*, es *"An error has ocurred."*. Si bien es verdad que aparece un mensaje más amigable para el usuario, a nosotros como desarrolladores no nos servirá de mucho. Por ello la *Solución 2* es más acertada.

### Solución 2
#### Envolviendo el *body* con el componente personalizado *ErrorBoundaryCustom*

Otra solución, es crear un componente personalizado para el tratamiento de las excepciones. En este caso, se ha creado un componente llamado *ErrorBoundaryCustom*, que trata tanto las excepciones de tipo *Exception* como las excepciones producidas por la API, como *ApiException*.

Este es el código de la parte visual del componente:
```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
    }
}
```

Este es el contendo del código:
```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
    }
}
```


En el *MainLayout* en lugar de llamar al componente *ErrorBoundary* llamaremos al componente *ErrorBoundaryCustom*.

<div class="page">
    <div class="sidebar">
        <NavMenu />
    </div>

    <main>
        <div class="top-row px-4">
            <a href="https://docs.microsoft.com/aspnet/" target="_blank">About</a>
        </div>

        <article class="content px-4">
            <ErrorBoundaryCustom>
                @Body
            </ErrorBoundaryCustom>
        </article>
    </main>
</div>

Ahora obtendremos el resultado bastante más detallado, con el mensaje de la excepción en el primer contenedor y con el *StackTrace* en el segundo contenedor:

![image](https://github.com/dceacm/blazorServerApp/assets/42771648/dc4522e4-0b48-42e4-b4bf-1bc1455a7587)

