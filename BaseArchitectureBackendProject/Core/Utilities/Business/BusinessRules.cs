using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    //İş katmanındaki bir çok if kontrolünü buraya taşımak amacıyla oluşturduğumuz bir yapı.
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var result in logics)
            {
                if (!result.Success)
                    return result;
            }

            return null;
        }
    }
}
