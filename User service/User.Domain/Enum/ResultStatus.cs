using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Enum
{
    public enum ResultStatus
    {
        InvalidToken,
        Ok,
        ParameterError,
        dataBaseError,
        processError,
        unHandeledError,
        kafkaError
    }
}
