using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;

namespace apiEsFeDemostracion.Helpers
{
    public class MiFiltroDeExcepcion: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logger = LogManager.GetLogger(typeof(Program));

            logger.Error("Hello World!");
        }
    }
}
