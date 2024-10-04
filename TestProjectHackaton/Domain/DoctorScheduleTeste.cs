using Domain.Entities;
using TestProjectHackaton.Config;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace TestProjectHackaton.Domain
{

    [Collection(nameof(PosTechFaseVCollectionDoctorSchedule))]
    public class DoctorScheduleTeste
    {
        private readonly DoctorScheduleFixture _fixtures;
        public DoctorScheduleTeste(DoctorScheduleFixture fixtures) => _fixtures = fixtures;

        [Trait("Categoria", "DoctorSchedule Válido")]
        [Fact]
        public void Criar_DoctorSchedule_Valido()
        {
            // Arrange
            var doctorSchedule = _fixtures.DoctorScheduleValido();
            var validator = new DoctorSchedule.ValidatorDoctorScheduleValido();

            // Act
            var result = validator.TestValidate(doctorSchedule);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Trait("Categoria", "DoctorSchedule Inválido")]
        [Fact]
        public void Criar_DoctorSchedule_DoctorNulo_Invalido()
        {

            // Arrange
            var doctorSchedule = _fixtures.DoctorScheduleValido();
            doctorSchedule = new DoctorSchedule(doctorSchedule.DayOfWeek, 
                                                null, 
                                                doctorSchedule.MorningStartHour, 
                                                doctorSchedule.MorningEndHour, 
                                                doctorSchedule.AfternoonStartHour, 
                                                doctorSchedule.AfternoonEndHour);

            var validator = new DoctorSchedule.ValidatorDoctorScheduleValido();

            // Act
            var validationResult = validator.Validate(doctorSchedule);
            var isValid = validationResult.IsValid;

            // Assert
            isValid.Should().BeFalse("Agenda Médica não foi gerado corretamente");
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Necessário um Médico válido");

        }

        [Trait("Categoria", "DoctorSchedule Inválido")]
        [Fact]
        public void Criar_DoctorSchedule_AllTimesEmpty_Invalido()
        {
            // Arrange
            var doctorSchedule = _fixtures.Criar_DoctorSchedule_EmptyTimes_Valido();
            var validator = new DoctorSchedule.ValidatorDoctorScheduleValido();

            // Act
            var validationResult = validator.Validate(doctorSchedule);
            var isValid = validationResult.IsValid;

            // Assert
            isValid.Should().BeFalse("Agenda Médica não foi gerado corretamente");
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Todos os horários não podem ficar sem preenchimento.");

        }

        [Trait("Categoria", "DoctorSchedule Inválido")]
        [Fact]
        public void Criar_DoctorSchedule_MorningTimesIncomplet_Invalido()
        {
            // Arrange
            var doctorSchedule = _fixtures.Criar_DoctorSchedule_MorningTimesIncompletInvalid_Invalido();
            var validator = new DoctorSchedule.ValidatorDoctorScheduleValido();

            // Act
            var validationResult = validator.Validate(doctorSchedule);
            var isValid = validationResult.IsValid;

            // Assert
            isValid.Should().BeFalse("Agenda Médica não foi gerado corretamente");
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Ao menos dois horários devem estar preenchidos.");

        }


        [Trait("Categoria", "DoctorSchedule Inválido")]
        [Fact]
        public void Criar_DoctorSchedule_MorningTimesInvalid_Inavlido()
        {
            // Arrange
            var doctorSchedule = _fixtures.Criar_DoctorSchedule_MorningTimesInInvalid_Invalido();
            var validator = new DoctorSchedule.ValidatorDoctorScheduleValido();

            // Act
            var validationResult = validator.Validate(doctorSchedule);
            var isValid = validationResult.IsValid;

            // Assert
            isValid.Should().BeFalse("Agenda Médica não foi gerado corretamente");
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Se os horários de início e término da manhã estiverem preenchidos, o horário de início deve ser menor que o horário de término.");

        }



    }

}
