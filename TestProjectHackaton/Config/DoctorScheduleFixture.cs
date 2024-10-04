    
using Bogus;
using Domain.Entities;

namespace TestProjectHackaton.Config
{

    [CollectionDefinition(nameof(PosTechFaseVCollectionDoctorSchedule))]
    public class PosTechFaseVCollectionDoctorSchedule : ICollectionFixture<DoctorScheduleFixture> { }

    public class DoctorScheduleFixture
    {
        private readonly Faker _faker = new Faker("pt_BR");

        public DoctorSchedule DoctorScheduleValido()
        {
            var doctorFixture = new DoctorFixture();
            var doctor = doctorFixture.GenerateValidDoctor();

            return new Faker<DoctorSchedule>("pt_BR")
                .CustomInstantiator(f => new DoctorSchedule(
                    f.PickRandom<DayOfWeek>(),         // Dia da semana aleatório
                    doctor,                            // Doctor válido
                    new TimeSpan(8, 0, 0),             // Horário de início da manhã: 08:00
                    new TimeSpan(12, 0, 0),            // Horário de término da manhã: 12:00
                    new TimeSpan(13, 0, 0),            // Horário de início da tarde: 13:00
                    new TimeSpan(18, 0, 0)))           // Horário de término da tarde: 18:00
                .Generate();
        }

        public DoctorSchedule Criar_DoctorSchedule_EmptyTimes_Valido()
        {
            var doctorFixture = new DoctorFixture();
            var doctor = doctorFixture.GenerateValidDoctor();

            return new Faker<DoctorSchedule>("pt_BR")
                .CustomInstantiator(f => new DoctorSchedule(
                    f.PickRandom<DayOfWeek>(),         // Dia da semana aleatório
                    doctor,                            // Doctor válido
                    TimeSpan.Zero,                     // Horário Zero
                    TimeSpan.Zero,                     // Horário Zero
                    TimeSpan.Zero,                     // Horário Zero
                    TimeSpan.Zero                      // Horário Zero
                ))           // Horário Zero
                .Generate();
        }

        public DoctorSchedule Criar_DoctorSchedule_MorningTimesIncompletInvalid_Invalido()
        {
            var doctorFixture = new DoctorFixture();
            var doctor = doctorFixture.GenerateValidDoctor();

            return new Faker<DoctorSchedule>("pt_BR")
                .CustomInstantiator(f => new DoctorSchedule(
                    f.PickRandom<DayOfWeek>(),         // Dia da semana aleatório
                    doctor,                            // Doctor válido
                    new TimeSpan(8, 0, 0),             // Horário de início da manhã: 08:00
                    TimeSpan.Zero,                     // Horário de término da manhã: 12:00
                    TimeSpan.Zero,                     // Horário Zero
                    TimeSpan.Zero                      // Horário Zero
                ))           // Horário Zero
                .Generate();
        }

        public DoctorSchedule Criar_DoctorSchedule_MorningTimesInInvalid_Invalido()
        {
            var doctorFixture = new DoctorFixture();
            var doctor = doctorFixture.GenerateValidDoctor();

            return new Faker<DoctorSchedule>("pt_BR")
                .CustomInstantiator(f => new DoctorSchedule(
                    f.PickRandom<DayOfWeek>(),         // Dia da semana aleatório
                    doctor,                            // Doctor válido
                    new TimeSpan(12, 0, 0),             // Horário de início da manhã: 08:00
                    new TimeSpan(8, 0, 0),            // Horário de término da manhã: 12:00
                    TimeSpan.Zero,                     // Horário Zero
                    TimeSpan.Zero                      // Horário Zero
                    ))           // Horário de término da tarde: 18:00
                .Generate();
        }

        public DoctorSchedule Criar_DoctorSchedule_AfternoonTimesIncompletInvalid_Inalido()
        {
            var doctorFixture = new DoctorFixture();
            var doctor = doctorFixture.GenerateValidDoctor();

            return new Faker<DoctorSchedule>("pt_BR")
                .CustomInstantiator(f => new DoctorSchedule(
                    f.PickRandom<DayOfWeek>(),         
                    doctor,                            
                    TimeSpan.Zero,                     
                    TimeSpan.Zero,                     
                    new TimeSpan(13, 0, 0),
                    TimeSpan.Zero
                ))           
                .Generate();
        }

        public DoctorSchedule Criar_DoctorSchedule_AfternoonTimesInInvalid_Inalido()
        {
            var doctorFixture = new DoctorFixture();
            var doctor = doctorFixture.GenerateValidDoctor();

            return new Faker<DoctorSchedule>("pt_BR")
                .CustomInstantiator(f => new DoctorSchedule(
                    f.PickRandom<DayOfWeek>(),
                    doctor,
                    TimeSpan.Zero,
                    TimeSpan.Zero,
                    new TimeSpan(16, 0, 0),
                    new TimeSpan(13, 0, 0)
                ))
                .Generate();
        }

    }
}
