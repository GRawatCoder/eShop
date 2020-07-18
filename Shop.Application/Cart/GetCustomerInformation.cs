using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Application.Cart
{
    public class GetCustomerInformation
    {
        private ISession _session;
        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }

        public Request Do()
        {
            var session = _session.GetString("customer-info");
            if (string.IsNullOrEmpty(session)) return null;
            var customerInfo = JsonConvert.DeserializeObject<Request>(session);
            return customerInfo;
        }

        public class Request
        {
            
            public string FirstName { get; set; }
            
            public string LastName { get; set; }
            
            public string Email { get; set; }
           
            public string PhoneNo { get; set; }
            
            public string Address1 { get; set; }
           
            public string Address2 { get; set; }
          
            public string City { get; set; }
          
            public string PostCode { get; set; }
        }
    }
}
