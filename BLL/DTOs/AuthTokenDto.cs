using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AuthTokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
