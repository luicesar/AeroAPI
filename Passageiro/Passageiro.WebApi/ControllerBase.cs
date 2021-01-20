using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Passageiro.Service.Interfaces;
using Passageiro.Shared.Entities;
using Passageiro.WebApi.Models;

namespace Passageiro.WebApi {
    public abstract class ControllerBase<E, M> : Controller where E : Entity where M : IModel<E> {
        private IServiceBase<E> repository;
        public ControllerBase (IServiceBase<E> repository) {
            this.repository = repository;
        }

        [NonAction]
        private M GetById (int Id) {
            var result = repository.GetEntityById (Id);
            return Mapper.Map<M> (result);
        }

        [NonAction]
        private IEnumerable<M> Get () {
            var result = repository.GetAll ().ToList ();
            foreach (var item in result)
                yield return Mapper.Map<M> (item);
        }

        [NonAction]
        private IActionResult Create ([FromBody] M model) {
            var _mapDomain = model.MapForDomain ();

            if (_mapDomain.Invalid)
                return BadRequest (new { errors = _mapDomain.Notifications });

            var isCreated = repository.Create (_mapDomain);
            if (isCreated)
                return Created (string.Empty, Mapper.Map<M> (_mapDomain));
            else {
                //Validações adicionais usando o repository.
                if (_mapDomain.Valid)
                    return StatusCode (500);
                else
                    return BadRequest (new { errors = _mapDomain.Notifications });
            }
        }

        [NonAction]
        private IActionResult Update (int Id, [FromBody] M model) {
            var foundEntity = repository.GetEntityById (Id);
            if (foundEntity == null)
                return NotFound ();

            var _mapDomain = model.MapForDomain ();

            foundEntity = (E) _mapDomain;

            if (_mapDomain.Invalid)
                return BadRequest (
                    new { errors = _mapDomain.Notifications });

            var isUpdated = repository.Update (Id, foundEntity);
            if (isUpdated)
                return Accepted (Mapper.Map<M> (foundEntity));
            else {
                //Validações adicionais usando o repository.
                if (_mapDomain.Valid)
                    return StatusCode (500);
                else
                    return BadRequest (new { errors = _mapDomain.Notifications });
            }
        }

        [NonAction]
        private IActionResult Delete (int Id) {
            var isDeleted = repository.DeleteEntity (Id);
            if (isDeleted)
                return NoContent ();
            else
                return NotFound ();
        }

        // [Authorize("Bearer")]
        [HttpGet]
        public virtual IEnumerable<M> GetAll () => this.Get ();

        // [Authorize("Bearer")]
        [HttpGet ("{Id}")]
        public virtual M GetEntityById (int Id) => this.GetById (Id);

        // [Authorize("Bearer")]
        [HttpPost]
        public virtual IActionResult CreateEntity ([FromBody] M model) => this.Create (model);

        // [Authorize("Bearer")]
        [HttpPut ("{Id}")]
        public virtual IActionResult UpdateEntity (int Id, [FromBody] M model) => this.Update (Id, model);

        // [Authorize("Bearer")]
        [HttpDelete ("{Id}")]
        public virtual IActionResult DeleteEntity (int Id) => this.Delete (Id);
    }
}