
using Domain.Entities;
using TestProjectHackaton.Config;
using FluentAssertions;


namespace TestProjectHackaton.Domain
{

    [Collection(nameof(PosTechFaseVCollectionHoliday))]
    public class HolidayTeste
    {
        private readonly HolidayFixture _fixtures;
        public HolidayTeste(HolidayFixture fixtures) => _fixtures = fixtures;

        [Trait("Categoria", "Holiday Válido")]
        [Fact]
        public void Criar_Holiday_Valido()
        {
            // Arrange
            var holiday = _fixtures.Gerar_Holiday_Valido();
            var validator = new Holiday.ValidatorHolidayValido();

            // Act
            var validationResult = validator.Validate(holiday);
            var isValid = validationResult.IsValid;

            // Assert
            isValid.Should().BeTrue("Holiday foi gerado com dados válidos");
            validationResult.Errors.Should().BeEmpty("não deve haver erros de validação para um holiday válido");
        }

        [Trait("Categoria", "Holiday Inválido")]
        [Fact]
        public void Criar_Holiday_DescriptionEmpty_Invalido()
        {
            // Arrange
            var holiday = _fixtures.Gerar_Holiday_DescricaoEmpty_Invalido();
            var validator = new Holiday.ValidatorHolidayValido();

            // Act
            var validationResult = validator.Validate(holiday);
            var isValid = validationResult.IsValid;

            // Assert
            isValid.Should().BeFalse("Holiday não foi gerado corretamente");
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Descrição é obrigatório.");
        }

        [Trait("Categoria", "Holiday Inválido")]
        [Fact]
        public void Criar_Holiday_DateEmpty_Invalido()
        {
            // Arrange
            var holiday = _fixtures.Gerar_Holiday_DateEmpty_Invalido();
            var validator = new Holiday.ValidatorHolidayValido();

            // Act
            var validationResult = validator.Validate(holiday);
            var isValid = validationResult.IsValid;

            // Assert
            isValid.Should().BeFalse("Holiday não foi gerado corretamente");
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "O campo Data precisa ser fornecido");
        }

        [Trait("Categoria", "Holiday Inválido")]
        [Fact]
        public void Criar_Holiday_DescriptionMaxCaracteres_Invalido()
        {
            // Arrange
            var holiday = _fixtures.Gerar_Holiday_DescriptionMaxCaracteres_Invalido();
            var validator = new Holiday.ValidatorHolidayValido();

            // Act
            var validationResult = validator.Validate(holiday);
            var isValid = validationResult.IsValid;

            // Assert
            isValid.Should().BeFalse("Holiday não foi gerado corretamente");
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Descrição deve ter no máximo 100 caracteres.");
        }
    }
}
