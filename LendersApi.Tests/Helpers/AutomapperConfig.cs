using AutoMapper;
using LendersApi.Dto;
using LendersApi.Repository.Model;

namespace LendersApi.Tests.Helpers
{
	public class AutoMapperConfig
	{
		public static void Initialize()
		{
			Mapper.Initialize(cfg => {
				cfg.CreateMap<Person, PersonDto>();
				cfg.CreateMap<PersonCreateDto, Person>();
				cfg.CreateMap<Loan, LoanDto>();
				cfg.CreateMap<LoanCreateDto, Loan>();
			});
		}
	}
}
