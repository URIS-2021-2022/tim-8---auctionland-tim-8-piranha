using AutoMapper;
using PaymentMicroservice.Entities;
using PaymentMicroservice.Models.Payment;


namespace PaymentMicroservice.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentCreationDto, Payment>();
            CreateMap<Payment, PaymentConfirmation>();
            CreateMap<PaymentConfirmation, PaymentConfirmationDto>();
            CreateMap<PaymentUpdateDto, Payment>();
            CreateMap<Payment, Payment>();

        }
    }
}
