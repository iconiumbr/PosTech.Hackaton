using Domain.Entities;
using TestProjectHackaton.Config;
using FluentAssertions;


namespace TestProjectHackaton.Domain
{

    [Collection(nameof(PosTechFaseVCollectionDoctor))]
    public class DoctorTeste
    {
        private readonly DoctorFixture _fixtures;
        public DoctorTeste(DoctorFixture fixtures) => _fixtures = fixtures;

        [Trait("Categoria", "Doctor Válido")]
        [Fact]
        public void Criar_Doctor_Valido()
        {
            // Arrange
            var doctor = _fixtures.GenerateValidDoctor();
            var validator = new Doctor.ValidatorDoctorValido();

            // Act
            var result = validator.Validate(doctor);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Trait("Categoria", "Doctor Inválido")]
        [Fact]
        public void Criar_Doctor_NAmeEmpty_Invalido()
        {
            // Arrange
            var doctor = _fixtures.GenerateInvalidEmptName();
            var validator = new Doctor.ValidatorDoctorValido();

            // Act
            var result = validator.Validate(doctor);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Nome do Médico é obrigatório.");
        }

        [Trait("Categoria", "Doctor Inválido")]
        [Fact]
        public void Criar_Doctor_CrmInvalid_Invalido()
        {
            // Arrange
            var doctor = _fixtures.GenerateInvalidDoctorCrm();
            var validator = new Doctor.ValidatorDoctorValido();

            // Act
            var result = validator.Validate(doctor);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Crm deve conter apenas caracteres alfanuméricos e espaços.");
        }

        [Trait("Categoria", "Doctor Inválido")]
        [Fact]
        public void Criar_Doctor_CrmEmptt_Invalido()
        {
            // Arrange
            var doctor = _fixtures.GenerateInvalidDoctorEmptyCrm();
            var validator = new Doctor.ValidatorDoctorValido();

            // Act
            var result = validator.Validate(doctor);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Crm do Médico é obrigatório.");
        }

        [Trait("Categoria", "Doctor Inválido")]
        [Fact]
        public void Criar_Doctor_ContractNumberEmpty_Invalido()
        {
            // Arrange
            var doctor = _fixtures.GenerateInvalidEmptyContractNumber();
            var validator = new Doctor.ValidatorDoctorValido();

            // Act
            var result = validator.Validate(doctor);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Numero do Contrato do Médico é obrigatório.");
        }

        [Trait("Categoria", "Doctor Inválido")]
        [Fact]
        public void Criar_Doctor_InvalidEmail_Invalido()
        {
            // Arrange
            var doctor = _fixtures.GenerateInvalidDoctorEmail();
            var validator = new Doctor.ValidatorDoctorValido();

            // Act
            var result = validator.Validate(doctor);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Email em formato inválido");
        }

        [Trait("Categoria", "Doctor Inválido")]
        [Fact]
        public void Criar_Doctor_InvalidCpf_Invalido()
        {
            // Arrange
            var doctor = _fixtures.GenerateInvalidDoctorCpf();
            var validator = new Doctor.ValidatorDoctorValido();

            // Act
            var result = validator.Validate(doctor);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Cpf inválido!");
        }
        
        [Trait("Categoria", "Doctor Inválido")]
        [Fact]
        public void Criar_Doctor_InvalidAddress_Invalido()
        {
            // Arrange
            var doctor = _fixtures.GenerateInvalidDoctorAddress();
            var validator = new Doctor.ValidatorDoctorValido();

            // Act
            var result = validator.Validate(doctor);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Rua é obrigatório.");
        }


    }
}
