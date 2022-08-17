﻿using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Services
{
    public interface IActivityService
    {
        public Task<ActionResult<string>> GetRandomActivityInGroupAsync(int userId, int groupId);
        public Task<ActionResult<string>> GetRandomActivityAloneAsync();
    }
}