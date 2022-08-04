## DIGEIG Diagram App

Es una app con el objetivo de crear diagramas de forma dinamica para representacion organizacional de las intituciones gubernamentales.

## Estado del Proyecto

Este proyecto es actualmente en Desarrollo.

## Instrucciones de instalación y configuración

Clona este repositorio. Necesitará `node` y`npm` instalados globalmente en su máquina.

Instalación:

`npm install`

Para inicializar la Aplicacion:

`npm run serve`

Para visitar la aplicación:

`localhost:8080/nstitutionStructure/(id de la institución)`

## Produccion

### Comando para compilar proyecto:

`npm run build`

### Variables de entorno

`env`

Esta variable apunta al endpoint del API local.

`env.test`

Esta variable apunta al endpoint del API de test.

`env.production`

Esta variable apunta al endpoint del API de Produccion.

### Archivo de configuracion

`vue.config.js`

Este contiene la configuracion para la puesta en produccion e indicar la ruta en la cual se inicializara la aplicación haciendo uso la variable de entorno dependiendo el ambiente en el que trabajamos.

:star: Por el Equipo de Software de la DIGEIG.
