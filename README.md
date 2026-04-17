# Calculadora de Cuotas - Prueba Técnica

Aplicación desarrollada en ASP.NET Core que calcula la cuota mensual de un préstamo según la edad, el monto y el plazo.

---

## 🌐 Demo en línea

Puedes probar la aplicación directamente aquí:

👉 http://prestamosadhoper.somee.com

---

## 📸 Vista de la aplicación

![Calculadora](./docs/CalculadoraCuotas.png)

---

## 🚀 Tecnologías utilizadas

- ASP.NET Core MVC
- ASP.NET Core Web API
- SQL Server
- Dapper
- Bootstrap
- Stored Procedures

---

## 🧠 Arquitectura

El proyecto está dividido en capas:

- Prestamos.Web → Interfaz (MVC)
- Prestamos.Api → API REST (hosteada en Azure)
- Prestamos.Business → Lógica de negocio
- Prestamos.Data → Acceso a datos
- Prestamos.Entities → Modelos y DTOs

---

## ⚙️ Funcionalidad

Permite ingresar:

- Fecha de nacimiento
- Monto del préstamo
- Meses (3, 6, 9, 12)

Y calcula:

Cuota = (Monto * Tasa) / Meses

También:
- Valida edad (18–25)
- Determina la tasa según rango de edad
- Registra cada consulta en la base de datos

---

## 🗄️ Base de datos

Ejecutar el archivo:

database.sql

Esto crea:
- Tablas
- Datos iniciales
- Stored Procedures

---

## ▶️ Ejecución local

1. Clonar el repositorio:

git clone https://github.com/Adhoper/PrestamosSolution.git

2. Abrir la solución en Visual Studio

3. Ejecutar el script `database.sql` en SQL Server

4. Configurar la cadena de conexión en:

Prestamos.Api/appsettings.json

5. Ejecutar los proyectos:

- Prestamos.Api
- Prestamos.Web

---

## ☁️ Despliegue

- API desplegada en Azure App Service
- Web desplegada en Somee
- Base de datos en SQL Server (Somee)

---

## 👨‍💻 Autor

Adrian Curet
