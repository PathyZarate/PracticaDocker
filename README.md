Para ejecutar el proyecto lo primero que se debe tener son los siguientes paquetes instalados en la version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore -v 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design -v 8.0.0 
dotnet add package Microsoft.EntityFrameworkCore.Tools -v 8.0.0  
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 8.0.0 
dotnet add package Microsoft.AspNetCore.Mvc -v 8.0.0 
dotnet add package Microsoft.AspNetCore.OpenApi -v 8.0.0

Con el comando: dotnet list package veremos que los tengamos instalados
En caso de tener los paquetes en otra version se deben cambiar las versiones en el archivo tienda.csproj y luego ejecutar los comandos:
dotnet clean
dotnet restore
dotnet build

Y una vez con este hecho lo que se debe hacer es crear la imagenes levantar el contenedor con los siguientes comandos:
docker compose build
docker compose up

Una vez esto levantado debemos ir a postman y poner el siguiente link para listar:
http://localhost:8001/api/Producto/Listar
http://localhost:8001/api/Serie/Listar
Aqui se debe tomar en cuenta que en mi codigo program.cs yo introduje una linea de espera de 10 segundos para el sql por lo que se debe volver al intentar en ese tiempo si no da el link al primer intento.
Y para agregar datos debemos poner:
http://localhost:8001/api/Producto
http://localhost:8001/api/Serie
