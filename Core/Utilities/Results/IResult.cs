using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }

        /*
        
        Why only getter used ?
        Because, getters can only set in constructres, we canalize the programer to use contsructure.
        This is a stantartisation for our architecture.
                                                                                                (ebrar)
         
         */
    }
}
