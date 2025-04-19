namespace Stefanini.Infra.Data.Mapping.Seed
{
    public static class PersonSeed
    {
        public static IEnumerable<object> People() =>
            new[]
            {
                new { Id = 1, CityId = 1 },
                new { Id = 2, CityId = 2 },
                new { Id = 3, CityId = 3 },
                new { Id = 4, CityId = 4 },
                new { Id = 5, CityId = 5 }
            };

        public static IEnumerable<object> PersonNames() =>
            new[]
            {
                new { PersonId = 1, Value = "John Doe" },
                new { PersonId = 2, Value = "Jane Smith" },
                new { PersonId = 3, Value = "Alice Johnson" },
                new { PersonId = 4, Value = "Bob Brown" },
                new { PersonId = 5, Value = "Charlie Davis" },
            };

        public static IEnumerable<object> PersonCPFs() =>
            new[]
            {
                new { PersonId = 1, Value = "12345678901" },
                new { PersonId = 2, Value = "23456789012" },
                new { PersonId = 3, Value = "34567890123" },
                new { PersonId = 4, Value = "45678901234" },
                new { PersonId = 5, Value = "56789012345" },
            };

        public static IEnumerable<object> PersonAges() =>
            new[]
            {
                new { PersonId = 1, Value = 30 },
                new { PersonId = 2, Value = 25 },
                new { PersonId = 3, Value = 28 },
                new { PersonId = 4, Value = 35 },
                new { PersonId = 5, Value = 40 },
            };
    }
}
