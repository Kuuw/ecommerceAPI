﻿namespace Entities.DTO
{
    public class AddressDTO
    {
        public int? AddressId { get; set; }

        public int? UserId { get; set; }

        public int CountryId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public string Telephone { get; set; }
    }
}
