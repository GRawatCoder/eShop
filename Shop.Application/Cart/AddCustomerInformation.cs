using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Application.Cart
{
    public class AddCustomerInformation
    {
        private ISession _session;
        public AddCustomerInformation(ISession session)
        {
            _session = session;
        }              

        public void Do(Request request)
        {                                 
            var value = JsonConvert.SerializeObject(request);
            _session.SetString("customer-info", value);
        }

        public class Request
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNo { get; set; }
            [Required]
            public string Address1 { get; set; }           
            public string Address2 { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string PostCode { get; set; }
        }
    }
}
