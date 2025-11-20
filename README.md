# 📚 Librería Digital - Proyecto ASP.NET Core Web API  
**Autor:** Juan Pablo Gutiérrez A.  
**Curso:** Desarrollo de Software  
**Institución:**  ITM 
**Fecha:** 17 de Octubre de 2025  

---

## 🧠 Descripción General

Este proyecto consiste en el desarrollo de una **Web API** en **ASP.NET Core** que permite a los usuarios **gestionar su propia colección de libros digitales**.  
Cada usuario puede:
- Registrar su cuenta.  
- Agregar libros a su biblioteca personal.  
- Calificarlos con estrellas (1 a 5).  
- Escribir reseñas sobre ellos.  

El sistema está construido con una arquitectura limpia basada en **Entity Framework Core**, el **patrón Repository**, y desplegado en **contenedores Docker** junto a una instancia de **SQL Server**.

---

## ⚙️ Tecnologías Utilizadas

- **ASP.NET Core 8.0** – Framework principal para la API.
- **Entity Framework Core** – ORM para la persistencia de datos.
- **SQL Server 2022 (Docker)** – Base de datos.
- **Docker Compose** – Orquestación de contenedores.
- **Swagger UI** – Pruebas e interacción con la API.
- **C#** – Lenguaje de programación.
- **JSON** – Formato de intercambio de datos.

---

## 🧩 Estructura del Proyecto

```
LibreriaDigital/
 ┣ src/
 ┃ ┗ Libreria.Api/
 ┃   ┣ Controllers/      → Controladores de endpoints
 ┃   ┣ Models/           → Clases de dominio (User, Book, UserBook)
 ┃   ┣ Repositories/     → Implementación del patrón Repository
 ┃   ┣ Data/             → Contexto de base de datos (AppDbContext)
 ┃   ┣ Program.cs        → Configuración principal del proyecto
 ┃   ┗ appsettings.json  → Configuración de conexión
 ┣ Dockerfile
 ┣ docker-compose.yml
 ┗ README.md
```

---

## 🧱 Modelos Principales

### 🧍‍♂️ User
```csharp
Id, Nombre, Apellido, Email, PasswordHash
```

### 📘 Book
```csharp
Id, Titulo, Autor, AnioPublicacion, ImagenPortadaUrl (opcional)
```

### 🔗 UserBook
Relaciona usuarios con libros y permite:
```csharp
UserId, BookId, Rating (1–5), Review, AddedAt
```

---

## 🧮 Patrón Repository

El proyecto implementa el patrón **Repository** para desacoplar la lógica de acceso a datos.  
Esto facilita:
- Reutilización del código.
- Mantenimiento limpio.
- Testeo más sencillo.

Ejemplo de repositorio:
```csharp
public interface IUserRepository {
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}
```

---

## 🐳 Ejecución con Docker

### 🧰 Requisitos previos
- Tener **Docker Desktop** instalado y en ejecución.
- Tener **.NET 8 SDK** si deseas ejecutar fuera del contenedor (opcional).

### ▶️ Pasos

1. Abre una terminal en la carpeta raíz del proyecto.  
2. Ejecuta:
   ```bash
   docker compose up --build
   ```
3. Espera hasta ver el mensaje:
   ```
   Now listening on: http://[::]:8080
   ```
4. Abre tu navegador y entra a:
   ```
   http://localhost:8080/swagger
   ```
5. ¡Listo! Ya puedes interactuar con los endpoints.

---

## 🔗 Endpoints Principales (Swagger)

### 👤 Usuarios
| Método | Endpoint | Descripción |
|--------|-----------|-------------|
| POST | `/api/users` | Registrar un usuario |
| GET | `/api/users` | Listar usuarios |
| PUT | `/api/users/{id}` | Editar usuario |
| DELETE | `/api/users/{id}` | Eliminar usuario |

### 📚 Libros
| Método | Endpoint | Descripción |
|--------|-----------|-------------|
| POST | `/api/books` | Agregar libro |
| GET | `/api/books` | Listar libros |
| PUT | `/api/books/{id}` | Editar libro |
| DELETE | `/api/books/{id}` | Eliminar libro |

### 📖 Colección de Usuario
| Método | Endpoint | Descripción |
|--------|-----------|-------------|
| POST | `/api/users/{userId}/collection/{bookId}` | Agregar libro a la colección |
| PUT | `/api/users/{userId}/collection/{bookId}/rate` | Calificar y reseñar libro |
| GET | `/api/users/{userId}/collection` | Ver colección del usuario |

---

## 🧪 Flujo de Prueba (Swagger)

1. **POST /api/users** → Crear usuario.  
2. **POST /api/books** → Crear libro.  
3. **POST /api/users/{userId}/collection/{bookId}** → Asociar libro.  
4. **PUT /api/users/{userId}/collection/{bookId}/rate** → Calificar libro.  
5. **GET /api/users/{userId}/collection** → Consultar colección completa.  

