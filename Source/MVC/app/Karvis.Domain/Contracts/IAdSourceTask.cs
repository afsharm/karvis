using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Domain.Dto;
using Razmyar.Domain.Contracts.Tasks;

namespace Karvis.Domain.Tasks
{
    public interface IAdSourceTask
    {
        IEnumerable<AdSourceDto> GetAllAdSources();
    }
}
