using Core.Application.DTOs.NewFolder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Queries
{
    public class FindByIdAsyncQuery : IRequest<ApplicationUserDto>
    {
        public string userId;
    }
}
