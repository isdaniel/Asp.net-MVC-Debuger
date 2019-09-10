using System.Collections.Generic;
using System.ComponentModel;

namespace Asp.net_MVC_Debuger.Models
{
    public class MessageViewModel
    {
        public AddressInfo Address{ get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }
    }
}