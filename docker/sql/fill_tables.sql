INSERT INTO public."WeatherForecast" ("Date", "TemperatureC", "Summary")
SELECT
        now() - random() * interval '30 days',
        trunc(random() * 50) - 20,
        CASE trunc(random() * 10)
            WHEN 0 THEN 'Hot'
            WHEN 1 THEN 'Cold'
            WHEN 2 THEN 'Rainy'
            WHEN 3 THEN 'Sunny'
            WHEN 4 THEN 'Cloudy'
            WHEN 5 THEN 'Feedmix'
            WHEN 6 THEN 'Camido'
            WHEN 7 THEN 'Wikibox'
            WHEN 8 THEN 'Meejo'
            WHEN 9 THEN 'Yombu'
            WHEN 10 THEN 'Lazzy'
            END
FROM generate_series(1, 100);