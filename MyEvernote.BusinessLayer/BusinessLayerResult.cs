using MyEverNote.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class BusinessLayerResult<T> where T: class
    {
       public List<ErrorMessageObject> Errors { get; set; }

       public T Result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessageObject>();
        }

        public void AddError(ErrorMessageCode code , string message)
        {
            Errors.Add(new ErrorMessageObject() { Code = code, Message = message });
        }
    }
}