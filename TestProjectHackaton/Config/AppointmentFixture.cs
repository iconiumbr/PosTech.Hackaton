
using Bogus;
using Domain.Entities;
using TestProjectHackaton.Config;


namespace TestProject.Config
{
    [CollectionDefinition(nameof(PosTechFaseVCollectionAppointment))]
    public class PosTechFaseVCollectionAppointment : ICollectionFixture<AppointmentFixture> { }

    public class AppointmentFixture
    {
        private readonly Faker _faker = new Faker("pt_BR");

        public Appointment GenerateValidAppointment()
        {
            return new Faker<Appointment>()
                .CustomInstantiator(f =>
                    new Appointment(
                        contact: f.Phone.PhoneNumber("###########"),  // Número de contato válido
                        doctor: new DoctorFixture().GenerateValidDoctor(),
                        service: new DoctorServiceFixture().GenerateValidDoctorService(),
                        dateTime: f.Date.Future(),  // Data futura válida
                        status: Domain.Enums.AppointmentStatus.Scheduled
                    ));
        }

        public Appointment GenerateInvalidAppointmentContact()
        {
            return new Faker<Appointment>()
                .CustomInstantiator(f =>
                    new Appointment(
                        contact: "@#123",  // Contato inválido
                        doctor: new DoctorFixture().GenerateValidDoctor(),
                        service: new DoctorServiceFixture().GenerateValidDoctorService(),
                        dateTime: f.Date.Future(),
                        status: Domain.Enums.AppointmentStatus.Scheduled
                    ));
        }

        public Appointment GenerateInvalidAppointmentEmptyContact()
        {
            return new Faker<Appointment>()
                .CustomInstantiator(f =>
                    new Appointment(
                        contact: null,  // Contato vazio
                        doctor: new DoctorFixture().GenerateValidDoctor(),
                        service: new DoctorServiceFixture().GenerateValidDoctorService(),
                        dateTime: f.Date.Future(),
                        status: Domain.Enums.AppointmentStatus.Scheduled
                    ));
        }

        public Appointment GenerateInvalidAppointmentEmptyDoctor()
        {
            return new Faker<Appointment>()
                .CustomInstantiator(f =>
                    new Appointment(
                        contact: f.Phone.PhoneNumber("###########"),
                        doctor: null,  // Médico inválido
                        service: new DoctorServiceFixture().GenerateValidDoctorService(),
                        dateTime: f.Date.Future(),
                        status: Domain.Enums.AppointmentStatus.Scheduled
                    ));
        }

        public Appointment GenerateInvalidAppointmentEmptyService()
        {
            return new Faker<Appointment>()
                .CustomInstantiator(f =>
                    new Appointment(
                        contact: f.Phone.PhoneNumber("###########"),
                        doctor: new DoctorFixture().GenerateValidDoctor(),
                        service: null,  // Serviço inválido
                        dateTime: f.Date.Future(),
                        status: Domain.Enums.AppointmentStatus.Scheduled
                    ));
        }

        public Appointment GenerateInvalidAppointmentEmptyDate()
        {
            return new Faker<Appointment>()
                .CustomInstantiator(f =>
                    new Appointment(
                        contact: f.Phone.PhoneNumber("###########"),
                        doctor: new DoctorFixture().GenerateValidDoctor(),
                        service: new DoctorServiceFixture().GenerateValidDoctorService(),
                        dateTime: default,  // Data inválida
                        status: Domain.Enums.AppointmentStatus.Scheduled
                    ));
        }
    }
}
