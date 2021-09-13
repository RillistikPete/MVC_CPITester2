using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCTesterCPI2.Infrastructure.Interfaces
{
    public interface ICpiClient
    {
        /// <summary>
        /// Send a GET request
        /// </summary>
        /// <typeparam name="T">Resource Model</typeparam>
        /// <param name="resourceUri">Resource Endpoint URI</param>
        /// <param name="properties">Additional string properties of object</param>
        /// <param name="offset">Number of records to skip</param>
        /// <param name="limit">Record limit</param>
        /// <returns>List of records of certain type</returns>
        Task<HttpResponse<List<T>>> Get<T>(string resourceUri, IDictionary<string, string> properties = null, int offset = 0, int limit = 100);

        /// <summary>
        /// Send a GET request with specific modifier in query
        /// </summary>
        /// <typeparam name="T">Resource Model</typeparam>
        /// <param name="resourceUri">Resource Endpoint URI including the query parameters</param>
        /// <param name="offset">Number of records to skip</param>
        /// <param name="limit">Record limit</param>
        /// <returns>List of records with model specified in Type</returns>
        Task<HttpResponse<List<T>>> GetBySpecific<T>(string resourceUri, IDictionary<string, string> properties = null, int offset = 0, int limit = 100);

        /// <summary>
        /// Send a GET request for a specific record by id
        /// </summary>
        /// <typeparam name="T">Resource Model</typeparam>
        /// <param name="resourceUri">Resource Endpoint URI</param>
        /// <param name="id">Record Id</param>
        /// <returns>Single record by id</returns>
        Task<HttpResponse<T>> GetById<T>(string resourceUri, string id, IDictionary<string, string> properties = null) where T : new();

        /// <summary>
        /// Send a GET request that returns one record
        /// </summary>
        /// <typeparam name="T">Resource Model</typeparam>
        /// <param name="resourceUri">Resource Endpoint URI</param>
        /// <returns>Single Record Object specified by <typeparamref name="T"/></returns>
        Task<HttpResponse<T>> GetSingle<T>(string resourceUri, IDictionary<string, string> properties = null) where T : new();

        /// <summary>
        /// Send a PUT request
        /// </summary>
        /// <param name="resourceUri">Resource Endpoint URI</param>
        /// <param name="id">Record Id to send request to</param>
        /// <param name="dto">Data Transfer Object to send with the request</param>
        /// <returns></returns>
        Task<ServerResponse> Put(string resourceUri, string id, dynamic dto);
    }
}