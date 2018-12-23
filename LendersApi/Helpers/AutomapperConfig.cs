﻿using AutoMapper;
using LendersApi.Dto;
using LendersApi.Repository.Model;

namespace LendersApi.Helpers
{
	public class AutoMapperConfig
	{
		public static void Initialize()
		{
			Mapper.Initialize(cfg => {
				cfg.CreateMap<Person, PersonDto>();
				cfg.CreateMap<Loan, LoanDto>();
			});
		}
	}
}