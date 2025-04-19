namespace Stefanini.Infra.Data.Mapping.Seed
{
    public static class CitySeed
    {
        public static IEnumerable<object> Cities() =>
            Enumerable.Range(1, 17).Select(i => new { Id = i });

        public static IEnumerable<object> CityNames() =>
        [
            new { CityId = 1, Value = "São Paulo" },
            new { CityId = 2, Value = "Rio de Janeiro" },
            new { CityId = 3, Value = "Belo Horizonte" },
            new { CityId = 4, Value = "Salvador" },
            new { CityId = 5, Value = "Porto Alegre" },
            new { CityId = 6, Value = "Curitiba" },
            new { CityId = 7, Value = "Fortaleza" },
            new { CityId = 8, Value = "Recife" },
            new { CityId = 9, Value = "Manaus" },
            new { CityId = 10, Value = "Belém" },
            new { CityId = 11, Value = "Goiânia" },
            new { CityId = 12, Value = "Campo Grande" },
            new { CityId = 13, Value = "Teresina" },
            new { CityId = 14, Value = "São Luís" },
            new { CityId = 15, Value = "João Pessoa" },
            new { CityId = 16, Value = "Natal" },
            new { CityId = 17, Value = "Maceió" }
        ];


        public static IEnumerable<object> CityUFs() =>
        [
            new { CityId = 1, Value = "SP" },
            new { CityId = 2, Value = "RJ" },
            new { CityId = 3, Value = "MG" },
            new { CityId = 4, Value = "BA" },
            new { CityId = 5, Value = "RS" },
            new { CityId = 6, Value = "PR" },
            new { CityId = 7, Value = "CE" },
            new { CityId = 8, Value = "PE" },
            new { CityId = 9, Value = "AM" },
            new { CityId = 10, Value = "PA" },
            new { CityId = 11, Value = "GO" },
            new { CityId = 12, Value = "MS" },
            new { CityId = 13, Value = "PI" },
            new { CityId = 14, Value = "MA" },
            new { CityId = 15, Value = "PB" },
            new { CityId = 16, Value = "RN" },
            new { CityId = 17, Value = "AL" }
        ];
    }
}
