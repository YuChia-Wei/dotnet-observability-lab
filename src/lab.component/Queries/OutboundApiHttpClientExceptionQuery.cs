﻿using lab.component.Entities;
using Mediator;

namespace lab.component.Queries;

public class OutboundApiHttpClientExceptionQuery : IQuery<IEnumerable<WeatherForecast>>
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}