public class Day5
{
    private List<string> lines;
    struct Map
    {
        public long sourceStart;
        public long destinationStart;
        public long length;
        public long sourceEnd { get { return sourceStart + (length - 1); } }
        public bool IsInRange(long source)
        {
            return source > sourceStart && source < sourceEnd;
        }
        public long GetDesination(long source)
        {
            return destinationStart + (source - sourceStart);
        }
    }

    List<int> Seeds = new List<int>();
    List<Map> SeedToSoil = new List<Map>();
    List<Map> SoilToFertilizer = new List<Map>();
    List<Map> FertilizerToWater = new List<Map>();
    List<Map> WaterToLight = new List<Map>();
    List<Map> LightToTemperature = new List<Map>();
    List<Map> TemperatureToHumidity = new List<Map>();
    List<Map> HumidityToLocation = new List<Map>();
    public Day5()
    {

        lines = File.ReadLines("C:\\src\\te\\AdventOfCode\\AdventOfCode\\Day5\\input.txt")
                    .ToList();
        var stsMap = lines.FindIndex(e => e.Contains("seed-to-soil map"));
        var stfMap = lines.FindIndex(e => e.Contains("soil-to-fertilizer map"));
        var ftwMap = lines.FindIndex(e => e.Contains("fertilizer-to-water map"));
        var wtlMap = lines.FindIndex(e => e.Contains("water-to-light map"));
        var lttMap = lines.FindIndex(e => e.Contains("light-to-temperature map"));
        var tthMap = lines.FindIndex(e => e.Contains("temperature-to-humidity map"));
        var htlMap = lines.FindIndex(e => e.Contains("humidity-to-location map"));


        SeedToSoil = CreateMap(stsMap + 1, stfMap - 1);
        SoilToFertilizer = CreateMap(stfMap + 1, ftwMap - 1);
        FertilizerToWater = CreateMap(ftwMap + 1, wtlMap - 1);
        WaterToLight = CreateMap(wtlMap + 1, lttMap - 1);
        LightToTemperature = CreateMap(lttMap + 1, tthMap - 1);
        TemperatureToHumidity = CreateMap(tthMap + 1, htlMap - 1);
        HumidityToLocation = CreateMap(htlMap + 1, lines.Count());

    }
    public long Part_1()
    {
        var seeds = lines.First()
                         .Replace("seeds: ", "")
                         .Split(" ")
                         .Select(e => long.Parse(e));

        return GetResult(seeds);
    }
    public long Part_2()
    {
        var seeds = lines.First()
                         .Replace("seeds: ", "")
                         .Split(" ")
                         .Select(e => long.Parse(e))
                         .ToArray();
        List<long> allSeeds = new List<long>();
        for (long i = 0; i < seeds.Count(); i++)
        {
            var start = seeds[i];
            var end = seeds[i] + seeds[1 + i];
            for (long s = start; s < end; s++)
            {
                allSeeds.Add(s);
            }
            i++;
        }
        return GetResult(allSeeds);
    }

    private long GetResult(IEnumerable<long> seeds)
    {
        var resultList = new List<long>();
        long counter = 0;
        foreach (var seed in seeds)
        {
            var soil = MapValues(SeedToSoil, seed);
            var fertilizer = MapValues(SoilToFertilizer, soil);
            var water = MapValues(FertilizerToWater, fertilizer);
            var light = MapValues(WaterToLight, water);
            var temperature = MapValues(LightToTemperature, light);
            var humidity = MapValues(TemperatureToHumidity, temperature);
            var location = MapValues(HumidityToLocation, humidity);
            resultList.Add(location);
            counter++;
        }
        return resultList.Min();
    }
    private long MapValues(List<Map> maps, long input)
    {
        var seedToSoilMap = maps.FirstOrDefault(e => e.IsInRange(input));
        if (seedToSoilMap.Equals(default(Map)))
            return input;
        else
            return seedToSoilMap.GetDesination(input);
    }

    private List<Map> CreateMap(int startIndex, int endIndex)
    {
        var result = new List<Map>();
        return lines.GetRange(startIndex, endIndex - startIndex)
        .Select(e =>
        {
            var parts = e.Split(" ");
            return new Map
            {
                sourceStart = long.Parse(parts[1]),
                destinationStart = long.Parse(parts[0]),
                length = int.Parse(parts[2])
            };
        }).ToList();
    }
}
