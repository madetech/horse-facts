using HorseFacts.Core.GatewayInterfaces;

namespace HorseFacts.WordProviders.Gateways
{
    public class AnimalWordProvider : IProvideWords
    {
        private static string[] _animals = new[]
        {
            "cat", "dog", "zebra", "giraffe", "lion", "tabby", "tabbie", "jaguar",
            "squirrel", "lemur", "elephant", "gorilla", "kitten", "bird", "snake",
            "monkey", "ape", "koala", "kangaroo", "penguin", "bear", "tiger", "goose",
            "duck", "swan", "snail", "slug", "ant", "wasp", "bee", "hornet", "insect",
            "spider", "scorpion", "millipede", "centipede", "owl", "hedgehog", "wolf",
            "dragon", "rhino", "fox", "narwhal", "unicorn", "fish", "shark", "dolphin",
            "octopus", "whale", "sloth", "cheetah", "ocelot", "tuna", "cod", "haddock",
            "mackerel", "kipper", "leopard", "kitty", "kittie", "housecat", "escalator"
        };

        public string[] GetWords()
        {
            return _animals;
        }
    }
}
