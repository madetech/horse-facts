﻿using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using HorseFacts.Core.GatewayInterfaces;

namespace HorseFacts.Core.UseCases
{
    public class GetRandomQuestionableHorseFact : IGetRandomQuestionableHorseFact
    {
        private readonly IAnimalFactGateway _animalFactGateway;

        public GetRandomQuestionableHorseFact(IAnimalFactGateway animalFactGateway)
        {
            _animalFactGateway = animalFactGateway;
        }

        public GetRandomQuestionableHorseFactResponse Execute()
        {
            var animals = new[] {"cat", "dog", "zebra", "giraffe", "lion"};

            var fact = _animalFactGateway.GetAnimalFact();

            var horseFact = animals.Aggregate(fact.Fact, (current, animal) =>
                Regex.Replace(current, @"\b" + animal + @"(s?)\b", "horse$1"));

            return new GetRandomQuestionableHorseFactResponse
            {
                HorseFact = horseFact
            };
        }
    }
}
