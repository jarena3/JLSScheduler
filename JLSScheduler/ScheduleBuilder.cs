using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JLSScheduler
{
    class ScheduleBuilder
    {
        public Book[] Books;

        public void Init()
        {
            //get books into our array
            Books = JsonConvert.DeserializeObject<Book[]>(Properties.Resources.Books);        
        }
    }
}
