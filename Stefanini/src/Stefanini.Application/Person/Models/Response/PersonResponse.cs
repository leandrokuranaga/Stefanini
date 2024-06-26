﻿using Stefanini.Domain.PersonAggregate;

namespace Stefanini.Application.Person.Models.Response
{
    public record PersonResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Cpf { get; set; }
        public int Age { get; set; }

        public static explicit operator PersonResponse(PersonDomain person)
        {
            return new PersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Cpf = person.CPF,
                Age = person.Age
            };
        }
    }
}
