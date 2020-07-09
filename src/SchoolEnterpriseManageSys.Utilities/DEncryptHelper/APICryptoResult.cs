using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.DEncryptHelper
{
    public class APICryptoResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public long Timestamp { get; set; }
        public string Ciphertext { get; set; }
        public string Plaintext { get; set; }
    }
}
