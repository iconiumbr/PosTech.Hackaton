using Bogus;
using Domain.Entities;

namespace TestProjectHackaton.Config
{

    [CollectionDefinition(nameof(PosTechFaseVCollectionHoliday))]
    public class PosTechFaseVCollectionHoliday : ICollectionFixture<HolidayFixture> { }

    public class HolidayFixture
    {
        private readonly Faker _faker = new Faker("pt_BR");

        public Holiday Gerar_Holiday_Valido()
        {
            return new Faker<Holiday>()
                 .CustomInstantiator(f =>
                     new Holiday(
                         /*Description*/ "Teste de descrição Válido",
                         /*Date*/ f.Date.Future(),
                         /*Doctor*/ new Doctor(
                             /*Name*/ "Thomas Turbando",
                             /*ContactNumber*/ "1234567890",
                             /*Email*/ f.Internet.Email(),
                             /*Street*/ f.Address.StreetName(),
                             /*Number*/ f.Address.BuildingNumber(),
                             /*Complement*/ f.Address.SecondaryAddress(),
                             /*ZipCode*/ "20271150",
                             /*Neighborhood*/ f.Address.City(),
                             /*City*/ f.Address.City(),
                             /*State*/ f.Address.StateAbbr(),
                             /*Cpf*/ "09297575722", // CPF válido
                             /*Crm*/ "CRM123456"
                         )
                     )
                 );
        }

        public Holiday Gerar_Holiday_DescricaoEmpty_Invalido()
        {
            return new Faker<Holiday>()
                .CustomInstantiator(f =>
                    new Holiday(
                        /*Description*/ null,  // Descrição vazia
                        /*Date*/ f.Date.Future(),
                        /*Doctor*/ new Doctor(
                            "Thomas Turbando",
                            "1234567890",
                            f.Internet.Email(),
                            f.Address.StreetName(),
                            f.Address.BuildingNumber(),
                            f.Address.SecondaryAddress(),
                            f.Address.ZipCode(),
                            f.Address.City(),
                            f.Address.City(),
                            f.Address.StateAbbr(),
                            "09297575722",
                            "CRM123456"
                        )
                    )
                );
        }

        public Holiday Gerar_Holiday_DateEmpty_Invalido()
        {
            return new Faker<Holiday>()
                .CustomInstantiator(f =>
                    new Holiday(
                        /*Description*/ f.Lorem.Sentence(3),
                        /*Date       */ default(DateTime), // Data vazia
                         /*Doctor*/ new Doctor(
                            "Thomas Turbando",
                            "1234567890",
                            f.Internet.Email(),
                            f.Address.StreetName(),
                            f.Address.BuildingNumber(),
                            f.Address.SecondaryAddress(),
                            f.Address.ZipCode(),
                            f.Address.City(),
                            f.Address.City(),
                            f.Address.StateAbbr(),
                            "09297575722",
                            "CRM123456"
                        )
                    ));
        }
        

        public Holiday Gerar_Holiday_DescriptionMaxCaracteres_Invalido()
        {
            return new Faker<Holiday>()
                .CustomInstantiator(f =>
                    new Holiday(
                        /*Description*/ f.Lorem.Sentence(101), // Descrição com mais de 100 caracteres
                        /*Date       */ f.Date.Future(),
                         /*Doctor*/ new Doctor(
                            "Thomas Turbando",
                            "1234567890",
                            f.Internet.Email(),
                            f.Address.StreetName(),
                            f.Address.BuildingNumber(),
                            f.Address.SecondaryAddress(),
                            f.Address.ZipCode(),
                            f.Address.City(),
                            f.Address.City(),
                            f.Address.StateAbbr(),
                            "09297575722",
                            "CRM123456"
                        )
                    ));
        }
    }
}