---

## 🎥 Guion de Video (para presentación)

> “Hola, soy Juan Pablo Gutiérrez y presento mi proyecto final: **Librería Digital**.  
> Este desarrollo fue construido con ASP.NET Core y SQL Server, implementando Entity Framework Core y el patrón Repository.  
> En el video muestro cómo la API corre en Docker, y desde Swagger registro un usuario, agrego un libro, lo asocio a una colección, lo califico, y finalmente consulto la colección del usuario.  
> Cumplo así los requisitos de registro, gestión y reseñas, demostrando el funcionamiento completo del sistema.”

---

## 🚀 Posibles Mejoras Futuras
- Implementar **JWT** para autenticación y seguridad.  
- Agregar **migraciones automáticas** con `dotnet ef`.  
- Crear una **interfaz web en Angular o React** para consumo del API.  
- Añadir **paginación, búsqueda y filtros avanzados**.

## 🧩 Integración agregada: Docker + GraphQL (.NET) + Axios + MongoDB

Este repo ahora incluye:
- `graphql-api/` – API GraphQL en .NET 8 con Hot Chocolate, JWT y MongoDB.
- `web/` – Frontend React + Axios que consume `/graphql`.
- `docker-compose.yml` – Orquesta `mongo`, `mongo-express`, `graphql-api`, `web`.

### Ejecutar
1) Copia `.env.example` a `.env` y ajusta `JWT_SECRET`.
2) `docker compose up --build`
   - Web: http://localhost:5173
   - GraphQL: http://localhost:5000/graphql
   - Mongo Express: http://localhost:8081

### Probar rápido
- En la web, haz **Login** con tu email (demo). Se genera un JWT de prueba.
- Crea una nota y luego se listan con `myNotes` (protegida).

PROYECTO FINAL — Integración Completa con Docker, GraphQL, MongoDB y Axios

Materia: Arquitectura de Software
Estudiante: Juan Pablo Gutiérrez
Profesor: —

🚀 Descripción del Proyecto

Este proyecto implementa una arquitectura moderna basada en contenedores que integra:

Backend en .NET 8 usando GraphQL (HotChocolate)

Base de datos NoSQL MongoDB

Autenticación JWT

Frontend (Vite + React) consumiendo GraphQL mediante Axios

Orquestación con Docker y Docker Compose

El objetivo es demostrar el uso real de tecnologías actuales y patrones de desarrollo utilizados en aplicaciones de producción.

🧱 Tecnologías Utilizadas
Tecnología	Uso
Docker & Docker Compose	Orquestación de contenedores
.NET 8	Backend
HotChocolate GraphQL	API GraphQL
MongoDB	Base de Datos NoSQL
JWT	Manejo de autenticación
React + Vite	Interfaz de usuario
Axios	Cliente HTTP para consumir GraphQL
🐳 Ejecución con Docker (Producción / Demo)
1️⃣ Clonar el repositorio
git clone https://github.com/tu-repo/proyecto.git
cd proyecto

2️⃣ Crear archivo .env (si no existe)

Ejemplo:

JWT_SECRET=supersecreto
MONGO_INITDB_ROOT_USERNAME=admin
MONGO_INITDB_ROOT_PASSWORD=admin123

3️⃣ Ejecutar con Docker Compose
docker-compose up --build

4️⃣ Acceder a los servicios:
Servicio	URL
Frontend (React + Axios)	http://localhost:5173

GraphQL Studio (.NET)	http://localhost:8080/graphql

Mongo Express	http://localhost:8081
🔌 Consumiendo GraphQL desde Axios

Ejemplo real desde /web/src/api.js:

import axios from "axios";

export const graphQLClient = async (query, variables = {}) => {
  const res = await axios.post("http://localhost:8080/graphql", {
    query,
    variables
  });
  return res.data;
};


Ejemplo de query:

const query = `
  query {
    notes {
      id
      title
      content
    }
  }
`;

graphQLClient(query).then(console.log);

🧪 Probar GraphQL en el navegador

Ir a:

👉 http://localhost:8080/graphql

Ejemplo de query:

query {
  notes {
    id
    title
  }
}


Ejemplo de mutation:

mutation {
  createNote(input: {
    title: "Nota desde GraphQL",
    content: "Contenido creado desde GraphQL Studio"
  }) {
    id
    title
  }
}

🔐 Autenticación JWT

La API genera tokens JWT a partir de un login.
El token se debe enviar desde Axios:

axios.post(url, data, {
  headers: { Authorization: `Bearer ${token}` }
});

🎯 Conclusiones del Proyecto

Se integró exitosamente GraphQL, MongoDB, JWT, Docker y Axios.

La arquitectura permite escalabilidad y adaptación a microservicios.

El uso de Docker garantiza portabilidad y consistencia.

GraphQL permite un consumo eficiente desde el frontend.