using System;
using System.Threading.Tasks;
using BlogApi.Core.Interfaces;
using BlogApi.Core.Interfaces.Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BlogApi.Core
{
    public abstract class ControllerBase<TEntity> : Controller where TEntity : class, IEntity
    {
        protected readonly IServiceBase<TEntity> _service;

        public ControllerBase(IServiceBase<TEntity> service) => _service = service;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync();
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JObject entity)
        {
            var result = await _service.AddAsync(entity.ToObject<TEntity>());
            return Json(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] JObject entity)
        {
            var item = await _service.GetByIdAsync(id);
            MergeValues(item, entity);
            var result = await _service.UpdateAsync(id, item);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.RemoveAsync(id);
            return Json(result);
        }

        protected void MergeValues(TEntity target, JObject data)
        {
            var props = data.Properties();
            Type t = typeof(TEntity);
            foreach (var prop in props)
            {
                var p = t.GetProperty(prop.Name);
                p.SetValue(target, prop.Value.ToObject<String>());
            }
        }
    }
}