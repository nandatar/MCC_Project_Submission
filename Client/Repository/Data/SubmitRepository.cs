using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Models;
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
    public class SubmitRepository : GeneralRepository<Project, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public SubmitRepository(Address address, string request = "Projects/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

    
        public async Task<ResponVM> Submit([FromForm] SubmitProjectVM submitProject)
        {
            ResponVM status = null;
            var UMLStream = new MemoryStream();
            var BPMNStream = new MemoryStream();

            submitProject.UML.CopyToAsync(UMLStream);
            submitProject.BPMN.CopyToAsync(BPMNStream);

            var submit = new SubmitVM
            {
                ProjectTitle = submitProject.Title,
                Description = submitProject.Description,
                CurrentStatus = 1,
                UML = UMLStream.ToArray(),
                BPMN = BPMNStream.ToArray(),
                Link = submitProject.Link
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(submit), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            status = JsonConvert.DeserializeObject<ResponVM>(apiResponse);

            return status;
        }
    }
}
