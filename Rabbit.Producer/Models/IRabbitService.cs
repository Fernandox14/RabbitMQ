using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Producer.Models
{
    public interface IRabbitService
    {
        public void PostMessage(Category message);
    }
}
