using Core.Domain.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Bases
{
	public class ResponseHandler
	{

        public Response<T> Deleted<T>(T entity, string message = null)
		{
			return new Response<T>()
			{
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = message == null ? SharedResourcesKeys.Deleted : message
			};
		}
		public Response<T> Success<T>(T entity, object Meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = SharedResourcesKeys.Success,
				Meta = Meta
			};
		}
		public Response<T> Unauthorized<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.Unauthorized,
				Succeeded = true,
				Message = message == null ? SharedResourcesKeys.UnAuthorized : message
			};
		}
		public Response<T> BadRequest<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.BadRequest,
				Succeeded = false,
				Message = message == null ? "Bad Request" : message
			};
		}
		public Response<T> UnprocessableEntity<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
				Succeeded = false,
				Message = message == null ? "UnpProcessableEntity" : message
			};
		}

        public Response<T> NotFound<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Succeeded = false,
				Message = message == null ? "Not Found" : message
			};
		}

		public Response<T> Created<T>(T entity, object Meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.Created,
				Succeeded = true,
				Message = SharedResourcesKeys.Created,
				Meta = Meta
			};
		}
	}


}
