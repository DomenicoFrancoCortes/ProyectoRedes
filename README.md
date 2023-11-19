# Subasta - Aplicación Cliente-Servidor 🛍️💻

Este repositorio contiene una aplicación de subastas implementada mediante una arquitectura cliente-servidor. Participa en emocionantes subastas, realiza ofertas y descubre quién se lleva el premio!

## Características 🌟

### Servidor 🖥️

- **Espera al Administrador:** El servidor aguarda la conexión del administrador para ingresar detalles iniciales.
- **Detalles de la Subasta:** Los detalles se envían a todos los clientes conectados.
- **Manejo de Participantes:** Los clientes envían ofertas y reciben actualizaciones en tiempo real.
- **Determinación del Ganador:** Al final, el servidor identifica al ganador y envía un mensaje.

### Cliente 🤝

- **Administrador:**
  - Ingresa detalles iniciales y se desconecta.

- **Cliente Regular:**
  - Participa realizando ofertas durante la subasta.

## Instrucciones de Uso 📝

### Servidor

1. Ejecuta la aplicación del servidor.
2. Espera al administrador para ingresar detalles iniciales.
3. Los participantes pueden unirse.

### Cliente

1. Ejecuta la aplicación del cliente.
2. Elige el rol: Administrador o Cliente.
3. Ingresa la dirección IP del servidor.
4. Administrador: Proporciona detalles iniciales.
5. Cliente: Ingresa tu nombre y participa en la subasta.

## Notas Adicionales 📌

- **Clientes Concurrentes:** El servidor maneja múltiples clientes simultáneamente con hilos.
- **Actualizaciones en Tiempo Real:** Los participantes reciben actualizaciones en tiempo real.
- **Determinación del Ganador:** El servidor determina al ganador y genera un documento de texto.

## Requisitos del Sistema ⚙️

- **Plataforma:** .NET Framework
- **Idioma de Programación:** C#

## Cómo Contribuir 🚀

1. ¡Fork del repositorio!
2. Crea una rama: `git checkout -b nueva-funcionalidad`
3. Realiza cambios y haz commit: `git commit -m 'Añade nueva funcionalidad'`
4. ¡Push a la rama: `git push origin nueva-funcionalidad`
5. Envía un pull request

## Autores 🧑‍💻

Este programa fue desarrollado por Domenico Franco, Daniela Arriazu , Pablo Blanco, Tatiana Ainchil, Miguel Nina, Ariel Ledezma.

¡Disfruta de la emoción de las subastas en tiempo real! 🚀🎉
