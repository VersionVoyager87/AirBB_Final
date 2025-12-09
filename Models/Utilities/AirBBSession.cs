using Microsoft.AspNetCore.Http;
using AirBB.Models.DataLayer;
using AirBB.Models.DomainModels;
using AirBB.Models.ViewModels;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace AirBB.Models.Utilities
{
    public class AirBBSession
    {
        private const string CriteriaKey = "filtercriteria";
        private const string ReservationKey = "reservations";

        private readonly ISession session;

        public AirBBSession(ISession session) => this.session = session;

        public void SetFilter(FilterCriteria criteria)
        {
            var json = JsonSerializer.Serialize(criteria);
            session.SetString(CriteriaKey, json);
        }

        public FilterCriteria? GetFilter()
        {
            var json = session.GetString(CriteriaKey);
            return json == null ? null :
                JsonSerializer.Deserialize<FilterCriteria>(json);
        }

        public void SetReservations(List<Reservation> reservations)
        {
            var dtoList = reservations.Select(r => new ReservationDTO(r)).ToList();
            var json = JsonSerializer.Serialize(dtoList);
            session.SetString(ReservationKey, json);
        }

        public List<Reservation> GetReservations(AirBBContext context)
        {
            var json = session.GetString(ReservationKey);
            if (string.IsNullOrEmpty(json)) return new List<Reservation>();

            var dtoList = JsonSerializer.Deserialize<List<ReservationDTO>>(json) ?? new();
            return dtoList.Select(dto => dto.ToReservation(context)).ToList();
        }

        public int GetReservationCount(AirBBContext context)
        {
            return GetReservations(context).Count;
        }
    }
}
