namespace ESE.Order.API.Application.DTO
{
    public class AddressDTO
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string CodePostal { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}