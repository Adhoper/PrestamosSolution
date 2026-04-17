using Prestamos.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prestamos.Business.Interfaces
{
    public interface IPrestamoService
    {
        Task<CalcularPrestamoResponseDto> CalcularPrestamoAsync(
            CalcularPrestamoRequestDto request,
            string? ipConsulta);
    }
}
