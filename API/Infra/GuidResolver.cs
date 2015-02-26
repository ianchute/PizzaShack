using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace API.Infra
{
    public class GuidResolver : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            return source.New(Guid.NewGuid());
        }
    }
}
