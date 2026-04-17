# Calculadora de Cuotas - Prueba Técnica

Proyecto desarrollado en **ASP.NET Core** con arquitectura por capas para calcular la cuota de un préstamo según la edad del solicitante, el monto solicitado y la cantidad de meses.

## Descripción general

La aplicación permite ingresar:

- Fecha de nacimiento
- Monto del préstamo
- Meses del préstamo

Con esos datos, el sistema:

- Calcula la edad del solicitante
- Obtiene la tasa correspondiente según la edad
- Valida que el plazo ingresado sea permitido
- Calcula la cuota mensual
- Registra la consulta en una tabla log con la IP de la solicitud

## Reglas de negocio

### Tasas por edad

| Edad | Tasa |
|------|------|
| 18 | 1.20 |
| 19 | 1.18 |
| 20 | 1.16 |
| 21 | 1.14 |
| 22 | 1.12 |
| 23 | 1.10 |
| 24 | 1.08 |
| 25 | 1.05 |

### Meses permitidos

- 3
- 6
- 9
- 12

### Fórmula aplicada

```text
Cuota = (Monto * Tasa) / Meses
