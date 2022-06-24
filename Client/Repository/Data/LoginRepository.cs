using API;
using API.ViewModel;
using API.ViewModel.ResponseViewModels;
using Client.Base;
using Client.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class LoginRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginRepository(Address address, string request = "Accounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public async Task<Object> GetRegistered()
        {
            Object result;

            using (var response = await httpClient.GetAsync(request + "getRegisteredData/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GetRegisteredEmployeeVM>(apiResponse);
            }
            return result;
        }

        public async Task<Object> GetEmployee()
        {
            Object result;

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GetEmployeeVM>(apiResponse);
            }
            return result;
        }

        public async Task<Object> GetEmployeeGenderDistribution()
        {
            Object result;

            using (var response = await httpClient.GetAsync(request + "GetGenderDistribution/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return result;
        }

        public async Task<Object> Register(RegisterVM registerVM)
        {
            Object result;

            using (var response = await httpClient.PostAsync(request + "register/", new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json")))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return result;
        }

        public async Task<Object> GetEmployeeByNIK(GetByNIKVM getByNIKVM )
        {
            Object result;

            using (var response = await httpClient.PostAsync(request + "GetEmployeeByNIK/", new StringContent(JsonConvert.SerializeObject(getByNIKVM), Encoding.UTF8, "application/json")))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return result;
        }

        public async Task<JWTokenVM> Auth(LoginVM loginVM)
        {
            JWTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "login/", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

            return token;
        }
    }
}
