﻿using System.Threading.Tasks;
using SolutionTemplate.Domain;
using SolutionTemplate.Persistence.Abstractions.Utils;

namespace SolutionTemplate.Persistence.Abstractions;

public interface IOrderRepository
{
    Task<long> Add(Order order, Connection connection);
    Task<Order> GetById(long id, Connection connection);
}
