using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
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
            var customerInfo = new CustomerInformation
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                City = request.City,
                Address1 = request.Address1,
                Address2 = request.Address2,
                PostCode = request.PostCode,
                PhoneNo = request.PhoneNo
            };
            var value = JsonConvert.SerializeObject(customerInfo);
            
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
