# ProyectoRedes
Subasta - Aplicación Cliente-Servidor 🛍️💻
Este repositorio contiene una aplicación de subastas implementada mediante una arquitectura cliente-servidor. ¡Participa en emocionantes subastas, realiza ofertas y descubre quién se lleva el premio!

Características 🌟
Servidor 🖥️
El servidor controla el proceso de subasta y brinda funciones clave:

Espera al Administrador: El servidor aguarda la conexión del administrador para ingresar detalles iniciales.
Detalles de la Subasta: Los detalles se envían a todos los clientes conectados.
Manejo de Participantes: Los clientes envían ofertas y reciben actualizaciones en tiempo real.
Determinación del Ganador: Al final, el servidor identifica al ganador y envía un mensaje.
Cliente 🤝
Los participantes, como administradores o clientes regulares, tienen funciones específicas:

Administrador:

Ingresa detalles iniciales y se desconecta.
Cliente Regular:

Participa realizando ofertas durante la subasta.
Instrucciones de Uso 📝
Servidor
Ejecuta la aplicación del servidor.
Espera al administrador para ingresar detalles iniciales.
Los participantes pueden unirse.
Cliente
Ejecuta la aplicación del cliente.
Elige el rol: Administrador o Cliente.
Ingresa la dirección IP del servidor.
Administrador: Proporciona detalles iniciales.
Cliente: Ingresa tu nombre y participa en la subasta.
Notas Adicionales 📌
Clientes Concurrentes: El servidor maneja múltiples clientes simultáneamente con hilos.
Actualizaciones en Tiempo Real: Los participantes reciben actualizaciones en tiempo real.
Determinación del Ganador: El servidor determina al ganador y genera un documento de texto.
Requisitos del Sistema ⚙️
Plataforma: .NET Framework
Idioma de Programación: C#
Cómo Contribuir 🚀
¡Fork del repositorio!
Crea una rama: git checkout -b nueva-funcionalidad
Realiza cambios y haz commit: git commit -m 'Añade nueva funcionalidad'
¡Push a la rama: git push origin nueva-funcionalidad
Envía un pull request
Autores 🧑‍💻
Este programa fue desarrollado por [Tu Nombre].

¡Disfruta de la emoción de las subastas en tiempo real! 🚀🎉





