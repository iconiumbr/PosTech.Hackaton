
using Bogus;
using Domain.Entities;

namespace TestProjectHackaton.Config
{
    [CollectionDefinition(nameof(PosTechFaseVCollectionDoctorService))]
    public class PosTechFaseVCollectionDoctorService : ICollectionFixture<DoctorServiceFixture> { }

    public class DoctorServiceFixture
    {
        private readonly Faker _faker = new Faker("pt_BR");

        public DoctorService GenerateValidDoctorService()
        {
            return new Faker<DoctorService>()
                .CustomInstantiator(f =>
                    new DoctorService(
                        /*Name*/ "Teste de Servico valido",
                        /*Description*/ "Teste descricao Válida",
                        /*Duration*/ f.Date.Timespan(),
                        /*Price*/ f.Random.Decimal(1, 1000)
                    )
                );
        }

        public DoctorService GenerateInvalidDoctorServiceName()
        {
            return new Faker<DoctorService>()
                .CustomInstantiator(f =>
                    new DoctorService(
                        /*Name*/ "@!#123", // Nome inválido
                        /*Description*/ f.Lorem.Sentence(),
                        /*Duration*/ f.Date.Timespan(),
                        /*Price*/ f.Random.Decimal(1, 1000)
                    )
                );
        }

        public DoctorService GenerateInvalidEmptyDoctorServiceName()
        {
            return new Faker<DoctorService>()
                .CustomInstantiator(f =>
                    new DoctorService(
                        /*Name*/ null, // Nome vazio
                        /*Description*/ f.Lorem.Sentence(),
                        /*Duration*/ f.Date.Timespan(),
                        /*Price*/ f.Random.Decimal(1, 1000)
                    )
                );
        }

        public DoctorService GenerateInvalidDoctorServiceDescription()
        {
            return new Faker<DoctorService>()
                .CustomInstantiator(f =>
                    new DoctorService(
                        /*Name*/ f.Commerce.ProductName(),
                        /*Description*/ "Descrição muito longa".PadRight(300, 'a'), // Descrição maior que 255 caracteres
                        /*Duration*/ f.Date.Timespan(),
                        /*Price*/ f.Random.Decimal(1, 1000)
                    )
                );
        }

        public DoctorService GenerateInvalidDoctorServicePrice()
        {
            return new Faker<DoctorService>()
                .CustomInstantiator(f =>
                    new DoctorService(
                        /*Name*/ f.Commerce.ProductName(),
                        /*Description*/ f.Lorem.Sentence(),
                        /*Duration*/ f.Date.Timespan(),
                        /*Price*/ -5 // Preço inválido
                    )
                );
        }

        public DoctorService GenerateEmptyDoctorServicePrice()
        {
            return new Faker<DoctorService>()
                .CustomInstantiator(f =>
                    new DoctorService(
                        /*Name*/ f.Commerce.ProductName(),
                        /*Description*/ f.Lorem.Sentence(),
                        /*Duration*/ f.Date.Timespan(),
                        /*Price*/ 0 // Preço igual a zero
                    )
                );
        }

        public DoctorService GenerateInvalidDoctorServiceDuration()
        {
            return new Faker<DoctorService>()
                .CustomInstantiator(f =>
                    new DoctorService(
                        /*Name*/ f.Commerce.ProductName(),
                        /*Description*/ f.Lorem.Sentence(),
                        /*Duration*/ TimeSpan.Zero, // Duração inválida
                        /*Price*/ f.Random.Decimal(1, 1000)
                    )
                );
        }
    }
}
