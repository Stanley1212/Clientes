# OrionTek Client Manager - Prueba Técnica Fullstack

Solución fullstack para gestionar clientes con direcciones usando **ASP.NET Core Web API** y **React + Vite**.

---

## Estructura

```
/OrionTek.API        → Backend (ASP.NET Core 8 Web API)
/oriontek-client    → Frontend (React + Vite + TypeScript)
```

---

## 🟦 Backend

### Requisitos

- .NET 8 SDK
- SQLite (incluido en .NET)

### Tecnologías

- **CQRS** con MediatR
- **Entity Framework Core** con SQLite
- **FluentValidation** para validación
- **AutoMapper** para mapeo de DTOs
- **Swagger/OpenAPI**


```bash
cd OrionTek.API
dotnet restore
dotnet run
```

El API estará disponible en: `http://localhost:5000`

Swagger: `http://localhost:5000/swagger`

### Endpoints

| Método | Ruta | Descripción |
|-------|-----|-------------|
| GET | `/api/clients` | Listar todos los clientes |
| GET | `/api/clients/{id}` | Obtener cliente por ID |
| POST | `/api/clients` | Crear cliente |
| PUT | `/api/clients/{id}` | Actualizar cliente |
| DELETE | `/api/clients/{id}` | Eliminar cliente |
| GET | `/api/addresses/client/{clientId}` | Listar direcciones |
| POST | `/api/addresses/client/{clientId}` | Agregar dirección |
| PUT | `/api/addresses/{id}` | Actualizar dirección |
| DELETE | `/api/addresses/{id}` | Eliminar dirección |

---

## 🟩 Frontend

### Requisitos

- Node.js 18+
- npm

### Tecnologías

- React 18 + Vite + TypeScript
- **react-router-dom** v6
- **axios**
- **react-hook-form** + **zod**
- **@tanstack/react-query**
- **Tailwind CSS**

### 安装 y ejecutar

```bash
cd oriontek-client
npm install
npm run dev
```

La app estará disponible en: `http://localhost:5173`

### Variables de entorno

Crea un archivo `.env`:

```
VITE_API_URL=http://localhost:54788
```

---

## 🏃‍♂️ Ejecutar ambos proyectos

1. Inicia el backend:
   ```bash
   cd OrionTek.API && dotnet run
   ```

2. En otra terminal, inicia el frontend:
   ```bash
   cd oriontek-client && npm run dev
   ```

3. Abre el navegador en `http://localhost:5173`

---

## 📁 Entregables

1. Carpeta `/OrionTek.API` - Backend
2. Carpeta `/oriontek-client` - Frontend
3. Este README.md