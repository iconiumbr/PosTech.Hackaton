    
using Domain.Entities;
using FluentAssertions;
using FluentValidation.TestHelper;
using TestProject.Config;
using Xunit;

namespace TestProject.Tests
{
    [Collection(nameof(PosTechFaseVCollectionAppointment))]
    public class AppointmentTeste
    {
        private readonly AppointmentFixture _fixture;

        public AppointmentTeste(AppointmentFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Criar_Appointment_Valido")]
        public void Appointment_Valid()
        {
            // Arrange
            var appointment = _fixture.GenerateValidAppointment();
            var validator = new Appointment.ValidatorAppointmentValido();

            // Act
            var result = validator.TestValidate(appointment);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Criar_AppointmentInvalidContact_Invalido")]
        public void Appointment_InvalidContact()
        {
            // Arrange
            var appointment = _fixture.GenerateInvalidAppointmentContact();
            var validator = new Appointment.ValidatorAppointmentValido();

            // Act
            var result = validator.TestValidate(appointment);

            // Assert
            result.ShouldHaveValidationErrorFor(a => a.Contact)
                .WithErrorMessage("Nome deve conter apenas caracteres alfanuméricos e espaços.");
        }

        [Fact(DisplayName = "Criar_AppointmentContactEmpty_Invalido")]
        public void Appointment_EmptyContact()
        {
            // Arrange
            var appointment = _fixture.GenerateInvalidAppointmentEmptyContact();
            var validator = new Appointment.ValidatorAppointmentValido();

            // Act
            var result = validator.TestValidate(appointment);

            // Assert
            result.ShouldHaveValidationErrorFor(a => a.Contact)
                .WithErrorMessage("Contato é obrigatório.");
        }

        [Fact(DisplayName = "Criar_AppointmentDoctorEmpty_Invalid")]
        public void Appointment_EmptyDoctor()
        {
            // Arrange
            var appointment = _fixture.GenerateInvalidAppointmentEmptyDoctor();
            var validator = new Appointment.ValidatorAppointmentValido();

            // Act
            var result = validator.TestValidate(appointment);

            // Assert
            result.ShouldHaveValidationErrorFor(a => a.Doctor)
                .WithErrorMessage("Necessário um Médico válido");
        }

        [Fact(DisplayName = "Criar_AppointmentServiceEmpty_Invalido ")]
        public void Appointment_EmptyService()
        {
            // Arrange
            var appointment = _fixture.GenerateInvalidAppointmentEmptyService();
            var validator = new Appointment.ValidatorAppointmentValido();

            // Act
            var result = validator.TestValidate(appointment);

            // Assert
            result.ShouldHaveValidationErrorFor(a => a.Service)
                .WithErrorMessage("Necessário Serviço Médico válido");
        }

        [Fact(DisplayName = "Criar_AppointmentEmptyDateTime_Invalido")]
        public void Appointment_EmptyDate()
        {
            // Arrange
            var appointment = _fixture.GenerateInvalidAppointmentEmptyDate();
            var validator = new Appointment.ValidatorAppointmentValido();

            // Act
            var result = validator.TestValidate(appointment);

            // Assert
            result.ShouldHaveValidationErrorFor(a => a.DateTime)
                .WithErrorMessage("Data é obrigatório.");
        }
    }
}
