using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Data {
    public class PostCode {
        public string postalCode { get; set; }
        public List<Address> addresses { get; set; }
    }

    public class Address {
        public string prefectureCode { get; set; }
        public Japanese ja { get; set; }
    }

    public class Japanese {
        public string prefecture { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string address4 { get; set; }
    }
}
