﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using GrapeCity.Viewer.Common;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DeliveryNotesByOrderController : ApiController
    {
        #region Private

        private readonly IDeliveryNotesService _deliveryNotesService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DeliveryNotesByOrderController(IDeliveryNotesService deliveryNotesService)
        {
            _deliveryNotesService = deliveryNotesService;
        }

        #endregion
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [ApiException]
        public List<DeliveryNote> Post(DeliveryNoteBaseResponse deliveryNoteList)
        {
            if (deliveryNoteList == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            List <DeliveryNote> updatedNote = new List<DeliveryNote>();
            if (deliveryNoteList.DeliveryNotes.Any())
            {

                deliveryNoteList.DeliveryNotes.ForEach(note => updatedNote.Add(_deliveryNotesService.SaveDeliveryNote(note.CreateFrom()).CreateFromListView()));
                //foreach (var deliveryNote in deliveryNoteList.DeliveryNotes)
                //{
                //    updatedNote.Add(_deliveryNotesService.SaveDeliveryNote(deliveryNote.CreateFrom()).CreateFromListView());
                //}
                
            }
            return updatedNote;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}