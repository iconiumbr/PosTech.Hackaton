
using Domain.Entities;
using FluentAssertions;
using FluentValidation.TestHelper;
using TestProjectHackaton.Config;
using Xunit;

namespace TestProjectHackaton.Domain
{
    [Collection(nameof(PosTechFaseVCollectionDoctorService))]
    public class DoctorServiceTest
    {
        private readonly DoctorServiceFixture _fixture;

        public DoctorServiceTest(DoctorServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Criar_DoctorService_Valido")]
        [Trait("Category", "DoctorService Validations")]
        public void Criar_DoctorService_Valido()
        {
            // Arrange
            var doctorService = _fixture.GenerateValidDoctorService();
            var validator = new DoctorService.ValidatorDoctorServiceValido();

            // Act
            var result = validator.TestValidate(doctorService);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact(DisplayName = "Criar_DoctorService_InvalidName_Invalido")]
        [Trait("Category", "DoctorService Inválido")]
        public void DoctorService_InvalidName_ShouldReturnError()
        {
            // Arrange
            var doctorService = _fixture.GenerateInvalidDoctorServiceName();
            var validator = new DoctorService.ValidatorDoctorServiceValido();

            // Act
            var result = validator.TestValidate(doctorService);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Nome deve conter apenas caracteres alfanuméricos e espaços.");
        }

        [Fact(DisplayName = "Criar_DoctorService_EmptyName_Invalido")]
        [Trait("Category", "DoctorService Inválido")]
        public void DoctorService_EmptyName_ShouldReturnError()
        {
            // Arrange
            var doctorService = _fixture.GenerateInvalidEmptyDoctorServiceName();
            var validator = new DoctorService.ValidatorDoctorServiceValido();

            // Act
            var result = validator.TestValidate(doctorService);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Nome é obrigatório.");
        }

        [Fact(DisplayName = "Criar_DoctorService_InvalidDescription_Invalido")]
        [Trait("Category", "DoctorService Inválido")]
        public void DoctorService_InvalidDescription_ShouldReturnError()
        {
            // Arrange
            var doctorService = _fixture.GenerateInvalidDoctorServiceDescription();
            var validator = new DoctorService.ValidatorDoctorServiceValido();

            // Act
            var result = validator.TestValidate(doctorService);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("Descrição deve ter no máximo 255 caracteres.");
        }

        [Fact(DisplayName = "Criar_DoctorService_InvalidPrice_Invalido")]
        [Trait("Category", "DoctorService Inválido")]
        public void DoctorService_InvalidPrice_ShouldReturnError()
        {
            // Arrange
            var doctorService = _fixture.GenerateInvalidDoctorServicePrice();
            var validator = new DoctorService.ValidatorDoctorServiceValido();

            // Act
            var result = validator.TestValidate(doctorService);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Price)
                .WithErrorMessage("O preço não pode ser menor que zero.");
        }

        [Fact(DisplayName = "Criar_DoctorService_EmptyPrice_Invalido")]
        [Trait("Category", "DoctorService Inválido")]
        public void DoctorService_EmptyPrice_ShouldReturnError()
        {
            // Arrange
            var doctorService = _fixture.GenerateEmptyDoctorServicePrice();
            var validator = new DoctorService.ValidatorDoctorServiceValido();

            // Act
            var result = validator.TestValidate(doctorService);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Price)
                .WithErrorMessage("Preço é obrigatório.");
        }

        [Fact(DisplayName = "Criar_DoctorService_InvalidDuration_Invalido")]
        [Trait("Category", "DoctorService Inválido")]
        public void DoctorService_InvalidDuration_ShouldReturnError()
        {
            // Arrange
            var doctorService = _fixture.GenerateInvalidDoctorServiceDuration();
            var validator = new DoctorService.ValidatorDoctorServiceValido();

            // Act
            var result = validator.TestValidate(doctorService);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Duration)
                .WithErrorMessage("Duração é obrigatório.");
        }
    }
}

