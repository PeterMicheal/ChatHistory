using System;
using System.Collections.Generic;
using System.Text;

namespace PowerDiaryBusiness.BusinessViewModels
{
   
    public class VrChat
    {
        /// <summary>
        /// Id of Message 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Message body
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Time Of Message
        /// </summary>
        public DateTime Time { get; set; }
    }
}
