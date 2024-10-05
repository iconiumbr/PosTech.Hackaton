
using Bogus;
using Domain.Entities;

namespace TestProjectHackaton.Config
{

    [CollectionDefinition(nameof(PosTechFaseVCollectionDoctor))]
    public class PosTechFaseVCollectionDoctor : ICollectionFixture<DoctorFixture> { }

    public class DoctorFixture
    {
        private readonly Faker _faker = new Faker("pt_BR");

        public Doctor GenerateValidDoctor()
        {
            var faker = new Faker();

            return new Faker<Doctor>()
                 .CustomInstantiator(f =>
                     new Doctor(
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
                 );
        }

        public Doctor GenerateInvalidDoctorName()
        {
            var faker = new Faker();

            return new Faker<Doctor>()
                 .CustomInstantiator(f =>
                     new Doctor(
                             /*Name*/ "@#123",
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
                 );
        }

        public Doctor GenerateInvalidEmptName()
        {
            var faker = new Faker();

            return new Faker<Doctor>()
                 .CustomInstantiator(f =>
                     new Doctor(
                             /*Name*/ null,
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
                 );
        }

        public Doctor GenerateInvalidEmptyContractNumber()
        {
            var faker = new Faker();

            return new Faker<Doctor>()
                 .CustomInstantiator(f =>
                     new Doctor(
                             /*Name*/ "José das Couves",
                             /*ContactNumber*/ null,
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
                 );
        }

        public Doctor GenerateInvalidDoctorCrm()
        {
            var faker = new Faker();

            return new Faker<Doctor>()
                 .CustomInstantiator(f =>
                     new Doctor(
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
                             /*Crm*/ "@#ãã~-_"
                         )
                 );
        }

        public Doctor GenerateInvalidDoctorEmptyCrm()
        {
            var faker = new Faker();

            return new Faker<Doctor>()
                 .CustomInstantiator(f =>
                     new Doctor(
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
                             /*Crm*/ null
                         )
                 );
        }

        public Doctor GenerateInvalidDoctorContactNumber()
        {
            var faker = new Faker();

            return new Faker<Doctor>()
                 .CustomInstantiator(f =>
                     new Doctor(
                             /*Name*/ "Thomas Turbando",
                             /*ContactNumber*/ "!@#$%",
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
                 );
        }

        public Doctor GenerateInvalidDoctorEmail()
        {
            var faker = new Faker();

            return new Faker<Doctor>()
                 .CustomInstantiator(f =>
                     new Doctor(
                             /*Name*/ "Thomas Turbando",
                             /*ContactNumber*/ "1234567890",
                             /*Email*/ "teste email inválido",
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
                 );
        }

        public Doctor GenerateInvalidDoctorCpf()
        {
            var faker = new Faker();

            return new Faker<Doctor>()
                             .CustomInstantiator(f =>
                                 new Doctor(
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
                                         /*Cpf*/ "123456", // CPF válido
                                         /*Crm*/ "CRM123456"
                                     )
                             );
        }

        public Doctor GenerateInvalidDoctorAddress()
        {
            return new Doctor(
                "Dr. John Doe",                // Nome
                "123456",                      // Número de contato
                "doctor@example.com",          // Email
                "",                            // Rua inválida
                "",                            // Número inválido
                "",                            // Complemento inválido
                "123",                         // CEP inválido
                "",                            // Bairro inválido
                "",                            // Cidade inválida
                "",                            // Estado inválido
                "12345678909",                 // CPF válido
                "123456"                       // CRM
            );
        }
    }
}
