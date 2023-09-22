using System;
using locationsApp.Models.Requests;
using locationsApp.Models.Responses;
using System.Threading.Tasks;
using locationsApp.Models.Responses.LocationResponse;
using System.Collections.Generic;

namespace locationsApp.Services.Interfaces
{
	public interface ILocationService
	{
        Task<IList<SearchResponse>> SearchAsync(SearchRequest SearchCriteria);

        Task<LocationResponse> GetLocation(LocationRequest location);

        Task<IList<PhotoResponse>> GetPhotos(IList<PhotoRequest> photos);
    }
}

