El proyecto Parking Manager se integra a través de 4 repositorios:
* [ClientApp](https://github.com/parkingmanager/ClientApp)
* [DataBase](https://github.com/parkingmanager/RestAPI)
* [Utils](https://github.com/parkingmanager/Utils)
* [RestAPI](https://github.com/parkingmanager/RestAPI)
# RestAPI
Api Rest del proyecto parking manager

El API Rest en la aplicación tiene como objetivo que dos sistemas de computación intercambien información de manera segura a través de Internet, en este caso la comunicación se realiza entre el componente de cliente (Aplicación parking manager de Python) y el almacenamiento de datos (API RestFul Backend). A través de métodos permite consultar, crear, editar y eliminar información de negocio en la base de datos, todo a través de servicios utilizando el protocolo HTTP

Estructura de la aplicación:

1. Parking.Web
  
    El proyecto WebApi2 Parking.Web contiene la interfaz RESTFul que se encarga de exponer a internet los servicios necesarios para el funcionamiento de la solución.        Utiliza controladores para organizar conjuntos de métodos de diferentes entidades,que permiten organizar las funcionalidades del API de forma estandarizada y estructurada, los controladores existentes son los siguientes: 
    
    ![img](https://github.com/parkingmanager/RestApi/blob/main/README_DATA/1.png)
    
    Internamente cada controlador posee los métodos de servicio que realizan las operaciones. Cabe resaltar que unicamente se encarga de gestionar las particularidades del protocolo HTTP y no de transformar información, unicamente la entrega al componente de Core.

2. Parking.Core


#### Visite los repositorios de [ClientApp](https://github.com/parkingmanager/ClientApp.git) y [DataBase](https://github.com/parkingmanager/Database.git) para poder correr el proyecto.
