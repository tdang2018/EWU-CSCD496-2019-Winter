using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.ApiModels;

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        public GroupsController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Groups = await secretSantaClient.GetGroupsAsync();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GroupInputViewModel group)
        {
            IActionResult result = View();
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    await secretSantaClient.CreateGroupAsync(group);
                    result = RedirectToAction(nameof(Index));
                }
                catch (SwaggerException se)
                {
                    ViewBag.ErrorMessage = se.Message;
                }
            }
            return result;
        }

        [HttpGet]
        public IActionResult Edit(int groupId)
        {
            ViewBag.GroupId = groupId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupInputViewModel group, int groupId)
        {
            IActionResult result = View();

            if (string.IsNullOrEmpty(group.Name))
            {
                ViewBag.ErrorMessage = "Group name cannot be empty!";
                ModelState.AddModelError("Name", "Group name is required!");
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    try
                    {
                        var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                        await secretSantaClient.UpdateGroupAsync(groupId, group);
                        result = RedirectToAction(nameof(Index));
                    }
                    catch (SwaggerException se)
                    {
                        ViewBag.ErrorMessage = se.Message;
                    }
                }
            }
            return result;
        }

        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result = View();
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    await secretSantaClient.DeleteGroupAsync(id);

                    result = RedirectToAction(nameof(Index));
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }

            return result;
        }
    }
}