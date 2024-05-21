﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
    }
}
