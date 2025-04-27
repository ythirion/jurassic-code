using System;
using System.Collections.Generic;
using JurassicCode.DataAccess.Entities;

namespace JurassicCode.DataAccess.Repositories
{
	public class Database
	{
		public readonly Dictionary<string, ZoneEntity> Zones = new Dictionary<string, ZoneEntity>();
		private readonly Dictionary<string, DinosaurEntity> _dinosaurs = new Dictionary<string, DinosaurEntity>();

		public Dictionary<string, DinosaurEntity> GetDinosaurs()
		{
			return _dinosaurs;
		}
	}
}