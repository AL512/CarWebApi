using CarWebApi.Models.Countries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWebApi.Queries.Countries
{
    /// <summary>
    /// Запрос списка стран производителей
    /// </summary>
    public class GetCountryListQuery : IRequest<CountryList>
    {
        //Дополнительная логика. Фильтрация по ролям и пр.
    }
}
