using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs.Extensions;

public static class AppointmentsExtensions
{
    public static Appointment ToDto(this AppointmentDbModel model)
    {
        return new Appointment
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AppointmentDbModel ToModel(
        this AppointmentUpdateInput updateDto,
        AppointmentWhereUniqueInput uniqueId
    )
    {
        var appointment = new AppointmentDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            appointment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            appointment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return appointment;
    }
}
