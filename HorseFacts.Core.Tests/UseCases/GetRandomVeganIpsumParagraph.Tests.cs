﻿using FluentAssertions;
using HorseFacts.Core.Domain;
using HorseFacts.Core.GatewayInterfaces;
using HorseFacts.Core.UseCases;
using HorseFacts.WordProviders.Gateways;
using System.Threading.Tasks;
using Xunit;

namespace HorseFacts.Core.Tests.UseCases
{
    public class GetRandomVeganIpsumParagraphTests : IFoodIpsumGateway
    {
        private string _sentence;
        private readonly GetRandomVeganIpsumParagraph _subject;

        public GetRandomVeganIpsumParagraphTests()
        {
            _subject = new GetRandomVeganIpsumParagraph(this, new MeatWordProvider());
        }

        [Fact]
        public async Task Sentence_should_be_unchanged_when_there_are_no_meat_words()
        {
            _sentence = "Suspendisse nulla erat, interdum nec est laoreet, gravida iaculis nunc";

            var result = await _subject.Execute();

            result.VeganIpsumParagraph.Should().Be(_sentence);
        }

        [Fact]
        public async Task Meat_words_should_be_replaced_with_vegetable_words()
        {
            _sentence = "Ex dolor velit proident swine. Dolor fatback hamburger ut tail ribeye esse pork loin cillum";
            var expected = "Ex dolor velit proident banana. Dolor banana banana ut banana banana esse banana cillum";

            var result = await _subject.Execute();

            result.VeganIpsumParagraph.Should().Be(expected);
        }

        [Fact]
        public async Task Plural_meat_words_should_be_replaced_with_plural_vegetable_words()
        {
            _sentence = "Ex dolor velit proident swines. Dolor fatbacks hamburgers ut tails ribeyes esse short loins cillum";
            var expected = "Ex dolor velit proident bananas. Dolor bananas bananas ut bananas bananas esse bananas cillum";

            var result = await _subject.Execute();

            result.VeganIpsumParagraph.Should().Be(expected);
        }

        [Fact]
        public async Task Capital_case_meat_words_should_be_replaced_with_capital_case_vegetable_words()
        {
            _sentence = "Ex dolor velit proident Swines. Dolor Fatback Hamburgers ut Tails Ribeye esse Short loins cillum";
            var expected = "Ex dolor velit proident Bananas. Dolor Banana Bananas ut Bananas Banana esse Bananas cillum";

            var result = await _subject.Execute();

            result.VeganIpsumParagraph.Should().Be(expected);
        }

        public Task<FoodIpsum> GetFoodIpsum()
        {
            return Task.FromResult(new FoodIpsum
            {
                Paragraph = _sentence
            });
        }
    }
}
