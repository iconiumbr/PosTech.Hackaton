using Application.Notifications;
using Application.Queries;
using Application.UseCases.Appointments.CreateAppointment;
using Application.UseCases.Doctors.AddService;
using Application.UseCases.Doctors.CreateHoliday;
using Application.UseCases.Doctors.DoctorRegistration;
using Application.UseCases.Doctors.InformPeriodService;
using Application.UseCases.Doctors.UpdateService;
using Application.UseCases.Users.AddUser;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Validators
            services.AddScoped<IValidator<CreateHolidayRequest>, CreateHolidayValidator>();
            services.AddScoped<IValidator<InformPeriodServiceRequest>, InformPeriodServiceValidator>();
            services.AddScoped<IValidator<AddServiceRequest>, AddServiceValidator>();
            services.AddScoped<IValidator<UpdateServiceRequest>, UpdateServiceValidator>();
            services.AddScoped<IValidator<CreateAppointmentRequest>, CreateAppointmentValidator>();
            services.AddScoped<IValidator<DoctorRegistrationRequest>, DoctorRegistrationValidator>();
            services.AddScoped<IValidator<AddUserRequest>, AddUserValidator>();

            //Queries
            services.AddScoped<IDoctorQueries, DoctorQueries>();
            services.AddScoped<IHolidayQueries, HolidayQueries>();
            services.AddScoped<IAvailabilityQueries, AvailabilityQueries>();
            services.AddScoped<IAppointmentsQueries, AppointmentsQueries>();


            //Notificator
            services.AddScoped<INotificator, Notificator>();

            //Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;

            //Email


        }
    }
}
